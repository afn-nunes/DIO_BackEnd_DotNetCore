using curso.api.models.Usuarios;

namespace curso.api.Business.Repositories
{
    public interface IAuthenticationService
    {
        string GerarToken(RegistroViewModelInput usuarioViewModelOutput);
    }
}
