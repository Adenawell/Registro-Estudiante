using Microsoft.EntityFrameworkCore;
using Registro_Estudiante.DAL; 
using Registro_Asignaturas.Models;
using System.Linq.Expressions;

namespace Registro_Asignaturas.Services;

public class AsignaturasServices
{

    private readonly IDbContextFactory<Contexto> DbFactory;

    public AsignaturasServices(IDbContextFactory<Contexto> dbFactory)
    {
        DbFactory = dbFactory;
    }

    public async Task<bool> Guardar(Asignaturas asignatura)
    {
        if (await ExisteAsignatura(asignatura.Nombre, asignatura.Codigo, asignatura.AsignaturaId))
            return false;

        if (!await Existe(asignatura.AsignaturaId))
            return await Insertar(asignatura);
        else
            return await Modificar(asignatura);
    }

    private async Task<bool> Insertar(Asignaturas asignatura)
    {
        await using var dbContext = await DbFactory.CreateDbContextAsync();
        dbContext.Asignaturas.Add(asignatura);
        return await dbContext.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int id)
    {
        await using var dbContext = await DbFactory.CreateDbContextAsync();
        return await dbContext.Asignaturas.AnyAsync(a => a.AsignaturaId == id);
    }

    public async Task<Asignaturas?> Buscar(int id)
    {
        await using var dbContext = await DbFactory.CreateDbContextAsync();
        return await dbContext.Asignaturas.FirstOrDefaultAsync(a => a.AsignaturaId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var dbContext = await DbFactory.CreateDbContextAsync();
        return await dbContext.Asignaturas.Where(a => a.AsignaturaId == id).ExecuteDeleteAsync() > 0;
    }

    private async Task<bool> Modificar(Asignaturas asignatura)
    {
        await using var dbContext = await DbFactory.CreateDbContextAsync();
        dbContext.Asignaturas.Update(asignatura);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<List<Asignaturas>> Listar(Expression<Func<Asignaturas, bool>> criterio)
    {
        await using var dbContext = await DbFactory.CreateDbContextAsync();
        return await dbContext.Asignaturas.Where(criterio).AsNoTracking().ToListAsync();
    }

    public async Task<bool> ExisteAsignatura(string nombre, int codigo, int id)
    {
        await using var dbContext = await DbFactory.CreateDbContextAsync();
        return await dbContext.Asignaturas
            .AnyAsync(a => (a.Nombre.ToLower() == nombre.ToLower() || a.Codigo == codigo) && a.AsignaturaId != id);
    }
}