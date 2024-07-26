using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace ElectricityBillingSystemDemo.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        //[Required]
        //public int CustomerId { get; set; }
        [ForeignKey("Bill")]
        public int BillId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public decimal Amount { get; set; }

        public string PaymentMode { get; set; }

        public string status { get; set; }


        //public Customer customer { get; set; }//navigation property
        //public virtual Bill Bill { get; set; }
  
        //public virtual Customer Customer { get; set; }


    }
}
