using PhoneBookApp.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhoneBookApp.DTOs
{ 
    public class EmailDto 
    {
        public int EmailID { get; set; } 

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Address { get; set; }

        [Required]
        public EmailType Type { get; set; }

        public bool IsPrimary { get; set; }
    }
}
