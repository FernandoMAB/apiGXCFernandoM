using System.Text.Json.Serialization;

namespace apiGXCFernandoM.Models
{
    public class Colegio
    {
        public Colegio()
        {
            Materias = new HashSet<Materia>();
            Calificaciones = new HashSet<Calificaciones>();
        }

        public int id { get; set; }
        public string? nombre { get; set; }
        public string? tipoColegio { get; set; }

        [JsonIgnore]
        public virtual ICollection<Materia> Materias { get; set; }
        [JsonIgnore]
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }
    }
}
