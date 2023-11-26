using eAgendaMedica.Aplicacao.ModuloAutenticacao;
using eAgendaMedica.WebApi.ViewModels.ModuloAutenticacao;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eAgendaMedica.WebApi.Config
{
    public static class UsuarioJwtExtension
    {
        public static TokenViewModel GerarJwt(this Usuario usuario, DateTime dataExpiracao)
        {
            var chaveToken = CriarChaveToken(usuario, dataExpiracao);

            var response = new TokenViewModel
            {
                Chave = chaveToken,
                DataExpiracao = dataExpiracao,
                UsuarioToken = new UsuarioTokenViewModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Login = usuario.UserName,
                    Email = usuario.Email
                }
            };

            return response;
        }

        private static string CriarChaveToken(Usuario usuario, DateTime dataExpiracao)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var chavePrivada = Encoding.ASCII.GetBytes("SegredoSuperSecretoDoeAgendaMedica");

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "eAgendaMedica",
                Audience = "http://localhost",
                Subject = ObterIdentityClaims(usuario),
                Expires = dataExpiracao,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chavePrivada), SecurityAlgorithms.HmacSha256Signature)
            });

            var keyToken = tokenHandler.WriteToken(token);

            return keyToken;
        }

        private static ClaimsIdentity ObterIdentityClaims(Usuario usuario)
        {
            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.GivenName, usuario.Nome));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserName));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));

            return identityClaims;
        }
    }
}
