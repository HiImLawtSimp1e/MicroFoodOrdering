namespace MangoFood.UI.Models.DTOs.Common
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
