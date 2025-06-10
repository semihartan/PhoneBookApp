using AutoMapper;
using PhoneBookApp.DTOs;
using PhoneBookApp.Domain.Models;

namespace PhoneBookApp.Application.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        { 
            CreateMap<Contact, ContactReadDto>();
            CreateMap<ContactCreateDto, Contact>();
            CreateMap<ContactUpdateDto, Contact>();

            // CreateMap<ContactUpdateDto, Contact>()
            //   .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers))
            //   .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));
        }
    }
}
