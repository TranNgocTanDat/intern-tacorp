using beSQLSugar.Application.Dto.request.ProductMedia;
using beSQLSugar.Application.Dto.response.ProductMedia;

namespace beSQLSugar.Application.Services.ProductMediaServices
{
    public interface IProductMediaService
    {
        Task<List<ProductMediaResponse>> GetAllMediaAsync();
        Task<List<ProductMediaResponse>> GetMediaByProductIdAsync(int productId);
        Task<ProductMediaResponse?> AddMediaAsync(int productId, ProductMediaRequest media);
        Task<ProductMediaResponse?> UpdateMediaAsync(int productId, int mediaId, ProductMediaRequest media);
        Task<List<ProductMediaResponse>> FilterProductMedia(ProductMediaFilterRequest request);
        Task<bool> DeleteMediaAsync(int id);
    }
}
