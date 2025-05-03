using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadiologyHIS.API.Data.Service;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanController : ControllerBase
    {
        private readonly IScanService _iScanService;

        public ScanController(IScanService scanService)
        {
            _iScanService = scanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllScans()
        {
            var scans = await _iScanService.GetAllScansAsync();
            return Ok(scans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScanById(int id)
        {
            var scan = await _iScanService.GetScanByIdAsync(id);
            if (scan == null) return NotFound();
            return Ok(scan);
        }

        [HttpPost]
        public async Task<IActionResult> AddScan([FromBody] Scan scan)
        {
            var added = await _iScanService.AddScanAsync(scan);
            return CreatedAtAction(nameof(GetScanById), new { id = added.Id }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScan(int id, [FromBody] Scan scan)
        {
            var updated = await _iScanService.UpdateScanAsync(id, scan);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScan(int id)
        {
            var deleted = await _iScanService.DeleteScanAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}