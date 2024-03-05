using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class Counter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CounterId { get; set; }
        public string PosName { get; set; } = string.Empty;
        public virtual IList<Customer>? Customers { get; set; }
    }
}
