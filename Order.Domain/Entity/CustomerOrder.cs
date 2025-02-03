using Order.Domain.Entity.Base;

namespace Order.Domain.Entity
{
    public class CustomerOrder : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<Product> Products { get; set; } = [];
    }
}
