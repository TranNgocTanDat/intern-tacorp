using beSQLSugar.Application.Dto.response.Partner;
using beSQLSugar.Application.Features.Partner.Commands;
using beSQLSugar.Application.Services.Partners;
using MediatR;

namespace beSQLSugar.Application.Features.Partner.Handlers
{
    public class PartnerCommandHandler :
        IRequestHandler<CreatePartnerComand, PartnerResponse>,
        IRequestHandler<UpdatePartnerCommand, PartnerResponse>,
        IRequestHandler<DeletePartnerCommand, bool>
    {
        private readonly IPartnerService _partnerService;
        public PartnerCommandHandler(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        public async Task<PartnerResponse> Handle(CreatePartnerComand request, CancellationToken cancellationToken)
        {
            var created = await _partnerService.CreatePartnerAsync(request.Request);
            return created;
        }
        public async Task<PartnerResponse> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
        {
            var updated = await _partnerService.UpdatePartnerAsync(request.Id, request.Request);
            return updated;
        }
        public async Task<bool> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _partnerService.DeletePartnerAsync(request.Id);
            return deleted;
        }

    }
}
