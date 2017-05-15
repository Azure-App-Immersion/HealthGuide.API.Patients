using HealthGuide.API.Patients.Data;
using HealthGuide.API.Patients.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthGuide.API.Patients.Controllers
{
    [Route("[controller]")]
    [ResponseCache(Duration = 30)]
    public class PatientsController : Controller
    {
        private PatientsContext _patientsContext;

        public PatientsController(PatientsContext patientsContext)
        {
            _patientsContext = patientsContext;
            _patientsContext.Database.EnsureCreated();
        }

        [HttpGet(Name = "GetPatients")]
        public async Task<IEnumerable<Patient>> Get()
        {
            return await _patientsContext.Patients.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetPatient")]
        public async Task<IActionResult> Get(int id)
        {
            Patient patient = await _patientsContext.Patients.Include(p => p.Visits).SingleOrDefaultAsync(patient => patient.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
    }
}