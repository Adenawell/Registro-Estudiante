using System.ComponentModel.DataAnnotations;

namespace Registro_Asignaturas.Models;

public class Asignaturas
{
    [Key]
    public int AsignaturaId { get; set; }

    [Required(ErrorMessage = "El código de la asignatura es obligatorio.")]
    public int Codigo { get; set; }

    [Required(ErrorMessage = "El nombre de la asignatura no puede estar vacío.")]
    [StringLength(100, ErrorMessage = "El nombre es demasiado largo.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe especificar un aula.")]
    public string Aula { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los créditos son obligatorios.")]
    [Range(1, 4, ErrorMessage = "Los créditos deben estar en un rango de 1 a 4.")]
    public int Creditos { get; set; }
}
