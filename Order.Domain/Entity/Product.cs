using Order.Domain.Entity.Base;

namespace Order.Domain.Entity
{
    public class Product : BaseEntity
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public ICollection<CustomerOrder> Orders { get; set; } = [];

    }
}
