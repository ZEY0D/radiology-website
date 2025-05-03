using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadiologyHIS.API.Data;
using RadiologyHIS.API.Data.Service;
using RadiologyHIS.API.Models;
namespace RadiologyHIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _iPatientService;
        public PatientController(IPatientService iPatientService){
            _iPatientService = iPatientService;
        }

        [HttpGet("{patientId}/profile")]
        public async Task<IActionResult>  GetPatientProfileAsync(int patientId){
            var patientProfile = await _iPatientService.GetPatientProfileAsync(patientId);
            if (patientProfile == null){
                return NotFound();
            }
            return Ok(patientProfile);
        }


        [HttpGet("{patientId}/appointments")]
        public async Task<IActionResult> GetAppointmentAsync(int patientId){
            var Appointments = await _iPatientService.GetAppointmentAsync(patientId);
            return Ok(Appointments);
        }


        [HttpPost("{patientId}/appointments")]
        public async Task<IActionResult> BookAppointmentAsync(int patientId, Appointment appointment){
            var bookedAppointment = await _iPatientService.BookAppointmentAsync(patientId, appointment);
            return Ok(bookedAppointment);
        }


        [HttpGet("{patientId}/scans")]
        public async Task<IActionResult> GetScanAsync(int patientId){
            var scan = await _iPatientService.GetScanAsync(patientId);
            return Ok(scan);
        }




    // admin managment part
    [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _iPatientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _iPatientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] Patient patient)
        {
            var newPatient = await _iPatientService.AddPatientAsync(patient);
            return CreatedAtAction(nameof(GetPatientById), new { id = newPatient.Id }, newPatient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            var updated = await _iPatientService.UpdatePatientAsync(id, patient);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _iPatientService.DeletePatientAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
