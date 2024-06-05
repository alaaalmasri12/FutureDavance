namespace Futureadvance.API.Customizeresponses
{
    public class ErrorResponse:Response
    {
        //public string? Message { get; set; }
        //public int statuscode { get; set; }
        public ErrorResponse(): base(400)
        {
           errors = new List<string>();
        }

  
        public IEnumerable<string> errors { get; set; }
    }
}
