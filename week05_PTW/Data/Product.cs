namespace week05_PTW.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public int Price { get; set; }
        public int? CategoryId { get; set; }

        public string? Image { get; set; }
        public int? Quantity { get; set; }
        public string? ImageHover { get; set; }
        public int? PriceSale { get; set; }
     
        public Categories? Categories { get; set; }
     
       
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
