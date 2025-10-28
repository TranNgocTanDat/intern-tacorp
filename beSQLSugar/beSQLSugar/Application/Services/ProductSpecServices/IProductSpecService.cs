using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Application.Dto.response.ProductMedia;
using beSQLSugar.Application.Dto.response.ProductSpec;

namespace beSQLSugar.Application.Services.ProductSpecServivces
{
    public interface IProductSpecService
    {
        Task<List<ProductSpecResponse>> GetAllSpecAsync();
        Task<List<ProductSpecResponse>> GetSpecsByProductIdAsync(int productId);
        Task<ProductSpecResponse?> AddSpecAsync(int productId, ProductSpecRequest spec);
        Task<ProductSpecResponse?> UpdateSpecAsync(int productId,int specId, ProductSpecRequest spec);
        Task<List<ProductSpecResponse>> FilterProductSpec(ProductSpecFilterRequest request);
        Task<bool> DeleteSpecAsync(int id);
    }
}
