using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricityBillingSystemDemo.Models
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillId { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [Required]
        public DateTime BillingDate { get; set; }

        [Required]
        public int UnitsConsumed { get; set; }

        [Required]
        public decimal BillAmount { get; set; }
        [Required]
        public DateTime DueDate { get; set; }

        //navigation properties-for relationships

        //public virtual Customer Customer { get; set; }//relates bill to Customer
       
    }
}
