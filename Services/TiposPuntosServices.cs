using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registro_Estudiante.DAL;
using Registro_Puntos.Models;

namespace Registro_TiposPuntos.Services
{
    public class TiposPuntosServices
    {
        private readonly IDbContextFactory<Contexto> DbFactory;

        public TiposPuntosServices(IDbContextFactory<Contexto> dbFactory)
        {
            DbFactory = dbFactory;
        }

        public async Task<bool> Guardar(TiposPuntos tipoPunto)
        {
            if (await ExisteTipoConNombre(tipoPunto.Nombre, tipoPunto.TipoId))
                return false;

            if (!await Existe(tipoPunto.TipoId))
                return await Insertar(tipoPunto);
            else
                return await Modificar(tipoPunto);
        }

        private async Task<bool> Insertar(TiposPuntos tipoPunto)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            dbContext.TiposPuntos.Add(tipoPunto);
            return await dbContext.SaveChangesAsync() > 0;
        }

        private async Task<bool> Existe(int id)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.TiposPuntos.AnyAsync(t => t.TipoId == id);
        }

        public async Task<TiposPuntos?> Buscar(int id)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.TiposPuntos.FirstOrDefaultAsync(t => t.TipoId == id);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.TiposPuntos
                .Where(t => t.TipoId == id)
                .ExecuteDeleteAsync() > 0;
        }

        private async Task<bool> Modificar(TiposPuntos tipoPunto)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            dbContext.Update(tipoPunto);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<TiposPuntos>> Listar(Expression<Func<TiposPuntos, bool>> criterio)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.TiposPuntos
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> ExisteTipoConNombre(string nombre, int id)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.TiposPuntos
                .AnyAsync(t => t.Nombre.ToLower() == nombre.ToLower() && t.TipoId != id);
        }
    }
}