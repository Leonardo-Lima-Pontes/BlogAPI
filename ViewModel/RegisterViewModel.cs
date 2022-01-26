using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome deve ser informado")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O E-mail deve ser informado")]
        [EmailAddress(ErrorMessage = "Por favor informe um email valido")]
        public string Email { get; set; }
    }
}
