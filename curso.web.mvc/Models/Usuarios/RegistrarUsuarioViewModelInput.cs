using System.ComponentModel.DataAnnotations;

namespace curso.web.mvc.Models.Usuarios
{
    public class RegistrarUsuarioViewModelInput
    {

        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O Email é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
