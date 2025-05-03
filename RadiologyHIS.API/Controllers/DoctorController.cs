using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadiologyHIS.API.Data.Service;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _iDoctorService;
        public DoctorController(IDoctorService iDoctorService){
            _iDoctorService = iDoctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors(){
            var Doctors = await _iDoctorService.GetAllDoctorsAsync();
            return Ok(Doctors);
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id){
            var doctor = await _iDoctorService.GetDoctorByIdAsync(id);
            if (doctor == null){
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] Doctor doctor){
            var newDoctor = await _iDoctorService.AddDoctorAsync(doctor);
            return CreatedAtAction(nameof(GetDoctorById), new { id = newDoctor.Id }, newDoctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor doctor){
            var updatedDoctor = await _iDoctorService.UpdateDoctorAsync(id, doctor);
            if (updatedDoctor == null){
                return NotFound();
            }
            return Ok(updatedDoctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id){
            var result = await _iDoctorService.DeleteDoctorAsync(id);
            if (!result){
                return NotFound();}
            return NoContent();
        }

    }
}
