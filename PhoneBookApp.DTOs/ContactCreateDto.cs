using System.ComponentModel.DataAnnotations;

namespace PhoneBookApp.DTOs
{ 
    public class ContactCreateDto
    {
        [Required(ErrorMessage = "Contact name is required.")]
        [StringLength(100, ErrorMessage = "Contact name cannot be longer that 100 chars.")]
        public string Name { get; set; } 

        public string? Notes { get; set; }
         
        public List<PhoneNumberCreateDto> PhoneNumbers { get; set; } = new List<PhoneNumberCreateDto>();
        public List<EmailCreateDto> Emails { get; set; } = new List<EmailCreateDto>();
    }
}
