using beSQLSugar.Application.Dto.response.Partner;
using MediatR;

namespace beSQLSugar.Application.Features.Partner.Queries
{
    public class GetAllPartnerQuery : IRequest<List<PartnerResponse>>
    {
        public GetAllPartnerQuery() { }
    }
}
