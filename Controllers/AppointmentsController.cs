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

        [HttpGet("visits/{id}", Name = "GetVisitsForPatient")]
        public async Task<IActionResult> Get(int id)
        {
            Patient patientMatch = await _patientsContext.Patients
                .Include(patient => patient.Visits)
                .SingleOrDefaultAsync(patient => patient.Id == id);
            if (patientMatch == null)
            {
                return NotFound();
            }
            return Ok(patientMatch.Visits);
        }

        [HttpPost(Name = "AddVisitForPatient")]
        public async Task<IActionResult> RecordAppointment(Appointment appointment)
        {
            Patient patientMatch = await _patientsContext.Patients.Where(
                p => p.LastName.Equals(appointment.Patient.LastName, StringComparison.OrdinalIgnoreCase)
            ).Where(
                p => p.FirstName.Equals(appointment.Patient.FirstName, StringComparison.OrdinalIgnoreCase)
            ).FirstOrDefaultAsync();
            if (patientMatch == null) {
                _patientsContext.Patients.Add(appointment.Patient);
                patientMatch = appointment.Patient;
            }
            Visit newVisit = appointment.Visit;
            patientMatch.Visits.Add(newVisit);
            await _patientsContext.SaveChangesAsync();
            return CreatedAtRoute("GetVisit", new { id = newVisit.Id }, newVisit);
        }
    }
}