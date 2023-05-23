namespace SievalAssignment.Models
{
    /// <summary>
    /// Sample article
    /// </summary>
    // TODO if this application would be used in the real world, verify that ID, SKU, name and price are non-nullable (this comment was marked as a TODO note to make this easily findable in a code repository
    public class Article
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public Article(int id, string sku, string name, double price)
        {
            Id = id;
            Sku = sku;
            Name = name;
            Price = price;
        }
    }
}
