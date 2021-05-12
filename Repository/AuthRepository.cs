using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using InternAPI.Data;
using InternAPI.Models;
using InternAPI.Repository.IRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace InternAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;
        public AuthRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public string Authenticate(string mail, string password)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            //Student authentication
            if (_db.Student.FirstOrDefault(x => x.Mail == mail && x.Password == password) != null)
            {
                var student = _db.Student.FirstOrDefault(x => x.Mail == mail && x.Password == password);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, student.Mail),
                        new Claim(ClaimTypes.Sid, student.Id.ToString()),
                        new Claim(ClaimTypes.Role, "Student")
                    }),
                    //After 7 days, token expires
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                student.Token = tokenHandler.WriteToken(token);

                var serializedStudent = JsonConvert.SerializeObject(student, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });
                return serializedStudent;
            }

            //Manager authentication
            else if (_db.Managers.FirstOrDefault(x => x.Mail == mail && x.Password == password) != null)
            {
                var manager = _db.Managers.FirstOrDefault(x => x.Mail == mail && x.Password == password);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, manager.Mail),
                        new Claim(ClaimTypes.Sid, manager.Id.ToString()),
                        new Claim(ClaimTypes.Role, "Manager")
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                manager.Token = tokenHandler.WriteToken(token);

                var serializedManager = JsonConvert.SerializeObject(manager, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });
                return serializedManager;
            }
            else
            {
                return null;
            }
        }

        public bool IsUnique(string mail)
        {
            return _db.Managers.Any(x => x.Mail == mail) || _db.Student.Any(x => x.Mail == mail) ? false : true;
        }


        public Student RegisterStudent(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}