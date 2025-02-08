namespace MangoFood.Service.OrderAPI.Models.Common
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = String.Empty;
    }
}
