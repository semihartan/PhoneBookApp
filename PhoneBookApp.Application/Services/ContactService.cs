using AutoMapper;
using PhoneBookApp.Application.Repositories;
using PhoneBookApp.Domain.Models;
using PhoneBookApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactReadDto>> GetAllContactsAsync()
        {
            var contacts = await _unitOfWork.Contacts.GetAllContactsWithDetailsAsync();
            return _mapper.Map<IEnumerable<ContactReadDto>>(contacts);
        }

        public async Task<ContactReadDto?> GetContactByIdAsync(int id)
        {
            var contact = await _unitOfWork.Contacts.GetContactWithDetailsAsync(id);
            if (contact == null) return null;
            return _mapper.Map<ContactReadDto>(contact);
        }

        public async Task<ContactReadDto> CreateContactAsync(ContactCreateDto contactCreateDto)
        {
            var contact = _mapper.Map<Contact>(contactCreateDto);
            await _unitOfWork.Contacts.AddAsync(contact);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ContactReadDto>(contact);
        }

        public async Task<bool> UpdateContactAsync(int id, ContactUpdateDto contactUpdateDto)
        {
            var existingContact = await _unitOfWork.Contacts.GetContactWithDetailsAsync(id);
            if (existingContact == null)
            {
                return false;
            }

            _mapper.Map(contactUpdateDto, existingContact);

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _unitOfWork.Contacts.GetByIdAsync(id);
            if (contact == null)
            {
                return false;
            }
            _unitOfWork.Contacts.Delete(contact);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
