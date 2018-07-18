using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Owin.Security;

namespace WebApi.Token
{
    internal class JSonDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        public string Protect(AuthenticationTicket data)
        {
            string key = "401b09eab3c013d4ca54922bb802bec8fd50a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var identity = data.Identity;
            var properties = data.Properties;
            var symmetricSecurityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(symmetricSecurityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwt = jwtSecurityTokenHandler.CreateEncodedJwt("https://www.glasswall-dev.com", "https://www.glasswall-dev.com", identity, properties.IssuedUtc.GetValueOrDefault().DateTime, properties.ExpiresUtc.GetValueOrDefault().DateTime, properties.IssuedUtc.GetValueOrDefault().DateTime, signingCredentials, null);
            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}