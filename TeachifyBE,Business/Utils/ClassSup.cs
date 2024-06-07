using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using TeachifyBE_Data.Models.ConfigModel;

namespace TeachifyBE_Business.Utils
{
    public class ClassSup
    {

        private static IConfiguration _config;
        private static IOptions<JwtModel> _setting;
        public ClassSup(IConfiguration config, IOptions<JwtModel> setting)
        {
            _setting = setting;
            _config = config;
        }
        public static string CreateToken(string email, Guid id, string roleName)
        {
            string key = "";
            string issuer = "";
            JwtSetting(ref key,ref issuer);

            List<Claim> claims = new()
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName),
                new Claim("rolename", roleName),
                new Claim("userid", id.ToString()),
                new Claim("email", email),
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(issuer, //_config["Jwt:Issuer"
              issuer,
                claims: claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return token;
        }
        private static void JwtSetting(ref string key, ref string issuer)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            key = config.GetValue<string>("Jwt:Key");
            issuer = config.GetValue<string>("Jwt:Issuer");
        }
    }
}
