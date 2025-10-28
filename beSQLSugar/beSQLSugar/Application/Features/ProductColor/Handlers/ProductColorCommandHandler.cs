using beSQLSugar.Application.Dto.response.ProductColor;
using beSQLSugar.Application.Features.ProductColor.Commands;
using beSQLSugar.Application.Services.ProductColorServices;
using MediatR;

namespace beSQLSugar.Application.Features.ProductColor.Handlers
{
    public class ProductColorCommandHandler :
        IRequestHandler<CreateProductColorCommand, ProductColorResponse>,
        IRequestHandler<UpdateProductColorCommand, ProductColorResponse>,
        IRequestHandler<DeleteProductColorCommand, bool>
    {
        private readonly IProductColorService _productColorService;
        public ProductColorCommandHandler(IProductColorService productColorService)
        {
            _productColorService = productColorService;
        }
        public async Task<ProductColorResponse> Handle(CreateProductColorCommand request, CancellationToken cancellationToken)
        {
            return await _productColorService.CreateAsync(request.Request);
        }
        
        public async Task<ProductColorResponse> Handle(UpdateProductColorCommand request, CancellationToken cancellationToken)
        {
            return await _productColorService.UpdateAsync(request.Id, request.Request!);
        }

        public async Task<bool> Handle(DeleteProductColorCommand request, CancellationToken cancellationToken)
        {
            return await _productColorService.DeleteAsync(request.Id);
        }
    }
}
