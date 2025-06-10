using AutoMapper;
using PhoneBookApp.DTOs;
using PhoneBookApp.Domain.Models;

namespace PhoneBookApp.Application.Profiles
{
    internal class PhoneNumberProfile : Profile
    {
        public PhoneNumberProfile()
        {
            CreateMap<PhoneNumber, PhoneNumberDto>();
            CreateMap<PhoneNumberCreateDto, PhoneNumber>();
            CreateMap<PhoneNumberDto, PhoneNumber>(); 
        }
    }
}
