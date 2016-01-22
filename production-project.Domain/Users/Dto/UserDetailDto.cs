namespace production_project.Domain.Users.Dto
{
    using System.ComponentModel.DataAnnotations;

    public class UserDetailDto
    {
        public UserDetailDto() { }
        public UserDetailDto(string id, string name, string telephone, string mobile, string email, bool emailNotification, bool smsNotification)
        {
            Id = id;
            Name = name;
            Telephone = telephone;
            Mobile = mobile;
            Email = email;
            EmailNotification = emailNotification;
            SmsNotification = smsNotification;
        }

        public string Id { get; set; }
        [Required]
        [StringLength(254, ErrorMessage = "Name length should be less than 254 characters.")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
       
        [Required]
        [Phone]
        [StringLength(11, ErrorMessage = "Telephone number should be less than 12 digits.")]
        public string Telephone { get; set; }

        [Required]
        [Phone]
        [StringLength(11, ErrorMessage = "Mobile number should be less than 12 digits.")]
        public string Mobile { get; set; }

        [Required]
        [StringLength(254, ErrorMessage = "Email length should be less than 254 characters.")]
        public string Email { get; set; }

        [Required]
        public bool EmailNotification { get; set; }

        [Required]
        public bool SmsNotification { get; set; }
    }
}
