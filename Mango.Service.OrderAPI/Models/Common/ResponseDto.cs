namespace MangoFood.Service.OrderAPI.Models.Common
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
