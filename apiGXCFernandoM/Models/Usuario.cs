using System.Text.Json.Serialization;

namespace apiGXCFernandoM.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Calificaciones = new HashSet<Calificaciones>();
        }

        public int Id { get; set; }
        public string? nombreCompleto { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? correoElectronico { get; set; }
        public string? rol { get; set; }

        [JsonIgnore]
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }

    }
}
