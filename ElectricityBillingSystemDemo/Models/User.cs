using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricityBillingSystemDemo.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName {  get; set; }
       
        public string Password {  get; set; }

        [Required(ErrorMessage = "Enter the email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Role(Role/ customer) is required")]
        public string? Role {  get; set; }
        public string? PasswordResetOTP { get; set; }
        public DateTime? PasswordResetOTPCreationTime { get; set; }
        public enum User_Role
        {
            Admin,
            Customer
        }
       
        
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
