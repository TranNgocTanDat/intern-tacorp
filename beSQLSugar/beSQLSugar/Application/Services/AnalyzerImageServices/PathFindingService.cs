using beSQLSugar.Application.Dto.response.AnalyzeImage;
using System.Collections.Generic;
using System.Linq;

namespace beSQLSugar.Application.Services.AnalyzerImageServices
{
    public class PathFindingService : IPathFindingService
    {
        private class Node
        {
            public int X { get; }
            public int Y { get; }
            public double G { get; set; } // cost từ start -> node hiện tại
            public double H { get; set; } // heuristic (ước lượng đến đích)
            public double F => G + H;     // tổng chi phí
            public Node? Parent { get; set; }

            public Node(int x, int y)
            {
                X = x;
                Y = y;
                G = 0;
                H = 0;
                Parent = null;
            }
        }

        // Các hướng di chuyển: lên, xuống, trái, phải
        private static readonly (int dx, int dy)[] Directions =
        {
            (1, 0), (-1, 0), (0, 1), (0, -1)
        };

        public FindPathResponse FindPath(int[,] grid, int startX, int startY, int endX, int endY)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            if (!IsValid(startX, startY, rows, cols) || !IsValid(endX, endY, rows, cols))
                return new FindPathResponse { Success = false, Message = "Điểm bắt đầu hoặc kết thúc không hợp lệ." };

            if (grid[startY, startX] == 1 || grid[endY, endX] == 1)
                return new FindPathResponse { Success = false, Message = "Điểm bắt đầu hoặc kết thúc nằm trong vật cản." };

            var openSet = new List<Node>();
            var closedSet = new HashSet<(int, int)>();

            var startNode = new Node(startX, startY);
            startNode.H = Heuristic(startX, startY, endX, endY);
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // 🔹 1. Lấy node có F thấp nhất trong openSet
                var current = openSet.OrderBy(n => n.F).First();

                // 🔹 2. Nếu đến đích → trả đường đi
                if (current.X == endX && current.Y == endY)
                    return new FindPathResponse
                    {
                        Success = true,
                        Message = "Tìm đường thành công (A*)!",
                        Path = ReconstructPath(current)
                    };

                openSet.Remove(current);
                closedSet.Add((current.X, current.Y));

                // 🔹 3. Duyệt 4 hướng
                foreach (var (dx, dy) in Directions)
                {
                    int nx = current.X + dx;
                    int ny = current.Y + dy;

                    if (!IsValid(nx, ny, rows, cols)) continue;
                    if (grid[ny, nx] == 1) continue; // vật cản
                    if (closedSet.Contains((nx, ny))) continue;

                    double tentativeG = current.G + 1;
                    var neighbor = openSet.FirstOrDefault(n => n.X == nx && n.Y == ny);

                    if (neighbor == null)
                    {
                        neighbor = new Node(nx, ny)
                        {
                            G = tentativeG,
                            H = Heuristic(nx, ny, endX, endY),
                            Parent = current
                        };
                        openSet.Add(neighbor);
                    }
                    else if (tentativeG < neighbor.G)
                    {
                        neighbor.G = tentativeG;
                        neighbor.Parent = current;
                    }
                }
            }

            return new FindPathResponse
            {
                Success = false,
                Message = "Không tìm thấy đường đi."
            };
        }

        private static double Heuristic(int x1, int y1, int x2, int y2)
        {
            // Manhattan distance (cho grid 4 hướng)
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private static bool IsValid(int x, int y, int rows, int cols)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }

        private static List<PathPoint> ReconstructPath(Node node)
        {
            var path = new List<PathPoint>();
            var current = node;

            while (current != null)
            {
                path.Add(new PathPoint { X = current.X, Y = current.Y });
                current = current.Parent;
            }

            path.Reverse();
            return path;
        }
    }
}
