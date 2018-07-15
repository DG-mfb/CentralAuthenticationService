using System.Collections.Generic;
using Kernel.Authorisation.Contexts;
using Kernel.Federation.Authorization;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace SSOOwinMiddleware.Contexts
{

    public class SSOTokenEndpointResponseContext : EndpointContext<SSOAuthenticationOptions>, ITokenEndpointResponseContext
    {
        public SSOTokenEndpointResponseContext(IOwinContext context, SSOAuthenticationOptions options, string token, AuthenticationTicket ticket, IDictionary<string, object> relayState, AuthorizationServerConfiguration configuration)
            : base(context, options)
        {
            this.Token = token;
            this.Ticket = ticket;
            this.RelayState = relayState;
            this.Configuration = configuration;
        }
        public AuthenticationTicket Ticket { get; }
        public string Token { get; }
        public IDictionary<string, object> RelayState { get; }
        public AuthorizationServerConfiguration Configuration { get; }
    }
}