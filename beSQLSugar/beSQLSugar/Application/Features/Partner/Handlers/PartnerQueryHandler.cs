using beSQLSugar.Application.Dto.response.Partner;
using beSQLSugar.Application.Features.Partner.Queries;
using beSQLSugar.Application.Services.Partners;
using MediatR;

namespace beSQLSugar.Application.Features.Partner.Handlers
{
    public class PartnerQueryHandler :
        IRequestHandler<GetAllPartnerQuery, List<PartnerResponse>>,
        IRequestHandler<GetPartnerById, PartnerResponse>,
        IRequestHandler<FilterPartnerQuery, List<PartnerResponse>>
    {
        private readonly IPartnerService _partnerService;
        public PartnerQueryHandler(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        public async Task<List<PartnerResponse>> Handle(GetAllPartnerQuery request, CancellationToken cancellationToken)
        {
            return await _partnerService.GetAllPartnersAsync();
        }

        public async Task<PartnerResponse> Handle(GetPartnerById request, CancellationToken cancellationToken)
        {
            return await _partnerService.GetPartnerByIdAsync(request.Id);
        }

        public async Task<List<PartnerResponse>> Handle(FilterPartnerQuery request, CancellationToken cancellationToken)
        {
            return await _partnerService.FilterPartnersAsync(request.Request!);
        }
    }
}
