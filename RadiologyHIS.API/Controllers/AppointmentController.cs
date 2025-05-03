using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadiologyHIS.API.Data.Service;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Controllers
{
   [Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _iAppointmentService;

    public AppointmentController(IAppointmentService service)
    {
        _iAppointmentService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _iAppointmentService.GetAllAppointmentsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _iAppointmentService.GetAppointmentByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        var created = await _iAppointmentService.AddAppointmentAsync(appointment);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Appointment appointment)
    {
        var updated = await _iAppointmentService.UpdateAppointmentAsync(id, appointment);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _iAppointmentService.DeleteAppointmentAsync(id);
        return success ? NoContent() : NotFound();
    }
}

}
