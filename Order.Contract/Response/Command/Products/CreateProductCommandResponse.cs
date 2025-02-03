namespace Order.Contract.Response.Command.Products
{
    public class CreateProductCommandResponse
    {
        public Guid Id { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
