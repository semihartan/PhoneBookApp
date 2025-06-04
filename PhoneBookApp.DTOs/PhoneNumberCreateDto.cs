using PhoneBookApp.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhoneBookApp.DTOs
{
    public class PhoneNumberCreateDto 
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 chars.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Number type is required.")]
        [EnumDataType(typeof(PhoneNumberType), ErrorMessage = "Invalid phone number type.")]
        public PhoneNumberType Type { get; set; }

        public bool IsPrimary { get; set; }
    }
}
