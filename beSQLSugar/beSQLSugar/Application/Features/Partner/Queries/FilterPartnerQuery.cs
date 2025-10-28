using beSQLSugar.Application.Dto.request.Partner;
using beSQLSugar.Application.Dto.response.Partner;
using MediatR;

namespace beSQLSugar.Application.Features.Partner.Queries
{
    public class FilterPartnerQuery : IRequest<List<PartnerResponse>>
    {
        public PartnerFilterRequest? Request { get; set; }
        public FilterPartnerQuery(PartnerFilterRequest? request)
        {
            Request = request;
        }
    }
}
