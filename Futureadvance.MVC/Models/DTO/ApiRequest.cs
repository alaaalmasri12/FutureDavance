
using static Futureadvance.Utility.StaticDetails;
namespace Futureadvance.MVC.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object data { get; set; }
    }
}
