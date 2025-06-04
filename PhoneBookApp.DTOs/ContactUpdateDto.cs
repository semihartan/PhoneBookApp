using System.ComponentModel.DataAnnotations;

namespace PhoneBookApp.DTOs
{ 
    public class ContactUpdateDto
    {
        [Required(ErrorMessage = "Contact name is required.")]
        [StringLength(100, ErrorMessage = "Contact name cannot be longer that 100 chars.")]
        public string Name { get; set; } 

        public string? Notes { get; set; }
         
        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new List<PhoneNumberDto>();  
        public List<EmailDto> Emails { get; set; } = new List<EmailDto>();
    }
}
