using beSQLSugar.Application.Dto.request.Partner;
using beSQLSugar.Application.Dto.response.Partner;
using MediatR;

namespace beSQLSugar.Application.Features.Partner.Commands
{
    public class UpdatePartnerCommand : IRequest<PartnerResponse>
    {
        public int Id { get; set; }
        public PartnerRequest Request { get; set; } 
        public UpdatePartnerCommand(int id, PartnerRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}
