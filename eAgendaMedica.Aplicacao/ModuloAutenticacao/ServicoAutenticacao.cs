using Microsoft.AspNetCore.Identity;

namespace eAgendaMedica.Aplicacao.ModuloAutenticacao
{
    public class ServicoAutenticacao : ServicoBase<Usuario, ValidadorUsuario>
    {
        private readonly UserManager<Usuario> userManeger;
        private readonly SignInManager<Usuario> signInManeger;

        public ServicoAutenticacao(UserManager<Usuario> userManeger, SignInManager<Usuario> signInManeger)
        {
            this.userManeger = userManeger;
            this.signInManeger = signInManeger;
        }

        public async Task<Result<Usuario>> RegistrarAsync(Usuario usuario, string senha)
        {
            var resultadoValidacao = Validar(usuario);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            IdentityResult usuarioResult = await userManeger.CreateAsync(usuario, senha);

            if (usuarioResult.Succeeded == false)
                return Result.Fail(usuarioResult.Errors
                             .Select(erro => new Error(erro.Description)));

            return Result.Ok(usuario);
        }

        public async Task<Result<Usuario>> AutenticarAsync(string userName, string senha)
        {
            var loginResult = await signInManeger.PasswordSignInAsync(userName, senha, false, true);

            var erros = new List<IError>();

            if (loginResult.IsLockedOut)
                erros.Add(new Error ("O acesso desse usuário foi bloqueado."));

            if (loginResult.IsNotAllowed)
                erros.Add(new Error("Senha ou login incorretos."));

            if (erros.Count > 0)
                return Result.Fail(erros);

            var usuario = await userManeger.FindByNameAsync(userName);

            return Result.Ok(usuario);
        }

        public async Task<Result<Usuario>> SairAsync()
        {
            await signInManeger.SignOutAsync();

            return Result.Ok();
        }
    }
}
