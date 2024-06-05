
using Futureadvance.Core.IGenericRepsitory;
using Futureadvance.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Futureadvance.Repostiory.GenericRepoistory
{
    public class Tokenservice : ITokenservice
    {
        private readonly SymmetricSecurityKey _key;
        public Tokenservice(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }


        public string createToken(Localuser user)
        {
            var Cliams = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };
            var creds= new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Cliams),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
       
        }

        
    }
}
