using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using beSQLSugar.Application.Features.HeroSectionProduct.Commands;
using beSQLSugar.Application.Services.HeroSectionProductServices;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSectionProduct.Handlers
{
    public class HeroSectionProductCommandHandler :
        IRequestHandler<CreateHeroSectionProductCommand, HeroSectionProductResponse>,
        IRequestHandler<DeleteHeroSectionProductCommand, bool>,
        IRequestHandler<UpdateHeroSectionProductCommand, HeroSectionProductResponse>
    {
        private readonly IHeroSectionProductService _heroSectionProductService;
        public HeroSectionProductCommandHandler(IHeroSectionProductService heroSectionProductService)
        {
            _heroSectionProductService = heroSectionProductService;
        }
        public async Task<HeroSectionProductResponse> Handle(CreateHeroSectionProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _heroSectionProductService.AddAsync(request.Request!, request.User);

            if (response == null)
            {
                throw new Exception("Failed to add hero section product");
            }   
            return response;
        }

        public async Task<bool> Handle(DeleteHeroSectionProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _heroSectionProductService.GetByIdAsync(request.HeroSectionProductId);
            if (existing == null) throw new Exception("Hero section product not found");
            return await _heroSectionProductService.DeleteAsync(request.HeroSectionProductId);
        }

        public async Task<HeroSectionProductResponse> Handle(UpdateHeroSectionProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _heroSectionProductService.GetByHeroSectionIdAsync(request.Id);
            if (existing == null) throw new Exception("Hero section product not found");
            var response = await _heroSectionProductService.UpdateAsync(request.Id, request.Request!, request.User);
            if (response == null)
            {
                throw new Exception("Failed to update hero section product");
            }
            return response;
        }
    }
}
