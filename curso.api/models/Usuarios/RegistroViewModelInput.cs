using System.ComponentModel.DataAnnotations;

namespace curso.api.models.Usuarios
{
    //Registro do usuario
    public class RegistroViewModelInput
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatório")]
        public string Senha { get; set; }


    }
}
