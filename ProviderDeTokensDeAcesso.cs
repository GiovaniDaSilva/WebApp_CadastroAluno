using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuario = BaseUsuarios
                .Usuarios()
                .FirstOrDefault(x => x.nome == context.UserName
                                && x.senha == context.Password);

            if (usuario == null)
            {
                context.SetError("invalid_grant", "Usuário não encontrado ou senha incorreta.");
                return;
            }

            var props = new AuthenticationProperties(new Dictionary<string, string> 
            {
                {"UserName",context.UserName}                 
            });

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var identidadeUsuario = new AuthenticationTicket(identity, props);

            foreach (var funcao in usuario.funcoes)
            {
                identidadeUsuario.Identity.AddClaim(new Claim(ClaimTypes.Role, funcao));
            }

            context.Validated(identidadeUsuario);
                                  
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var item in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(item.Key, item.Value);
            }

            var clains = context.Identity.Claims
                .GroupBy(x => x.Type)
                .Select(y => new { Claim = y.Key, Value = y.Select(z => z.Value).ToArray() });


            foreach (var item in clains)
            {
                context.AdditionalResponseParameters.Add(item.Claim, JsonConvert.SerializeObject(item.Value));
            }

            return base.TokenEndpoint(context);
        }
    }
}