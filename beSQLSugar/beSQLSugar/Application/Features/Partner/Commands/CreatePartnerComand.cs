using beSQLSugar.Application.Dto.request.Partner;
using beSQLSugar.Application.Dto.response.Partner;
using MediatR;

namespace beSQLSugar.Application.Features.Partner.Commands
{
    public class CreatePartnerComand : IRequest<PartnerResponse>
    {
        public PartnerRequest Request { get; set; }
        public CreatePartnerComand(PartnerRequest _request)
        {
            Request = _request;
        }

    }
}
