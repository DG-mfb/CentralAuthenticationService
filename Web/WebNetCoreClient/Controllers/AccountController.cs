using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebNetCoreClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IActionResult> Get()
        {
            string key = "401b09eab3c013d4ca54922bb802bec8fd50a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var symmetricSecurityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(symmetricSecurityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenFromString = Request.Query["token"];
            if (tokenFromString.Count > 0)
            {
                var token = jwtSecurityTokenHandler.ReadToken(tokenFromString.First());
                return new JsonResult(token);
            }
            return Ok();
        }

        [HttpGet]
        [Route("sso/{client}", Name = "SSO")]
        public ActionResult<IActionResult> SSO(string client)
        {
            return Redirect(String.Format("https://localhost:44316/account/sso?clientId={0}", client));
        }

        [HttpGet]
        [Route("login", Name = "Login")]
        public ActionResult<IActionResult> Login()
        {
            return Ok("Redirect to Glasswall login page.");
        }
    }
}
