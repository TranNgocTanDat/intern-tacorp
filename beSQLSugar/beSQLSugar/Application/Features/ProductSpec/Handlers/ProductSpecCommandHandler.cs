using beSQLSugar.Application.Dto.response.ProductSpec;
using beSQLSugar.Application.Features.ProductSpec.Commands;
using beSQLSugar.Application.Services.ProductSpecServivces;
using MediatR;

namespace beSQLSugar.Application.Features.ProductSpec.Handlers
{
    public class ProductSpecCommandHandler : IRequestHandler<AddSpecCommand, ProductSpecResponse>,
        IRequestHandler<DeleteSpecCommand, bool>,
        IRequestHandler<UpdateSpecCommand, ProductSpecResponse>
    {
        private readonly IProductSpecService _productSpecService;
        public ProductSpecCommandHandler(IProductSpecService productSpecService)
        {
            _productSpecService = productSpecService;
        }

        public async Task<ProductSpecResponse> Handle(AddSpecCommand request, CancellationToken cancellationToken)
        {
            var specResponse = await _productSpecService.AddSpecAsync(request.ProductId,request.Request!);
            if (specResponse == null)
            {
                throw new Exception("Failed to add product spec");
            }
            return specResponse;
        }

        public async Task<bool> Handle(DeleteSpecCommand request, CancellationToken cancellationToken)
        {
            return await _productSpecService.DeleteSpecAsync(request.SpecId);
        }

        public async Task<ProductSpecResponse> Handle(UpdateSpecCommand request, CancellationToken cancellationToken)
        {
            var specResponse = await _productSpecService.UpdateSpecAsync(request.ProductId, request.SpecId, request.Request!);
            if (specResponse == null)
            {
                throw new Exception("Failed to update product spec");
            }
            return specResponse;
        }
    }

}
