using AutoMapper;
using beSQLSugar.Application.Dto.request.AnalyzeImage;
using beSQLSugar.Application.Dto.response.AnalyzeImage;
using beSQLSugar.Application.Dto.response.HeroSection;
using beSQLSugar.Infrastructure;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories.Images;
using beSQLSugar.Share.Utils;
using OpenCvSharp;
using System.Text.Json;

namespace beSQLSugar.Application.Services.AnalyzerImageServices
{
    public class AnalyzerImageService : IAnalyzerImageSerivce
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageToGridService _imageToGridService;
        private readonly IPathFindingService _pathFindingService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AnalyzerImageService(IImageRepository imageRepository, IImageService imageService ,IMapper mapper, IWebHostEnvironment env, IImageToGridService imageToGridService, IPathFindingService pathFindingService)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
            _pathFindingService = pathFindingService;
            _mapper = mapper;
            _env = env;
            _imageToGridService = imageToGridService;
        }

        public async Task<AnalyzeImageResponse> AddAsync(AnalyzeImageRequest request)
        {
            var image = _mapper.Map<AnalyzedImage>(request);

            // Xử lý upload media như cũ...
            if (request.FilePathUrl != null && request.FilePathUrl.Length > 0 && request.FilePathMap != null && request.FilePathMap.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.FilePathUrl.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                var uniqueFileNameMap = Guid.NewGuid().ToString() + Path.GetExtension(request.FilePathMap.FileName);
                var filePathMap = Path.Combine(uploadsFolder, uniqueFileNameMap);
                try
                {
                    //Ghi file ảnh tạm ra disk
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.FilePathUrl.CopyToAsync(fileStream);
                    }
                    
                    using (var fileStreamMap = new FileStream(filePathMap, FileMode.Create))
                    {
                        await request.FilePathMap.CopyToAsync(fileStreamMap);
                    }

                    //Phân tích ảnh
                    var analyzeResult = _imageService.Analyze(filePath, filePathMap);

                    //  Phân tích xong thì cập nhật entity
                    image.IsMapLike = analyzeResult.IsMap;

                    if (analyzeResult.IsMap)
                    {
                        var grid = _imageToGridService.ConvertImageToGrid(analyzeResult.CleanMaskPath);
                        var jagged = grid.ToJagged();

                        var json = JsonSerializer.Serialize(jagged);
                        var compressed = CompressionHelper.Compress(json);

                        image.GridDataCompressed = compressed;
                        image.FilePathUrl = analyzeResult.ResizedImage;
                        image.FilePathMap = analyzeResult.ResizedImageMap;

                    }
                    image.CreatedTime = DateTime.UtcNow;
                }
                finally
                {
                    //  Dù có lỗi hay không cũng xóa file tạm
                    if (File.Exists(filePath) && File.Exists(filePathMap))
                    {
                        try
                        {
                            File.Delete(filePath);
                            File.Delete(filePathMap);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Không thể xóa file tạm: {filePath}. Chi tiết: {ex.Message}");
                            // Có thể log vào hệ thống logging thực tế
                        }
                    }
                }
            }



            var addedEntity = await _imageRepository.AddAsync(image);
            var response = _mapper.Map<AnalyzeImageResponse>(addedEntity);
            response.Message = image.IsMapLike ? "Ảnh là bản đồ" : "Ảnh không phải bản đồ";

            return response;
        }

        public async Task<FindPathResponse> FindPathAsync(FindPathRequest request)
        {
            // Lấy ảnh từ DB
            var image = await _imageRepository.GetByIdAsync(request.ImageId);
            if (image == null)
                return new FindPathResponse { Success = false, Message = "Không tìm thấy ảnh." };

            if (string.IsNullOrEmpty(image.GridDataCompressed))
                return new FindPathResponse { Success = false, Message = "Ảnh chưa có dữ liệu grid." };

            //  Giải nén Base64 → JSON → int[][]
            var json = CompressionHelper.Decompress(image.GridDataCompressed);
            var jaggedArray = JsonSerializer.Deserialize<int[][]>(json);
            if (jaggedArray == null)
                return new FindPathResponse { Success = false, Message = "Dữ liệu grid không hợp lệ." };

            // . Chuyển về int[,]
            var grid = jaggedArray.To2D();

            // Gọi A* tìm đường
            var result = _pathFindingService.FindPath(grid, request.StartX, request.StartY, request.EndX, request.EndY);

            return result;
        }


    }
}
