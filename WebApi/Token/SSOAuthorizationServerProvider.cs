using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Kernel.Authorisation;
using Kernel.DependancyResolver;
using Kernel.Federation.Constants;
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
                var jSonDataFormat = new JSonDataFormat();
                var token = jSonDataFormat.Protect(sSOTokenEndpointResponseContext.Ticket);
                var redirectUri = this.ResolveReturnUrl(sSOTokenEndpointResponseContext);
                var uri = String.Format("{0}?token={1}", redirectUri, token);
                sSOTokenEndpointResponseContext.Response.Redirect(uri);
                context.RequestCompleted();
            }
        }

        private string ResolveReturnUrl(SSOTokenEndpointResponseContext context)
        {
            object url;
            var state = context.RelayState;
            if (!state.TryGetValue(RelayStateContstants.RedirectUrl, out url))
                url = context.Configuration.TokenResponseUrl;
            return url.ToString();
        }
    }
}