using AutoMapper;
using PhoneBookApp.DTOs;
using PhoneBookApp.Domain.Models;

namespace PhoneBookApp.Application.Profiles
{
    internal class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<Email, EmailDto>();
            CreateMap<EmailCreateDto, Email>();
            CreateMap<EmailDto, Email>();
        }
    }
}
