using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.Features.HeroSection.Commands;
using beSQLSugar.Application.ServiceInterfaces;
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
            var result = await _heroSectionService.AddAsync(request.Request);
            if (result is null)
                throw new InvalidOperationException("Failed to create HeroSection.");
            return result;
        }

        public async Task<HeroSectionResponse> Handle(UpdateHeroSectionCommand request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.UpdateAsync(request.Id, request.Request);
        }

        public async Task<bool> Handle(DeleteHeroSectionCommand request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.DeleteAsync(request.Id);
        }
    }
}
