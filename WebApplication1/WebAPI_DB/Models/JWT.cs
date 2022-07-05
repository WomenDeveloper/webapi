using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI_DB.Models;

namespace WebAPI_DB.Models
{
    public class JWT : IJwt
    {
        private string _key;
        private readonly FilmDBContext _db;

        public JWT(string key)
        {
            _key = key;
        }
        public JWT(FilmDBContext db,string key)
        {
            _db = db;
            _key = key;
        }

        public string KontrolEt(string kullanici, string sifre)
        {
            string result = null;

            FilmDBContext filmDb = new FilmDBContext();

            User? user = filmDb.Users.Where(u => u.Name == kullanici && u.Password == sifre).SingleOrDefault();

            if (user != null) 
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var bytKey = Encoding.UTF8.GetBytes(_key);

                var tokenDesc = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, kullanici) }),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDesc);
                result = tokenHandler.WriteToken(token);

            }
            
            return result;

        }
    }
}
