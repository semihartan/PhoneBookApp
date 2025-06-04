using PhoneBookApp.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhoneBookApp.DTOs
{ 
    public class PhoneNumberDto 
    {
        public int PhoneNumberID { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Number { get; set; }

        [Required]
        public PhoneNumberType Type { get; set; }

        public bool IsPrimary { get; set; }
    }
}
