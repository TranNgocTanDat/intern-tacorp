using beSQLSugar.Application.Dto.request.Category;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repository.CategoryRepository;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Repositories.CategoryRepository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SqlSugarDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> FilterAsync(CategoryFilterRequest request)
        {
            var query = _context.Db.Queryable<Category>()
                .Includes(c => c.Parent)
                .Includes(c => c.Partner); 

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(c => SqlSugar.SqlFunc.Contains(c.Name, request.Name));
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(c => c.Description != null && SqlSugar.SqlFunc.Contains(c.Description, request.Description));
            }

            if (request.IsActive.HasValue)
            {
                query = query.Where(c => c.IsActive == request.IsActive.Value);
            }

            if (request.ParentId.HasValue)
            {
                query = query.Where(c => c.ParentId == request.ParentId.Value);
            }

            if (!string.IsNullOrEmpty(request.CreatedName))
            {
                query = query.Where(c => c.CreatedName != null && SqlFunc.Contains(c.CreatedName, request.CreatedName));
            }

            if (!string.IsNullOrEmpty(request.UpdatedName))
            {
                query = query.Where(c => c.UpdatedName != null && SqlFunc.Contains(c.UpdatedName, request.UpdatedName));
            }

            if (request.UpdateTimeFrom.HasValue)
            {
                query = query.Where(c => c.UpdateTime >= request.UpdateTimeFrom.Value);
            }

            if (request.UpdateTimeTo.HasValue)
            {
                query = query.Where(c => c.UpdateTime <= request.UpdateTimeTo.Value);
            }

            if (!string.IsNullOrEmpty(request.Note))
            {
                query = query.Where(c => c.Note != null && SqlSugar.SqlFunc.Contains(c.Note, request.Note));
            }

            return await query.ToListAsync();
        }


        public async Task<List<Category>> GetAllWithDetailsAsync()
        {
            // 1️⃣ Lấy category cha
            var parents = await _context.Db.Queryable<Category>()
                .Where(c => c.ParentId == null)

                .Includes(c => c.Parent)
                .Includes(c => c.Partner)
                .ToListAsync();

            if (!parents.Any())
                return new List<Category>();

            var parentIds = parents.Select(p => p.Id).ToList();

            // 2️⃣ Load children
            var children = await _context.Db.Queryable<Category>()
                .Where(c => parentIds.Contains(c.ParentId ?? 0))
                .Includes(c => c.Partner)
                .ToListAsync();

            // gán children vào parent
            foreach (var parent in parents)
            {
                parent.Children = children.Where(c => c.ParentId == parent.Id).ToList();
            }

            // 3️⃣ Load products cho cha và con
            var allCategoryIds = parents.Select(p => p.Id).Concat(children.Select(c => c.Id)).ToList();
            var products = await _context.Db.Queryable<Product>()
                .Where(p => allCategoryIds.Contains(p.CategoryId))
                .Includes(p => p.MediaList)  // include MediaList
                .Includes(p => p.Specs)
                .ToListAsync();

            // gán product cho cha và con
            foreach (var parent in parents)
            {
                parent.Products = products.Where(p => p.CategoryId == parent.Id).ToList();
            }
            foreach (var child in children)
            {
                child.Products = products.Where(p => p.CategoryId == child.Id).ToList();
            }

            return parents;
        }


        public async Task<Category?> GetByNameAsync(string name)
        {
            var category = _context.Db.Queryable<Category>()
                .Where(c => c.Name == name)
                .FirstAsync();
            return await category;
        }

        public async Task<List<Category>> GetCategoryChildrenAsync()
        {
            // 1️⃣ Lấy các category con (có ParentId)
            var children = await _context.Db.Queryable<Category>()
                .Where(c => c.ParentId != null)
                .Includes(c => c.Parent)
                .Includes(c => c.Partner)
                .ToListAsync();

            if (!children.Any())
                return new List<Category>();

            // 2️⃣ Lấy danh sách Id các category con
            var childIds = children.Select(c => c.Id).ToList();

            // 3️⃣ Lấy toàn bộ sản phẩm của các category con
            var products = await _context.Db.Queryable<Product>()
                .Where(p => childIds.Contains(p.CategoryId))
                .Includes(p => p.MediaList)
                .Includes(p => p.Specs)
                .ToListAsync();

            // 4️⃣ Gán product vào từng category con tương ứng
            foreach (var child in children)
            {
                child.Products = products
                    .Where(p => p.CategoryId == child.Id)
                    .ToList();
            }

            // 5️⃣ Trả về danh sách category con có đầy đủ thông tin
            return children;
        }


        public async Task<Category?> GetCategoryWithDetailsAsync(int id)
        {
            // 1️⃣ Lấy category cha
            var parent = await _context.Db.Queryable<Category>()
                .Where(c => c.Id == id)
                .Includes(c => c.Parent)
                .Includes(c => c.Partner)
                .FirstAsync();

            if (parent == null)
                return null;

            // 2️⃣ Lấy children của category cha
            var children = await _context.Db.Queryable<Category>()
                .Where(c => c.ParentId == parent.Id)
                .Includes(c => c.Partner)
                .ToListAsync();
            parent.Children = children;

            // 3️⃣ Lấy products của cha và con
            var allCategoryIds = new List<int> { parent.Id };
            allCategoryIds.AddRange(children.Select(c => c.Id));

            var products = await _context.Db.Queryable<Product>()
                .Where(p => allCategoryIds.Contains(p.CategoryId))
                .Includes(p => p.MediaList)  // include MediaList
                .Includes(p => p.Specs)
                .ToListAsync();

            // gán product cho cha
            parent.Products = products.Where(p => p.CategoryId == parent.Id).ToList();

            // gán product cho con
            foreach (var child in children)
            {
                child.Products = products.Where(p => p.CategoryId == child.Id).ToList();
            }

            return parent;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Db.Queryable<Category>()
                       .Includes(c => c.Parent)
                       .Includes(c => c.Partner)
                       .ToListAsync();
        }

    }
}
