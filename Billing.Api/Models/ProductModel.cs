namespace Billing.Api.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
    }
}