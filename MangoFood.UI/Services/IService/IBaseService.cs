using MangoFood.UI.Models.DTOs.Common;

namespace MangoFood.UI.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
