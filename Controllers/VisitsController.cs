using HealthGuide.API.Patients.Data;
using HealthGuide.API.Patients.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthGuide.API.Patients.Controllers
{
    [Route("[controller]")]
    [ResponseCache(Duration = 30)]
    public class VisitsController : Controller
    {
        private PatientsContext _patientsContext;

        public VisitsController(PatientsContext patientsContext)
        {
            _patientsContext = patientsContext;
            _patientsContext.Database.EnsureCreated();
        }

        [HttpGet("{id}", Name = "GetVisit")]
        public async Task<IActionResult> Get(int id)
        {
            Visit visitMatch = await _patientsContext.Visits.SingleOrDefaultAsync(visit => visit.Id == id);
            if (visitMatch == null)
            {
                return NotFound();
            }
            return Ok(visitMatch);
        }
    }
}