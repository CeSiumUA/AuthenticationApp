using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationApp.Controllers
{
    public class AccountController : Controller
    {
        private List<Person> personsList = new List<Person>();
        public AccountController()
        {
            Person prsUsr = new Person()
            {
                Login = "user",
                Role = AuthenticationApp.Roles.PersonRole.User,
                Password = "12345"
            };
            Person prsAdm = new Person()
            {
                Login = "admin",
                Role = AuthenticationApp.Roles.PersonRole.Admin,
                Password = "admin"
            };
            Person prsMod = new Person()
            {
                Login = "moder",
                Password = "moder",
                Role = AuthenticationApp.Roles.PersonRole.Moderator
            };
            personsList.Add(prsAdm);
            personsList.Add(prsMod);
            personsList.Add(prsUsr);
        }
        [HttpPost("/token")]
        public IActionResult GetToken(string username, string password)
        {
            ClaimsIdentity claimsIdentity = GetPerson(username, password);
            if (claimsIdentity != null)
            {
                DateTime currentTime = DateTime.Now;
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer:AuthenticationToken.Issuer, 
                    AuthenticationToken.Audience, 
                    claimsIdentity.Claims,
                    currentTime,
                    currentTime.Add(TimeSpan.FromMinutes(AuthenticationToken.TTL)), 
                    new SigningCredentials(AuthenticationToken.GetSymmetricSecurityKey(), 
                        SecurityAlgorithms.HmacSha256));
                string token =
                    new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                var response = new
                {
                    access_token = token,
                    username = claimsIdentity.Name
                };
                return Json(response);
            }
            else
            {
                return BadRequest(400);
            }
            
        }

        public ClaimsIdentity GetPerson(string username, string password)
        {
            Person person = null;
            person = personsList.Where(x => x.Login == username && x.Password == password).FirstOrDefault();
            if (person != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString())
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}