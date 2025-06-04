using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PhoneBookApp.Domain.Models
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailID { get; set; }

        public int? ContactID { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Address { get; set; }

        [Required]
        public EmailType Type { get; set; }

        public bool IsPrimary { get; set; } = false;

        [ForeignKey("ContactID")]
        [JsonIgnore]
        public virtual Contact? Contact { get; set; }
    }
}
