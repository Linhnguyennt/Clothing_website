namespace week05_PTW.Data
{
    public class Categories
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        
        public ICollection<Product>? Products { get; set; }
    }
}
