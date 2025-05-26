using System.ComponentModel.DataAnnotations;

namespace MusicRadio.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido (debe contener @)")]
        [StringLength(50, ErrorMessage = "El correo no debe exceder los 50 caracteres")]
        [Display(Name = "Correo Electrónico")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(30, ErrorMessage = "La contraseña no debe exceder los 30 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}