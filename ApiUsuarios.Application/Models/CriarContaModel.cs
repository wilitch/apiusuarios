using System.ComponentModel.DataAnnotations;

namespace ApiUsuarios.Application.Models
{
    public class CriarContaModel
    {
        [RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{6,150}$", ErrorMessage = "Informe um nome válido de 6 a 150 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do usuário.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string? Email { get; set; }

        [SenhaForte(ErrorMessage = "Informe pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial (!@#$%&)")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string? Senha { get; set; }
    }

    public class SenhaForte : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                var senha = value.ToString();

                return senha.Any(char.IsUpper) //pelo menos 1 letra maiúscula
                    && senha.Any(char.IsLower) //pelo menos 1 letra minuscula
                    && senha.Any(char.IsDigit) //pelo menos 1 dígito numérico
                    && ( //pelo menos 1 dos caracteres especiais abaixo:
                           senha.Contains("!")
                        || senha.Contains("@")
                        || senha.Contains("#")
                        || senha.Contains("$")
                        || senha.Contains("%")
                        || senha.Contains("&")
                    );
            }

            return false;
        }
    }
}



