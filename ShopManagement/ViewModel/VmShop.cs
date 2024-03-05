using ShopManagement.Models;

namespace ShopManagement.ViewModel
{
    public class VmShop
    {
        public int CounterId { get; set; }
        public string PosName { get; set; } = string.Empty;

        public int CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        public string[]? ProductName { get; set; }
        public string[]? Photo { get; set; }
        public decimal[]? UnitPrice { get; set; }
        public decimal[]? Quantity { get; set; }
        public decimal[]? TotalPrice { get; set; }

        public List<Customer>? Customers { get; set; }
        public IList<VmProduct> Products { get; set; } = new List<VmProduct>();
        public class VmProduct
        {
            public int ProductId { get; set; }
            public int CustomerId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public string? Photo { get; set; }
            public int UnitPrice { get; set; }
            public int Quantity { get; set; }
            public int TotalPrice { get; set; }
        }
    }
}
