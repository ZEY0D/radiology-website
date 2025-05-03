using System;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service;

public interface IInquiryService
{
    Task<IEnumerable<Inquiry>> GetAllInquiriesAsync();
    Task<Inquiry?> GetInquiryByIdAsync(int id);
    Task<Inquiry> AddInquiryAsync(Inquiry inquiry);
    Task<bool> DeleteInquiryAsync(int id);
}
