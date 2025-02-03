using Order.Contract.Dtos;

namespace Order.Contract.Response.Query.CustomerOrders
{
    public class GetCustomerOrderByIdQueryResponse
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
