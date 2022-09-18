using apiGXCFernandoM.Context;
using apiGXCFernandoM.Exeptions;
using apiGXCFernandoM.Models;
using apiGXCFernandoM.Tools;
using Microsoft.EntityFrameworkCore;

namespace apiGXCFernandoM.Services
{
    public class MateriaService : IMateriaService
    {
        ColegioContext dbcontext;
        private readonly ILogger<MateriaService> logger;

        public MateriaService (ColegioContext dbcontext, ILogger<MateriaService> logger)
        {
            this.dbcontext = dbcontext;
            this.logger = logger;
        }

        public IEnumerable<Materia> GetAll()
        {
            if (dbcontext.Materias.Any())
                return dbcontext.Materias.Include(x => x.Colegio);
            else throw new NotFoundException(Constants.NONREGIST);
        }

        public Materia? GetById(int id)
        {
            return dbcontext.Materias.Include(x => x.Colegio).FirstOrDefault(x => x.Id == id)
                is Materia materia
                    ? materia
                    : throw new NotFoundException(Constants.NONMATE);
        }

        public async Task<IResult> Save(Materia materia)
        {
            if (dbcontext.Materias.FirstOrDefault(x => x.nombre.Equals(materia.nombre)) is Materia materiaRepetido)
            {
                throw new NotFoundException(Constants.MATREPE);
            }
            else
            {
                dbcontext.Materias.Add(materia);
                await dbcontext.SaveChangesAsync();
                return Results.Created($"{materia.Id}", materia);
            }
        }

        public async Task<IResult> Update(int id, Materia materia)
        {
            var materiaToUpdate = dbcontext.Materias.Find(id);

            if (materiaToUpdate != null)
            {
                if (dbcontext.Materias.Where(x => x.nombre.Equals(materia.nombre) && x.Id != materia.Id) is Materia usuarioRepetido)
                {
                    throw new BusinessException(Constants.USEREPE);
                }

                materiaToUpdate.idColegio           = materia.idColegio;
                materiaToUpdate.nombre              = materia.nombre;
                materiaToUpdate.area                = materia.area;
                materiaToUpdate.numeroEstudiantes   = materia.numeroEstudiantes;
                materiaToUpdate.docenteAsignado     = materia.docenteAsignado;
                materiaToUpdate.curso               = materia.curso;
                materiaToUpdate.paralelo            = materia.paralelo;

                await dbcontext.SaveChangesAsync();
                return Results.Ok(Constants.UPDATEEX);

            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

        public async Task<IResult> Delete(int id)
        {
            var materiaToDelete = dbcontext.Materias.Find(id);

            if (materiaToDelete != null)
            {
                dbcontext.Materias.Remove(materiaToDelete);
                await dbcontext.SaveChangesAsync();
                return Results.Ok(Constants.DELREGEX);
            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

    }

    public interface IMateriaService
    {
        IEnumerable<Materia> GetAll();
        Materia? GetById(int id);
        Task<IResult> Save(Materia materia);
        Task<IResult> Update(int id, Materia materia);
        Task<IResult> Delete(int id);
    }
}
