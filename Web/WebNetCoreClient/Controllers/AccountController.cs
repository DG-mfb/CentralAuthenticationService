using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebNetCoreClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, "Cookies", base.User);
            return Ok();
            //return Redirect("https://localhost:44397/api/account/home");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("sso/{client}", Name = "SSO")]
        public ActionResult<IActionResult> SSO(string client)
        {
            return Redirect(String.Format("https://localhost:44316/account/sso?clientId={0}", client));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login", Name = "Login")]
        public ActionResult<IActionResult> Login()
        {
            return Redirect("https://localhost:44342/client");
        }

        [HttpGet]
        [Route("home", Name = "Home")]
        public async Task<IActionResult> Home()
        {
            return Ok();
        }
    }
}