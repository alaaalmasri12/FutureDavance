using Futureadvance.Core.Models;
using Futureadvance.Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.Core.IGenericRepsitory
{
    public interface IUserRepository
    {
     Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO);
     Task<Localuser> Regiserasync(RegisteritionRequestDto registeritionRequestDto);
   Task<IReadOnlyList<Localuser>> getAllusers();
   public Task<Localuser> Updateuser(UpdateuserDto enitiy);
        public Task<Localuser> DeleteUserAsync(int userId);


    }
}
