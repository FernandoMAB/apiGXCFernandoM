using apiGXCFernandoM.Context;
using apiGXCFernandoM.Exeptions;
using apiGXCFernandoM.Models;
using apiGXCFernandoM.Tools;

namespace apiGXCFernandoM.Services
{
    public class ColegioService : IColegioService
    {
        ColegioContext dbcontext;
        private readonly ILogger<ColegioService> logger;

        public ColegioService(ColegioContext dbcontext, ILogger<ColegioService> logger)
        {
            this.dbcontext = dbcontext;
            this.logger = logger;
        }

        public IEnumerable<Colegio> GetAll()
        {
            if (dbcontext.Colegios.Any())
                return dbcontext.Colegios;
            else throw new NotFoundException(Constants.NONREGIST);
        }

        public Colegio? GetById(int id)
        {
            return dbcontext.Colegios.Find(id)
                is Colegio colegio
                    ? colegio
                    : throw new NotFoundException(Constants.NONCOLE);
        }

        public async Task<IResult> Save(Colegio colegio)
        {
            if (dbcontext.Colegios.FirstOrDefault(x => x.nombre.Equals(colegio.nombre)) is Colegio colegioRepetido)
            {
                throw new NotFoundException(Constants.COLREPE);
            }
            else
            {
                dbcontext.Colegios.Add(colegio);
                await dbcontext.SaveChangesAsync();
                return Results.Created($"{colegio.id}", colegio);
            }
        }

        public async Task<IResult> Update(int id, Colegio colegio)
        {
            var colegioToUpdate = dbcontext.Colegios.Find(id);

            if (colegioToUpdate != null)
            {
                if (dbcontext.Colegios.FirstOrDefault(x => x.nombre.Equals(colegio.nombre) && x.id != colegio.id) is Colegio colegioRepetido)
                {
                    throw new BusinessException(Constants.COLREPE);
                }

                colegioToUpdate.nombre          = colegio.nombre;
                colegioToUpdate.tipoColegio     = colegio.tipoColegio;

                await dbcontext.SaveChangesAsync();
                return Results.Ok(Constants.UPDATEEX);

            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

        public async Task<IResult> Delete(int id)
        {
            var colegioToDelete = dbcontext.Colegios.Find(id);

            if (colegioToDelete != null)
            {
                dbcontext.Colegios.Remove(colegioToDelete);
                await dbcontext.SaveChangesAsync();
                return Results.Ok(Constants.DELREGEX);
            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

    }

    public interface IColegioService
    {
        IEnumerable<Colegio> GetAll();
        Colegio? GetById(int id);
        Task<IResult> Save(Colegio colegio);
        Task<IResult> Update(int id, Colegio colegio);
        Task<IResult> Delete(int id);
    }
}
