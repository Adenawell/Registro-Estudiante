using Microsoft.EntityFrameworkCore;
using Registro_Asignaturas.Models;


namespace Registro_Asignaturas.DAL;

public class ContextoAsignaturas : DbContext
{
    public ContextoAsignaturas(DbContextOptions<ContextoAsignaturas> options) : base(options) { }

    public DbSet<Asignaturas> Asignaturas { get; set; } = null!;
}