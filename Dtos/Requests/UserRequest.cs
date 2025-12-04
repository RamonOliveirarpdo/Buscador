using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Buscador.Dtos
{
    public class UserRequest
    {
        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "O UserName deve ter no minimo 8 e maximo 16 caracteres.")]
        [DefaultValue("")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "O formato dos dados fornecidos é inválido.")]
        [DefaultValue("usuario@exemplo.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "A senha deve ter no mínimo 8 e maximo 16 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@!$#%^&*()_+])[A-Za-z\d@!$#%^&*()_+]{8,}$",
        ErrorMessage = "A senha não atende aos requisitos de segurança.")]
        [DefaultValue("Senha@123")]
        public string Password { get; set; }
    }
}
