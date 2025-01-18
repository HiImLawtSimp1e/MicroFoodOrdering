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
		public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
		{
			try
			{
				HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
				HttpRequestMessage message = new();
				message.Headers.Add("Accept", "application/json");
                //token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }
                message.RequestUri = new Uri(requestDto.Url);

				if (requestDto.Data != null)
				{
					message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
				}

				HttpResponseMessage? apiResponse = null;
				switch (requestDto.ApiType)
				{
					case ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
					case ApiType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
					case ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
					default:
						message.Method = HttpMethod.Get;
						break;
				}

				apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        var errorContent = await apiResponse.Content.ReadAsStringAsync();
                        try
                        {
                            var errorResponseDto = JsonConvert.DeserializeObject<ResponseDto>(errorContent);
                            if (errorResponseDto != null)
                            {
                                return errorResponseDto;
                            }
                        }
                        catch
                        {
                            return new ResponseDto
                            {
                                Success = false,
                                Message = apiResponse.StatusCode.ToString()
                            };
                        }
                        break;

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }

                // Nếu không khớp nhánh nào, trả về ResponseDto mặc định
                return new ResponseDto
                {
                    Success = false,
                    Message = "Unhandled status code."
                };
            }
			catch (Exception ex)
			{
				var dto = new ResponseDto
				{
					Message = ex.Message.ToString(),
					Success = false
				};
				return dto;
			}
		}
	}
}
