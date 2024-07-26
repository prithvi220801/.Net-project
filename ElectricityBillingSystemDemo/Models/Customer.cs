using System.ComponentModel.DataAnnotations;

namespace ElectricityBillingSystemDemo.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage ="Enter the customer Name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage ="Enter the Address")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Enter the Contact Number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact Number should be a 10 digit Number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage ="Enter the email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

       
    }
}
