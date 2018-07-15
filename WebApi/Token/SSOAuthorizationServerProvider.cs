using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Kernel.Authorisation;
using Kernel.DependancyResolver;
using Kernel.Initialisation;
using Kernel.Security.CertificateManagement;
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
                var jth = new JwtSecurityTokenHandler();
                
                var identity = sSOTokenEndpointResponseContext.Ticket.Identity;
                var properties = sSOTokenEndpointResponseContext.Ticket.Properties;
                var resolver = ApplicationConfiguration.Instance.DependencyResolver;
                var certManager = resolver.Resolve<ICertificateManager>();
                var certContext = new X509CertificateContext { StoreName = "My", StoreLocation = System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine };
                certContext.SearchCriteria.Add(new CertificateSearchCriteria { SearchCriteriaType = System.Security.Cryptography.X509Certificates.X509FindType.FindBySubjectName, SearchValue = "www.eca-international.com" });
                var certificate = certManager.GetCertificateFromContext(certContext);
                var jwt = jth.CreateEncodedJwt("https://www.glasswall-dev.com", "https://www.glasswall-dev.com", identity, properties.IssuedUtc.GetValueOrDefault().DateTime, properties.ExpiresUtc.GetValueOrDefault().DateTime, properties.IssuedUtc.GetValueOrDefault().DateTime , new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.X509SecurityKey(certificate), SecurityAlgorithms.RsaSha256Signature), null);
                var redirectUri = sSOTokenEndpointResponseContext.Configuration.TokenResponseUrl;
                var uri = String.Format("{0}?token={1}", redirectUri.AbsoluteUri, jwt);
                sSOTokenEndpointResponseContext.Response.Redirect(uri);
                context.RequestCompleted();
            }
        }
    }
}