using Microsoft.EntityFrameworkCore;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service
{
    public class ScanService : IScanService
    {
        private readonly HISContext _hisContext;

        public ScanService(HISContext context)
        {
            _hisContext = context;
        }

        public async Task<IEnumerable<Scan>> GetAllScansAsync()
        {
            return await _hisContext.Scans.ToListAsync();
        }

        public async Task<Scan?> GetScanByIdAsync(int id)
        {
            return await _hisContext.Scans.FindAsync(id);
        }

        public async Task<Scan> AddScanAsync(Scan scan)
        {
            _hisContext.Scans.Add(scan);
            await _hisContext.SaveChangesAsync();
            return scan;
        }

        public async Task<Scan?> UpdateScanAsync(int id, Scan updatedScan)
        {
            var existingScan = await _hisContext.Scans.FindAsync(id);
            if (existingScan == null) return null;

            existingScan.PatientId = updatedScan.PatientId;
            existingScan.DoctorId = updatedScan.DoctorId;
            existingScan.FilePath = updatedScan.FilePath;
            existingScan.Description = updatedScan.Description;
            existingScan.UploadedAt = updatedScan.UploadedAt;

            await _hisContext.SaveChangesAsync();
            return existingScan;
        }

        public async Task<bool> DeleteScanAsync(int id)
        {
            var scan = await _hisContext.Scans.FindAsync(id);
            if (scan == null) return false;

            _hisContext.Scans.Remove(scan);
            await _hisContext.SaveChangesAsync();
            return true;
        }
    }
}
