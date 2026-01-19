using Registro_Estudiante.Models;
using Microsoft.EntityFrameworkCore;

namespace Registro_Estudiante.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Estudiantes> Estudiantes { get; set; } = null!;
    }
}
