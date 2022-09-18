using System.Text.Json.Serialization;

namespace apiGXCFernandoM.Models
{
    public class Materia
    {
        public Materia()
        {
            Calificaciones = new HashSet<Calificaciones>();
        }

        public int Id { get; set; }
        public int? idColegio { get; set; }
        public string? nombre { get; set; }
        public string? area { get; set; }
        public int? numeroEstudiantes { get; set; }
        public string? docenteAsignado { get; set; }
        public string? curso { get; set; }
        public string? paralelo { get; set; }

        public virtual Colegio? Colegio { get; set; }

        [JsonIgnore]
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }
    }
}
