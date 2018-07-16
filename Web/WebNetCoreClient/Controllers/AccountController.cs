using System;
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
        public ActionResult<IActionResult> Get()
        {
            return Ok();
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
    }
}