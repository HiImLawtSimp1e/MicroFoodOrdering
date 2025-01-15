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

		public BaseService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
		{
			try
			{
				HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
				HttpRequestMessage message = new();
				message.Headers.Add("Accept", "application/json");
				//token
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
						return new() { Success = false, Message = "Not Found" };
					case HttpStatusCode.Forbidden:
						return new() { Success = false, Message = "Access Denied" };
					case HttpStatusCode.Unauthorized:
						return new() { Success = false, Message = "Unauthorized" };
					case HttpStatusCode.BadRequest:
						return new() { Success = false, Message = "Bad Request" };
					case HttpStatusCode.InternalServerError:
						return new() { Success = false, Message = "Internal Server Error" };
					default:
						var apiContent = await apiResponse.Content.ReadAsStringAsync();
						var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
						return apiResponseDto;
				}
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
