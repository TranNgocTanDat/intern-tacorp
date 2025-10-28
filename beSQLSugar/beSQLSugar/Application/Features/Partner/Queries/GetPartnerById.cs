using beSQLSugar.Application.Dto.response.Partner;
using MediatR;

namespace beSQLSugar.Application.Features.Partner.Queries
{
    public class GetPartnerById : IRequest<PartnerResponse>
    {
        public int Id { get; set; }
        public GetPartnerById(int id)
        {
            Id = id;
        }
    }
}
