using AutoMapper;
using beSQLSugar.Application.Dto.request.Contact;
using beSQLSugar.Application.Dto.response.Contact;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactResponse>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
            CreateMap<ContactRequest, Contact>();
        }
    }
}
