using apiGXCFernandoM.Context;
using apiGXCFernandoM.Exeptions;
using apiGXCFernandoM.Models;
using apiGXCFernandoM.Tools;
using Microsoft.EntityFrameworkCore;

namespace apiGXCFernandoM.Services
{
    public class CalificacionService : ICalificacionService
    {
        ColegioContext dbcontext;
        private readonly ILogger<CalificacionService> logger;

        public CalificacionService (ColegioContext dbcontext, ILogger<CalificacionService> logger)
        {
            this.dbcontext = dbcontext;
            this.logger = logger;
        }

        public IEnumerable<Calificaciones> GetAll()
        {
            if (dbcontext.Calificaciones.Any())
                return dbcontext.Calificaciones.Include(x => x.Colegio)
                                                .Include(x => x.Materia)
                                                .Include(x => x.Usuario);
            else throw new NotFoundException(Constants.NONREGIST);
        }

        public Calificaciones? GetById(int id)
        {
            return dbcontext.Calificaciones.Include(x => x.Colegio)
                                            .Include(x => x.Materia)
                                            .Include(x => x.Usuario)
                                            .FirstOrDefault(x => x.Id == id)
                is Calificaciones calificaciones
                    ? calificaciones
                    : throw new NotFoundException(Constants.NONCOLE);
        }

        public async Task<IResult> Save(Calificaciones calificaciones)
        {
            dbcontext.Calificaciones.Add(calificaciones);
            await dbcontext.SaveChangesAsync();
            return Results.Created($"{calificaciones.Id}", calificaciones);
        }

        public async Task<IResult> Update(int id, Calificaciones calificaciones)
        {
            var califToUpdate = dbcontext.Calificaciones.Find(id);

            if (califToUpdate != null)
            {

                califToUpdate.idColegio     = calificaciones.idColegio;
                califToUpdate.idMateria     = calificaciones.idMateria;
                califToUpdate.idUsuario     = calificaciones.idUsuario;
                califToUpdate.calificacion  = calificaciones.calificacion;

                await dbcontext.SaveChangesAsync();
                return Results.Ok(Constants.UPDATEEX);

            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

        public async Task<IResult> Delete(int id)
        {
            var califToDelete = dbcontext.Calificaciones.Find(id);

            if (califToDelete != null)
            {
                dbcontext.Calificaciones.Remove(califToDelete);
                await dbcontext.SaveChangesAsync();
                return Results.Ok(Constants.DELREGEX);
            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

    }

    public interface ICalificacionService
    {
        IEnumerable<Calificaciones> GetAll();
        Calificaciones? GetById(int id);
        Task<IResult> Save(Calificaciones calificaciones);
        Task<IResult> Update(int id, Calificaciones calificaciones);
        Task<IResult> Delete(int id);
    }
}
