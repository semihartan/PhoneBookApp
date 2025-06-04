using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PhoneBookApp.Domain.Models
{
    public class PhoneNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int PhoneNumberID { get; set; }

        public int? ContactID { get; set; }

        [Required]
        [StringLength(20)]
        public string Number { get; set; } // E.164 format

        [Required]
        public PhoneNumberType Type { get; set; }

        public bool IsPrimary { get; set; } = false;

        [ForeignKey("ContactID")]
        [JsonIgnore]
        public virtual Contact? Contact { get; set; }    
    }
}
