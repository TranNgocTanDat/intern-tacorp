using beSQLSugar.Application.Dto.response.AnalyzeImage;
using OpenCvSharp;

namespace beSQLSugar.Application.Services.AnalyzerImageServices
{
    public class ImageService : IImageService
    {
        public AImageResult Analyze(string filePath, string filePathMap)
        {
            var img = Cv2.ImRead(filePath);

            var imgMap = Cv2.ImRead(filePathMap);

            if (img.Empty())
                throw new Exception("Image not found or unable to load.");
            
            img = ResizeWithPadding(img, 800, 800);

            if (imgMap.Empty())
                throw new Exception("Image not found or unable to load.");

            imgMap = ResizeWithPadding(imgMap, 800, 800);

            // 1. Chuyển sang HSV
            Mat imgHsv = new Mat();
            Cv2.CvtColor(img, imgHsv, ColorConversionCodes.BGR2HSV);

            // Lọc màu xanh dương (đường đi)
            Scalar lowerBlue = new Scalar(100, 200, 150);
            Scalar upperBlue = new Scalar(115, 255, 255);
            Mat maskBlue = new Mat();
            Cv2.InRange(imgHsv, lowerBlue, upperBlue, maskBlue);

            // 5. Lọc vùng trắng (nền) - để loại bỏ
            Scalar lowerWhite = new Scalar(0, 0, 240);
            Scalar upperWhite = new Scalar(180, 20, 255);
            Mat maskWhite = new Mat();
            Cv2.InRange(imgHsv, lowerWhite, upperWhite, maskWhite);

            // 6. Lọc viền ô vuông (gần trắng nhưng hơi tối hơn)
            Scalar lowerLine = new Scalar(0, 0, 235);
            Scalar upperLine = new Scalar(180, 10, 239);
            Mat maskLine = new Mat();
            Cv2.InRange(imgHsv, lowerLine, upperLine, maskLine);

            // 7. Gộp cả nền trắng + viền vào 1 mask loại bỏ
            Mat maskRemove = new Mat();
            Cv2.BitwiseOr(maskWhite, maskLine, maskRemove);

            // 6. Đảo maskRemove
            Mat maskRemoveInv = new Mat();
            Cv2.BitwiseNot(maskRemove, maskRemoveInv);

            // 8. Loại bỏ nền và viền khỏi combinedMask
            Mat maskCleaned = new Mat();
            Cv2.BitwiseAnd(maskBlue, maskRemoveInv, maskCleaned);

            // 9. Làm sạch mask bằng morphological opening
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3));
            Mat cleanMask = new Mat();
            Cv2.MorphologyEx(maskCleaned, cleanMask, MorphTypes.Open, kernel);

            Cv2.GaussianBlur(cleanMask, cleanMask, new Size(3, 3), 0);
            // 10. Canny edge detection
            Mat edges = new Mat();
            Cv2.Canny(cleanMask, edges, 100, 200);

            // 11. HoughLinesP - phát hiện đường thẳng
            LineSegmentPoint[] lines = Cv2.HoughLinesP(edges, 1, Math.PI / 180, 50, minLineLength: 30, maxLineGap: 10);
            Console.WriteLine($"Số đường thẳng phát hiện: {lines.Length}");

            // 12. Phát hiện polygon từ contour
            Mat eroded = new Mat();
            Cv2.Erode(cleanMask, eroded, kernel);

            Cv2.FindContours(eroded, out Point[][] contours, out HierarchyIndex[] hierarchy, RetrievalModes.CComp, ContourApproximationModes.ApproxSimple);

            int polygonCount = 0;
            foreach (var contour in contours)
            {
                double area = Cv2.ContourArea(contour);
                if (area < 30) continue;

                double peri = Cv2.ArcLength(contour, true);
                var approx = Cv2.ApproxPolyDP(contour, 0.01 * peri, true);

                if (approx.Length >= 3 && approx.Length <= 10)
                {
                    polygonCount++;
                }
            }


            Console.WriteLine($"Số polygon phát hiện: {polygonCount}");

            // 13. Mật độ cạnh
            double edgeDensity = Cv2.CountNonZero(edges) / (double)(edges.Rows * edges.Cols);
            Console.WriteLine($"Mật độ cạnh: {edgeDensity}");

            // 14. Lưu các mask debug
            Cv2.ImWrite("maskGray.png", maskBlue);
            //Cv2.ImWrite("combinedMask.png", combinedMask);
            Cv2.ImWrite("maskWhite.png", maskWhite);
            Cv2.ImWrite("maskLine.png", maskLine);
            Cv2.ImWrite("maskRemove.png", maskRemove);
            Cv2.ImWrite("maskCleaned.png", maskCleaned);
            Cv2.ImWrite("cleanMask.png", cleanMask);


            string fileName = $"cleanMask_{Guid.NewGuid()}.png";
            string maskPath = Path.Combine("wwwroot", "uploads", fileName);
            Cv2.ImWrite(maskPath, cleanMask);

            var resizedFileName = $"resized_{Guid.NewGuid()}.png";
            var uploadsFolder = Path.Combine("wwwroot", "uploads"); // đảm bảo folder tồn tại
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var resizedPath = Path.Combine(uploadsFolder, resizedFileName);
            Cv2.ImWrite(resizedPath, img); // img ở đây là ảnh đã resize + padding

            var resizedFileNameMap = $"resized_{Guid.NewGuid()}.png";
            var resizedPathMap = Path.Combine(uploadsFolder, resizedFileNameMap);
            Cv2.ImWrite(resizedPathMap, imgMap); // img ở đây là ảnh đã resize + padding

            bool isMap = lines.Length > 10 && polygonCount > 4 && edgeDensity > 0.01;

            return new AImageResult
            {
                IsMap = isMap,
                CleanMaskPath = maskPath,
                ResizedImage = $"/uploads/{resizedFileName}",
                ResizedImageMap = $"/uploads/{resizedFileNameMap}",
                LineCount = lines.Length,
                PolygonCount = polygonCount,
                EdgeDensity = edgeDensity,
                Message = isMap ? "Ảnh có vẻ là bản đồ." : "Không phải bản đồ."
            };
        }


        private Mat ResizeWithPadding(Mat img, int targetWidth, int targetHeight)
        {
            int originalWidth = img.Width;
            int originalHeight = img.Height;

            float ratio = Math.Min((float)targetWidth / originalWidth, (float)targetHeight / originalHeight);
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            Mat resized = new Mat();
            Cv2.Resize(img, resized, new Size(newWidth, newHeight));

            int top = (targetHeight - newHeight) / 2;
            int bottom = targetHeight - newHeight - top;
            int left = (targetWidth - newWidth) / 2;
            int right = targetWidth - newWidth - left;

            Mat padded = new Mat();
            Cv2.CopyMakeBorder(resized, padded, top, bottom, left, right, BorderTypes.Constant, Scalar.Black);

            return padded;
        }

    }


}

