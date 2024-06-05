using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.Core.Models
{
    public class Apiresponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSucssess { get; set; }
        public List<string> ErrorMessages { get; set; }=new List<string>();
        public object Result { get; set; }
    }
}
