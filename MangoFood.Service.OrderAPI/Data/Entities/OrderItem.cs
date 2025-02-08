using System.Text.Json.Serialization;

namespace MangoFood.Service.OrderAPI.Data.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
