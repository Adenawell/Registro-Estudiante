using System.ComponentModel.DataAnnotations;

namespace Registro_Estudiante.Models
{
    public class Estudiantes
    {
        [Key]
        public int EstudianteId { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string EstudiantesNames { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe introducir un formato de correo electronico valido")]
        public string Emails { get; set; } = string.Empty;

        [Required(ErrorMessage = "La edad es obligatoria")]
        [Range(1, 120, ErrorMessage = "La edad debe estar entre 1 y 120 años")]
        public int? Edad { get; set; }
    }
}