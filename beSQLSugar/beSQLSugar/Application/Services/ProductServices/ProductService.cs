using AutoMapper;
using beSQLSugar.Application.Dto.request.Product;
using beSQLSugar.Application.Dto.response.Product;
using beSQLSugar.Application.Services.Helper;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories.Products;
using System.Security.Claims;

namespace beSQLSugar.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;

        public ProductService(IProductRepository productRepository, IMapper mapper, IUserContextService userContext)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<ProductResponse?> AddProductAsync(ProductRequest product)
        {
            int userId = _userContext.GetUserId();
            string userName = _userContext.GetUserName();
            decimal originalPrice = product.OriginalPrice ?? 0;
            decimal discountPrice = product.DiscountPrice ?? 0;
            int discount = (originalPrice > 0 && discountPrice > 0)
                ? (int)Math.Round((originalPrice - discountPrice) / originalPrice * 100)
                : 0;
            var existingProduct = await _productRepository.GetByNameAsync(product.ProductName);
            if (existingProduct != null)
            {
                throw new Exception("Product name already exists");
            }
            // Map DTO to entity
            var newProduct = _mapper.Map<Product>(product);
            newProduct.Discount = discount;
            newProduct.CreateUid = userId;
            newProduct.CreatedName = userName;
            // Save entity and return DTO response
            await _productRepository.AddAsync(newProduct);
            return _mapper.Map<ProductResponse>(newProduct);
        }

        // Xóa sản phẩm theo id
        public async Task<bool> DeleteProductAsync(int id)
        {
            // Kiểm tra nếu sản phẩm tồn tại
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            // Xóa sản phẩm
            await _productRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<ProductResponse?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductResponse>(product);
        }

        // Cập nhật sản phẩm theo slug
        public async Task<ProductResponse> UpdateProductAsync(int id, ProductRequest product)
        {
            int userId = _userContext.GetUserId();
            string userName = _userContext.GetUserName();
            decimal originalPrice = product.OriginalPrice ?? 0;
            decimal discountPrice = product.DiscountPrice ?? 0;
            int discount = (originalPrice > 0 && discountPrice > 0)
                ? (int)Math.Round((originalPrice - discountPrice) / originalPrice * 100)
                : 0;
            // Kiểm tra nếu sản phẩm tồn tại
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            // Kiểm tra nếu tên sản phẩm mới đã tồn tại
            var productWithSameName = await _productRepository.GetByNameAsync(product.ProductName);
            if (productWithSameName != null && productWithSameName.Id != id)
            {
                throw new Exception("Product name already exists");
            }
            // Map DTO sang entity
            var updatedProduct = _mapper.Map<Product>(product);
            updatedProduct.Discount = discount;
            updatedProduct.Id = id;
            updatedProduct.UpdatedName = userName;
            updatedProduct.UpdateTime = DateTime.Now;
            // Cập nhật entity
            await _productRepository.UpdateAsync(updatedProduct);
            return _mapper.Map<ProductResponse>(updatedProduct);
        }

        // Lọc sản phẩm theo các tiêu chí trong request
        public async Task<List<ProductResponse>> FilterProductsAsync(ProductFilterRequest filterRequest)
        {
            // Lấy danh sách sản phẩm sau khi lọc
            var filteredProducts = await _productRepository.FilterProductsAsync(filterRequest);
            return _mapper.Map<List<ProductResponse>>(filteredProducts);

        }

        public async Task<ProductResponse?> GetByNameAsync(string name)
        {
            // Lấy sản phẩm theo tên
            var product = await _productRepository.GetByNameAsync(name);
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse?> GetBySlugAsync(string slug)
        {
            // Lấy sản phẩm theo slug
            var product = await _productRepository.GetBySlugAsync(slug);
            return _mapper.Map<ProductResponse>(product);

        }

        public async Task<ProductResponse?> GetProductWithDetailsAsync(int id)
        {
            var product = await _productRepository.GetProductWithDetailsAsync(id);
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<List<ProductResponse>> GetFeaturedProductsAsync()
        {
            var products = await _productRepository.GetFeaturedProductsAsync();
            return _mapper.Map<List<ProductResponse>>(products);
        }
    }
}
