using System.ComponentModel.DataAnnotations;

namespace Registro_Asignaturas.Models;

public class Asignaturas
{
    [Key]

    public int AsignaturaId { get; set; }
    

    [Required(ErrorMessage = "El codigo es obligaritorio")]
    public int Codigo { get; set; }

    [Required(ErrorMessage = "El nombre de la asginatura es obligotorio")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La aula de la asignatura es obligatoria")]
    public string Aula{ get; set; }

    [Required]
    [Range(1, 4, ErrorMessage = "Los creditos deben estar entre 1 y 4")]
    public string Creditos { get; set; }


}
