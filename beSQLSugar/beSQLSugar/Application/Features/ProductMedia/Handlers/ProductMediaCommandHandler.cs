using beSQLSugar.Application.Dto.response.ProductMedia;
using beSQLSugar.Application.Features.ProductMedia.Commands;
using beSQLSugar.Application.Services.ProductMediaServices;
using MediatR;

namespace beSQLSugar.Application.Features.ProductMedia.Handlers
{
    public class ProductMediaCommandHandler :
        IRequestHandler<AddMediaCommand, ProductMediaResponse>,
        IRequestHandler<UpdateMediaCommand, ProductMediaResponse>,
        IRequestHandler<DeleteMediaCommand, bool>
    {
        private readonly IProductMediaService _productMediaService;
        public ProductMediaCommandHandler(IProductMediaService productMediaService)
        {
            _productMediaService = productMediaService;
        }
        public async Task<ProductMediaResponse> Handle(AddMediaCommand request, CancellationToken cancellationToken)
        {

            var mediaResponse = await _productMediaService.AddMediaAsync(request.ProductId, request.Request!);
            if (mediaResponse == null)
            {
                throw new Exception("Failed to add product media");
            }
            return mediaResponse;
        }

        public async Task<bool> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            return await _productMediaService.DeleteMediaAsync(request.MediaId);
        }

        public async Task<ProductMediaResponse> Handle(UpdateMediaCommand request, CancellationToken cancellationToken)
        {
            var mediaResponse = await _productMediaService.UpdateMediaAsync(request.ProductId, request.MediaId, request.Request!);
            if (mediaResponse == null)
            {
                throw new Exception("Failed to update product media");
            }
            return mediaResponse;
        }
    }
}
