namespace apiGXCFernandoM.Models
{
    public class Calificaciones
    {
        public int Id { get; set; }

        public int idColegio { get; set; }
        public int idMateria { get; set; }
        public int idUsuario { get; set; }

        public decimal calificacion { get; set; }

        public virtual Colegio Colegio { get; set; }
        public virtual Materia Materia { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
