using PhoneBookApp.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhoneBookApp.DTOs
{
    public class EmailCreateDto 
    {
        [Required(ErrorMessage = "Email address is required.")]
        [StringLength(100, ErrorMessage = "Email address cannot be longer than 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email type is required.")]
        [EnumDataType(typeof(EmailType), ErrorMessage = "Invalid email type.")]
        public EmailType Type { get; set; }

        public bool IsPrimary { get; set; }
    }
}
