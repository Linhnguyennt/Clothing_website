namespace week05_PTW.Data
{
    public class OrderProduct
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public Product? Product { get; set; }

		public int? OrderId { get; set; }
		public Order? Orders { get; set; }

		public int? Quantity { get; set; }
		public decimal? Price { get; set; }
	}
}
