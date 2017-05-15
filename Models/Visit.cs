using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HealthGuide.API.Patients.Models
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }

        public string Reason { get; set; }

        public string Prescription { get; set; }

        public string Notes { get; set; }

        [JsonIgnore]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [JsonIgnore]
        public virtual Patient Patient { get; set; }
    }
}