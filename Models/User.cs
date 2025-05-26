namespace MusicRadio.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Required(ErrorMessage = "La identificación es obligatoria")]
        [StringLength(10, ErrorMessage = "La identificación no debe exceder los 10 caracteres")]
        [Display(Name = "Identificación")]
        public string Id { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        [Display(Name = "Nombre Completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido (debe contener @)")]
        [StringLength(50, ErrorMessage = "El correo no debe exceder los 50 caracteres")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Formato de correo inválido")]
        [Display(Name = "Correo Electrónico")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(30, ErrorMessage = "La contraseña no debe exceder los 30 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(500, ErrorMessage = "La dirección no debe exceder los 500 caracteres")]
        [Display(Name = "Dirección")]
        public string Direction { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no debe exceder los 20 caracteres")]
        [Phone(ErrorMessage = "Ingrese un número de teléfono válido")]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
    }
}