using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o email")]
        [EmailAddress(ErrorMessage = "Informe o endereço de email de maneira correta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; }
    }
}
