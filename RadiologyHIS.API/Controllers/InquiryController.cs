using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadiologyHIS.API.Data.Service;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class InquiryController : ControllerBase
{
    private readonly IInquiryService _iInquiryService;
    public InquiryController(IInquiryService service) => _iInquiryService = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _iInquiryService.GetAllInquiriesAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var inquiry = await _iInquiryService.GetInquiryByIdAsync(id);
        return inquiry == null ? NotFound() : Ok(inquiry);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Inquiry inquiry)
    {
        var newInquiry = await _iInquiryService.AddInquiryAsync(inquiry);
        return CreatedAtAction(nameof(GetById), new { id = newInquiry.Id }, newInquiry);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _iInquiryService.DeleteInquiryAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}

}
