using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Infraestructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        
        private readonly CursoDbContext _contexto;


        public UsuarioRepository(CursoDbContext contexto)
        {
            _contexto = contexto;
            var migracoesPendentes = contexto.Database.GetPendingMigrations();

            if (migracoesPendentes.Count() > 0)
            {
                contexto.Database.Migrate();
            }
        }

        public void Adicionar(Usuario usuario)
        {
            _contexto.Usuario.Add(usuario);
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public async Task<Usuario> ObterUsuarioAsync(string login)
        {
            return await _contexto.Usuario.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}
