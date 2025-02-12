using DsaGame.web.Models;
using DsaGame.web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static DsaGame.web.Utility.SD;

namespace DsaGame.web.Service
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

        public async Task<ResponesDto> SendAsync(RequestDto requestDto, bool withBearer)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("ComGroup");
                HttpRequestMessage message = new();
                message.Headers.Add("Accsepts", "application/json");
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

                HttpResponseMessage? apiRsponse = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiRsponse = await httpClient.SendAsync(message);

                switch (apiRsponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponesDto() { IsSuccess = false, Message = "NotFound" };
                    case HttpStatusCode.Forbidden:
                        return new ResponesDto() { IsSuccess = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponesDto() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponesDto() { IsSuccess = false, Message = "InternalServerError" };
                    default:
                        var apiContent = await apiRsponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponesDto>(apiContent);
                        return apiResponseDto;

                }
            }
            catch (Exception ex)
            {
                var dto = new ResponesDto()
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };

                return dto;
            }
        }
    }
}
