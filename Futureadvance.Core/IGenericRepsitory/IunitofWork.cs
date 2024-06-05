using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Futureadvance.Core.IGenericRepsitory
{
    public interface IunitofWork
    {
        public IUserRepository userRepository { get; set; }

        public Task<int> SaveAsync();
    }
}
