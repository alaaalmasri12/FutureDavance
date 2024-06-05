
namespace Futureadvance.API.Customizeresponses
{
    public class Response
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public Response(int statuscode,string? message=null)
        {
            this.StatusCode = statuscode;
            this.Message = message?? DefaultMESSAGE(statuscode);
        }

        private string? DefaultMESSAGE(int? statuscode)
        {
            return statuscode switch
            {
                400 => "Bad Request",
                404 => "Not Found"
            };
        }
    }
}
