using Futureadvance.Core.Models;
using Futureadvance.MVC.Models;
using Futureadvance.MVC.Models.DTO;
using Futureadvance.MVC.Services;
using Futureadvance.MVC.Services.Iservices;
using static Futureadvance.Utility.StaticDetails;

namespace Booky.MVC.Services
{
    public class UserService :BaseService, IUserservice
    {
        private readonly IHttpClientFactory _httpClient;
        private string ApiURL;
        public UserService(IHttpClientFactory httpClient,IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            ApiURL = configuration.GetValue<string>("ServicesUrl:Apiurl");
        }

     
        public async Task<T> RegisterAsync<T>(RegisteritionRequestDto registerDTO)
        {
            var request = new ApiRequest();
            request.data = registerDTO;
            request.ApiType = ApiType.POST;
            request.Url = ApiURL + "/api/userAuth/Register";
            return await sendAsunc<T>(request);
        }

        public async Task<T> DeleteuserAsync<T>(int id)
        {
            var request = new ApiRequest();
            request.ApiType = ApiType.POST;
            request.Url = ApiURL + "/api/userAuth/Deleteuser?userId=" + id;
            return await sendAsunc<T>(request);
        }

        public async Task<T> getAllAsync<T>()
        {

            var request = new ApiRequest();

            request.ApiType = ApiType.GET;
            request.Url = "https://localhost:7227/api/Estate/";
            return await sendAsunc<T>(request);
        }

        public async Task<T> getbyidAsync<T>(int id)
        {
            var request = new ApiRequest();

            request.ApiType = ApiType.GET;
            request.Url = "https://localhost:7227/api/Estate/"+id;
            return await sendAsunc<T>(request);
        }

        public async Task<T> loginAsync<T>(LoginRequestDTO loginDto)
        {
            var request = new ApiRequest();
            request.data = loginDto;
            request.ApiType = ApiType.POST;
            request.Url = ApiURL + "/api/userAuth/Login";
            return await sendAsunc<T>(request);
        }

      

        public async Task<T> UpdateuserAsync<T>(RegisteritionRequestDto registerDTO)
        {
            var request = new ApiRequest();
            request.data = registerDTO;
            request.ApiType = ApiType.POST;
            request.Url = ApiURL + "/api/userAuth/updateuser";
            return await sendAsunc<T>(request);
        }
    }
}
