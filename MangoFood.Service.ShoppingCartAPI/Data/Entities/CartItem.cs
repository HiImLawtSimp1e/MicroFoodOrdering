using System.Text.Json.Serialization;

namespace MangoFood.Service.ShoppingCartAPI.Data.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        [JsonIgnore]
        public Cart Cart { get; set; }
    }
}
