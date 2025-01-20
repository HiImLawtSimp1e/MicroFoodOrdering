using MangoFood.UI.Services.IService;
using static MangoFood.UI.Utilities.SD;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using MangoFood.UI.Models.DTOs.Common;

namespace MangoFood.UI.Services.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new()
                {
                    RequestUri = new Uri(requestDto.Url),
                    Method = requestDto.ApiType switch
                    {
                        ApiType.POST => HttpMethod.Post,
                        ApiType.DELETE => HttpMethod.Delete,
                        ApiType.PUT => HttpMethod.Put,
                        _ => HttpMethod.Get
                    }
                };

                message.Headers.Add("Accept", "application/json");

                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(requestDto.Data),
                        Encoding.UTF8,
                        "application/json");
                }

                var apiResponse = await client.SendAsync(message);

                // Xử lý các trạng thái HTTP
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseDto>(apiContent) ?? new ResponseDto
                        {
                            Success = true,
                            Message = "Response deserialized but is null."
                        };

                    case HttpStatusCode.Forbidden:
                        return new ResponseDto { Success = false, Message = "Access Denied" };

                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto { Success = false, Message = "Unauthorized" };

                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto { Success = false, Message = "Internal Server Error" };

                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.NotFound:
                        var errorContent = await apiResponse.Content.ReadAsStringAsync();
                        var errorResponseDto = JsonConvert.DeserializeObject<ResponseDto>(errorContent);
                        if (errorResponseDto != null)
                        {
                            return errorResponseDto;
                        }
                        return new ResponseDto
                        {
                            Success = false,
                            Message = "Error response could not be deserialized."
                        };

                    default:
                        var defaultContent = await apiResponse.Content.ReadAsStringAsync();
                        return new ResponseDto
                        {
                            Success = false,
                            Message = $"Unhandled status code: {apiResponse.StatusCode}",
                            Data = defaultContent
                        };
                }
            }
            catch (Exception ex)
            {
                // Trả về ResponseDto chứa thông tin lỗi
                return new ResponseDto
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
