using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace curso.api.Infraestructure.Repositories
{
    /// <summary>
    /// Repositório de cursos
    /// </summary>
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _contexto;

        /// <summary>
        /// Construtor do repositório de cursos
        /// </summary>
        /// <param name="contexto"></param>
        public CursoRepository(CursoDbContext contexto)
        {
            _contexto = contexto;
        }

        /// <summary>
        /// Método responsável por adicionar um novo curso
        /// </summary>
        /// <param name="curso"></param>
        public void Adicionar(Curso curso)
        {
            _contexto.Curso.Add(curso);
        }

        /// <summary>
        /// método responsável por persistir os cursos adicionados no banco.
        /// </summary>
        public void Commit()
        {
            _contexto.SaveChanges();
        }

        /// <summary>
        /// Método responsável por listar os cursos de um determinado usuário
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <returns></returns>
        public IList<Curso> ObterPorUsuario(int codigoUsuario)
        {
            return _contexto.Curso.Include(i => i.Usuario).Where(w => w.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}
