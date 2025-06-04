using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 
using PhoneBookApp.Application.Services;
using PhoneBookApp.Domain.Models;
using PhoneBookApp.DTOs;

namespace PhoneBookApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService; 

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactReadDto>>> GetContacts()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactReadDto>> GetContact(int id)
        {
            var contactDto = await _contactService.GetContactByIdAsync(id);
            if (contactDto == null)
            {
                return NotFound();
            }
            return Ok(contactDto);
        }

        [HttpPost]
        public async Task<ActionResult<ContactReadDto>> PostContact(ContactCreateDto contactCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdContactDto = await _contactService.CreateContactAsync(contactCreateDto);
            return CreatedAtAction(nameof(GetContact), new { id = createdContactDto.ContactID }, createdContactDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, ContactUpdateDto contactUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _contactService.UpdateContactAsync(id, contactUpdateDto);
            if (!success)
            {
                // This could mean NotFound or some other update failure if service distinguishes
                return NotFound(); // Or a more specific error from the service
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var success = await _contactService.DeleteContactAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

