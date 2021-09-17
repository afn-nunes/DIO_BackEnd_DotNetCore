using System.ComponentModel.DataAnnotations;

namespace curso.api.models.Cursos
{
    public class CursoViewModelInput
    {        
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required(ErrorMessage ="O nome do curso é obrigatório")]
        public string Nome { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required(ErrorMessage = "A descrição do curso é obrigatório")]
        public string Descricao { get; set; }
    }
}
