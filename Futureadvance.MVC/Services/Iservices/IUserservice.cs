

using Futureadvance.MVC.Models.DTO;

namespace Futureadvance.MVC.Services.Iservices
{
    public interface IUserservice
    {
        Task<T> getAllAsync<T>();
        Task<T> loginAsync<T>(LoginRequestDTO loginDto);
        Task<T> getbyidAsync<T>(int id);
        Task<T> RegisterAsync<T>(RegisteritionRequestDto registerDTO);
        Task<T> UpdateuserAsync<T>(RegisteritionRequestDto registerDTO);
        Task<T> DeleteuserAsync<T>(int id);

    }
}
