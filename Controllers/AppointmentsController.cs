using HealthGuide.API.Patients.Data;
using HealthGuide.API.Patients.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HealthGuide.API.Patients.Controllers
{
    [Route("[controller]")]
    [ResponseCache(Duration = 30)]
    public class AppointmentsController : Controller
    {
        private PatientsContext _patientsContext;

        public AppointmentsController(PatientsContext patientsContext)
        {
            _patientsContext = patientsContext;
            _patientsContext.Database.EnsureCreated();
        }

        [HttpGet("visits/{patientid}", Name = "GetVisitsForPatient")]
        public async Task<IActionResult> Get(int patientid)
        {
            Patient patientMatch = await _patientsContext.Patients
                .Include(patient => patient.Visits)
                .SingleOrDefaultAsync(patient => patient.Id == patientid);
            if (patientMatch == null)
            {
                return NotFound();
            }
            return Ok(patientMatch.Visits);
        }

        [HttpPost("record", Name = "AddVisitForPatient")]
        public async Task<IActionResult> Post([FromBody]Appointment appointment)
        {            
            Patient patientMatch = await _patientsContext.Patients.Where(
                p => p.LastName.ToLower() == appointment.Patient.LastName.ToLower()
                &&
                p.FirstName.ToLower() == appointment.Patient.FirstName.ToLower()
            ).FirstOrDefaultAsync();
            if (patientMatch == null) {
                _patientsContext.Patients.Add(appointment.Patient);
                patientMatch = appointment.Patient;
            }
            Visit newVisit = appointment.Visit;
            patientMatch.Visits = new List<Visit> { newVisit };
            await _patientsContext.SaveChangesAsync();
            return CreatedAtRoute("GetVisit", new { id = newVisit.Id }, newVisit);
        }
    }
}