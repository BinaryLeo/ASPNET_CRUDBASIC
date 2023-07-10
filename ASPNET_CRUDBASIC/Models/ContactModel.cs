namespace ASPNET_CRUDBASIC.Models
{
    using System.ComponentModel.DataAnnotations;
    public class ContactModel

    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "The username is required.")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "The e-mail is required.")]
        public string UserEmail { get; set; } = string.Empty;
        [Required(ErrorMessage = "The phone number is required.")]
        public string UserPhone { get; set; } = string.Empty;


    }

    
}
