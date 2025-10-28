using beSQLSugar.Application.Dto.response.HeroSection;
using beSQLSugar.Application.Features.HeroSection.Commands;
using beSQLSugar.Application.Services.HeroSectionServices;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Handlers
{
    public class HeroSectionCommandHandler : 
        IRequestHandler<CreateHeroSectionCommand, HeroSectionResponse>,
        IRequestHandler<UpdateHeroSectionCommand, HeroSectionResponse>,
        IRequestHandler<DeleteHeroSectionCommand, bool>
    {
        private readonly IHeroSectionService _heroSectionService;

        public HeroSectionCommandHandler(IHeroSectionService heroSectionService)
        {
            _heroSectionService = heroSectionService;
        }

        public async Task<HeroSectionResponse> Handle(CreateHeroSectionCommand request, CancellationToken cancellationToken)
        {
            var result = await _heroSectionService.AddAsync(request.Request, request.User);
            if (result is null)
                throw new InvalidOperationException("Failed to create HeroSection.");
            return result;
        }

        public async Task<HeroSectionResponse> Handle(UpdateHeroSectionCommand request, CancellationToken cancellationToken)
        {
            var result = await _heroSectionService.UpdateAsync(request.Id, request.Request!, request.User);
            if (result is null)
                throw new InvalidOperationException("Failed to update HeroSection.");
            return result;
        }

        public async Task<bool> Handle(DeleteHeroSectionCommand request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.DeleteAsync(request.Id);
        }
    }
}
