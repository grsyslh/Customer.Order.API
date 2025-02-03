using Order.Domain.Entity.Base;

namespace Order.Domain.Entity
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public ICollection<CustomerOrder> Orders { get; set; } = new List<CustomerOrder>();

    }
}