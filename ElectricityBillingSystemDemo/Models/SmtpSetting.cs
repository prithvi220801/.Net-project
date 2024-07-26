using System.ComponentModel.DataAnnotations;

namespace ElectricityBillingSystemDemo.Models
{
    public class SmtpSetting
    {
        [Required(ErrorMessage = "SMTP server address is required")]
        public string? Server { get; set; }

        [Required(ErrorMessage = "SMTP port number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Port number must be a positive integer")]
        public int Port { get; set; }

        [Required(ErrorMessage = "SMTP username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "SMTP password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Sender email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? SenderEmail { get; set; }
    }
}
