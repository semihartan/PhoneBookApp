namespace PhoneBookApp.DTOs
{ 
    public class ContactReadDto
    {
        public int ContactID { get; set; } 
        public string Name { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<PhoneNumberDto> PhoneNumbers { get; set; } = new List<PhoneNumberDto>();
        public ICollection<EmailDto> Emails { get; set; } = new List<EmailDto>();
    }
}
