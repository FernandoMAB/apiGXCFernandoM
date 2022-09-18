using apiGXCFernandoM.Context;
using apiGXCFernandoM.Exeptions;
using apiGXCFernandoM.Models;
using apiGXCFernandoM.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiGXCFernandoM.Services
{
    public class UsuarioService : IUsuarioService
    {
        ColegioContext dbcontext;
        private readonly ILogger<UsuarioService> logger;

        public UsuarioService(ColegioContext dbcontext, ILogger<UsuarioService> logger)
        {
            this.dbcontext = dbcontext;
            this.logger = logger;
        }

        public IEnumerable<Usuario> GetAll()
        {
            if (dbcontext.Usuarios.Any())
                return dbcontext.Usuarios;
            else throw new NotFoundException(Constants.NONREGIST);
        }

        public Usuario? GetById(int id)
        {
            return dbcontext.Usuarios.Find(id)
                is Usuario usuario
                    ? usuario
                    : throw new NotFoundException(Constants.NONUSER);
        }

        public async Task<IResult> Save (Usuario usuario)
        {
            if(dbcontext.Usuarios.FirstOrDefault(x => x.username.Equals(usuario.username)) is Usuario usuarioRepetido)
            {
                throw new NotFoundException(Constants.USEREPE);
            }
            else
            {
                dbcontext.Usuarios.Add(usuario);
                await dbcontext.SaveChangesAsync();
                return Results.Created($"{usuario.Id}",usuario);
            }
        }

        public async Task<IResult> Update (int id, Usuario usuario)
        {
            var usuarioToUpdate = dbcontext.Usuarios.Find(id);

            if (usuarioToUpdate != null)
            {
                if (dbcontext.Usuarios.Where(x => x.username.Equals(usuario.username) && x.Id != usuario.Id) is Usuario usuarioRepetido)
                {
                    throw new BusinessException(Constants.USEREPE);
                }

                usuarioToUpdate.nombreCompleto      = usuario.nombreCompleto;
                usuarioToUpdate.username            = usuario.username;
                usuarioToUpdate.password            = usuario.password;
                usuarioToUpdate.correoElectronico   = usuario.correoElectronico;
                usuarioToUpdate.rol                 = usuario.rol;

                await dbcontext.SaveChangesAsync();
                return Results.Ok(Constants.UPDATEEX);

            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

        public async Task<IResult> Delete(int id)
        {
            var usuarioToDelete = dbcontext.Usuarios.Find(id);

            if (usuarioToDelete != null)
            {
                dbcontext.Usuarios.Remove(usuarioToDelete);
                await dbcontext.SaveChangesAsync();
                return Results.Ok(Constants.DELREGEX);
            }
            throw new NotFoundException(Constants.NOTFOUND);
        }
    }

    public interface IUsuarioService
    {
        IEnumerable<Usuario> GetAll();
        Usuario? GetById(int id);
        Task<IResult> Save(Usuario usuario);
        Task<IResult> Update(int id, Usuario usuario);
        Task<IResult> Delete(int id);
    }
}
