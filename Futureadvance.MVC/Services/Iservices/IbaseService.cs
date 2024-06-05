using Futureadvance.MVC.Models;
using Futureadvance.Core.Models;

namespace Futureadvance.MVC.Services.Iservices
{
    public interface IbaseService
    {
        public Apiresponse ResponseModel { get; set; }
        Task<T> sendAsunc<T>(ApiRequest apiRequest);
    }
}
