using Registro_Estudiante.Models;
using Microsoft.EntityFrameworkCore;
using Registro_Asignaturas.Models;

namespace Registro_Estudiante.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Estudiantes> Estudiantes { get; set; } = null!;

        public DbSet<Asignaturas> Asignaturas { get; set; } = null!;
    }
}
