using OpenCvSharp;

namespace beSQLSugar.Application.Services.AnalyzerImageServices
{
    public class ImageToGridService : IImageToGridService
    {
        public int[,] ConvertImageToGrid(string filePath)
        {
            // Load lại ảnh đã xử lý trước đó (mask nhị phân)
            var cleanMask = Cv2.ImRead(filePath, ImreadModes.Grayscale);

            if (cleanMask.Empty())
                throw new Exception("Không thể load mask ảnh để chuyển sang lưới.");


            int rows = cleanMask.Rows;
            int cols = cleanMask.Cols;
            int[,] grid = new int[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    byte pixelValue = cleanMask.At<byte>(y, x);
                    grid[y, x] = pixelValue > 128 ? 0 : 1;
                }
            }

            return grid;
        }
    }
}
