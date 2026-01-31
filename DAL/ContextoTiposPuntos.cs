using Microsoft.EntityFrameworkCore;
using Registro_Estudiante.DAL;
using Registro_Estudiante.Models;
using Registro_Puntos.Models;

namespace Registro_Puntos.DAL
{
    public class ContextoTiposPuntos : DbContext
    {
        public ContextoTiposPuntos(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<TiposPuntos> TiposPuntos { get; set; } = null!;
    }
}
