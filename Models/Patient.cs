using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthGuide.API.Patients.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}