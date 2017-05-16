using System;

namespace HealthGuide.API.Patients.Models
{
    public class Appointment
    {
        public string Id { get; set; }

        public DateTimeOffset Slot { get; set; }

        public Patient Patient { get; set; }

        public Visit Visit { get; set; }
    }
}