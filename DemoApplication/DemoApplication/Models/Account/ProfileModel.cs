namespace DemoApplication.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Metadata.Attributes;

    public partial class ProfileModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public virtual String Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [Textbox(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}