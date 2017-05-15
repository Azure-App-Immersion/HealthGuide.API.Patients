using HealthGuide.API.Patients.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthGuide.API.Patients.Data
{
    public class PatientsContext : DbContext
    {
        public PatientsContext(DbContextOptions<PatientsContext> options) 
            : base(options)
        { }
        
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visit> Visits { get; set; }
    }
}