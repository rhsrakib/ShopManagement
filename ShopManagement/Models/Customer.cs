using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        [ForeignKey("Counter")]
        public int CounterId { get; set; }
        public Counter? Counter { get; set; }

        public IList<Product> Products { get; set; } = new List<Product>();
    }
}
