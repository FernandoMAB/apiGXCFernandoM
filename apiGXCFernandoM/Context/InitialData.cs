using apiGXCFernandoM.Models;

namespace apiGXCFernandoM.Context
{
    public class InitialData
    {
        public List<Calificaciones> addCalificaciones()
        {
            List<Calificaciones> calificacionesInit = new List<Calificaciones>();
            calificacionesInit.Add(new Calificaciones()
            {
                Id = 1,
                idColegio = 1,
                idMateria = 1,
                idUsuario = 1,
                calificacion = 5
                });
            return calificacionesInit;
        }

        public List<Colegio> addColegios()
        {
            List<Colegio> colegioInit = new List<Colegio>();
            colegioInit.Add(new Colegio()
            {
                id = 1,
                nombre = "Nombre de Prueba",
                tipoColegio = "Privado"
            });
            return colegioInit;
        }

        public List<Materia> addMaterias()
        {
            List<Materia> materiaInit = new List<Materia>();
            materiaInit.Add(new Materia()
            {
                Id = 1,
                idColegio = 1,
                nombre = "Nombre Prueba",
                area = "Area Prueba",
                numeroEstudiantes = 10,
                docenteAsignado = "Docente Prueba",
                curso = "Primero",
                paralelo = "A"
            });
            return materiaInit;
        }

        public List<Usuario> addUsuarios()
        {
            List<Usuario> usuarioInit = new List<Usuario>();
            usuarioInit.Add(new Usuario()
            {
                Id = 1,
                nombreCompleto = "Usuario Prueba",
                username = "Fer",
                password = "admin",
                correoElectronico = "fer@fer.com",
                rol = "admin"
            });
            usuarioInit.Add(new Usuario()
            {
                Id = 2,
                nombreCompleto = "John Doe",
                username = "John",
                password = "john",
                correoElectronico = "john@fer.com",
                rol = "seller"
            });
            return usuarioInit;
        }
    }
}
