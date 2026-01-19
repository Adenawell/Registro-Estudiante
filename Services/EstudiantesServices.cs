using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Registro_Estudiante.DAL;
using Registro_Estudiante.Models;

namespace Registro_Estudiante.Services
{
    public class EstudiantesServices
    {
        private readonly IDbContextFactory<Contexto> DbFactory;

        public EstudiantesServices(IDbContextFactory<Contexto> dbFactory)
        {
            DbFactory = dbFactory;
        }

        public async Task<bool> Guardar(Estudiantes estudiante)
        {
            if (await ExisteEstudianteConName(estudiante.EstudiantesNames, estudiante.EstudianteId))
                return false;

            if (!await existe(estudiante.EstudianteId))
                return await insertar(estudiante);
            else
                return await Modificar(estudiante);
        }

        private async Task<bool> insertar(Estudiantes estudiante)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            dbContext.Estudiantes.Add(estudiante);
            return await dbContext.SaveChangesAsync() > 0;
        }

        private async Task<bool> existe(int idEstudiantes)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.Estudiantes.AnyAsync(e => e.EstudianteId == idEstudiantes);
        }

        private async Task<Estudiantes?> Buscar(int id)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.Estudiantes
                .FirstOrDefaultAsync(e => e.EstudianteId == id);
        }

        public async Task<bool> Eliminar(int idEstudiante)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.Estudiantes
                .Where(e => e.EstudianteId == idEstudiante)
                .ExecuteDeleteAsync() > 0;
        }

        private async Task<bool> Modificar(Estudiantes estudiante)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            dbContext.Estudiantes.Update(estudiante);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Estudiantes>> Listar(Expression<Func<Estudiantes, bool>> criterio)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.Set<Estudiantes>().Where(criterio).ToListAsync();
        }

        public async Task<bool> ExisteEstudianteConName(string nombre, int id)
        {
            await using var dbContext = await DbFactory.CreateDbContextAsync();
            return await dbContext.Estudiantes
                .AnyAsync(e => e.EstudiantesNames.ToLower() == nombre.ToLower()
                          && e.EstudianteId != id);
        }

    }
}
