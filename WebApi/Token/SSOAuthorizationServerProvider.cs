﻿using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Kernel.Authorisation;
using Kernel.DependancyResolver;
using Kernel.Initialisation;
using Kernel.Security.CertificateManagement;
using Microsoft.IdentityModel.Tokens;
using SSOOwinMiddleware.Contexts;

namespace WebApi.Token
{
    public class SSOAuthorizationServerProvider : IAuthorizationServerProvider
    {
        private readonly IDependencyResolver _resolver;
        public SSOAuthorizationServerProvider(IDependencyResolver resolver)
        {
            this._resolver = resolver;
        }
        async Task IAuthorizationServerProvider.TokenEndpointResponse<TContext>(TContext context)
        { 
            var sSOTokenEndpointResponseContext = context as SSOTokenEndpointResponseContext;
            if (sSOTokenEndpointResponseContext != null)
            {
                string key = "401b09eab3c013d4ca54922bb802bec8fd50a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
                var identity = sSOTokenEndpointResponseContext.Ticket.Identity;
                var properties = sSOTokenEndpointResponseContext.Ticket.Properties;
                var resolver = ApplicationConfiguration.Instance.DependencyResolver;
                var certManager = resolver.Resolve<ICertificateManager>();
                var certContext = new X509CertificateContext { StoreName = "My", StoreLocation = System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine };
                certContext.SearchCriteria.Add(new CertificateSearchCriteria { SearchCriteriaType = System.Security.Cryptography.X509Certificates.X509FindType.FindBySubjectName, SearchValue = "www.eca-international.com" });
                var certificate = certManager.GetCertificateFromContext(certContext);
                var x509SecurityKey = new X509SecurityKey(certificate);
                var symmetricSecurityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                //var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(x509SecurityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.RsaSha256Signature);
                var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(symmetricSecurityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var jwt = jwtSecurityTokenHandler.CreateEncodedJwt("https://www.glasswall-dev.com", "https://www.glasswall-dev.com", identity, properties.IssuedUtc.GetValueOrDefault().DateTime, properties.ExpiresUtc.GetValueOrDefault().DateTime, properties.IssuedUtc.GetValueOrDefault().DateTime , signingCredentials, null);
                var redirectUri = sSOTokenEndpointResponseContext.Configuration.TokenResponseUrl;
                var uri = String.Format("{0}api/values?token={1}", redirectUri.AbsoluteUri, jwt);
                sSOTokenEndpointResponseContext.Response.Redirect(uri);
                context.RequestCompleted();
            }
        }
    }
}