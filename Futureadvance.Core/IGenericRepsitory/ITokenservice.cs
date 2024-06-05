using Futureadvance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.Core.IGenericRepsitory
{
    public interface ITokenservice
    {
        string createToken(Localuser user);
    }
}
