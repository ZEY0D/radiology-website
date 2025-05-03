using System;

using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service
{
    public interface IScanService
    {
        Task<IEnumerable<Scan>> GetAllScansAsync();
        Task<Scan?> GetScanByIdAsync(int id);
        Task<Scan> AddScanAsync(Scan scan);
        Task<Scan?> UpdateScanAsync(int id, Scan updatedScan);
        Task<bool> DeleteScanAsync(int id);
    }
}
