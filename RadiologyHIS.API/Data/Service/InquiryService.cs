using System;
using Microsoft.EntityFrameworkCore;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service;

public class InquiryService : IInquiryService
{
    private readonly HISContext _hisContext;
    public InquiryService(HISContext context) => _hisContext = context;

    public async Task<IEnumerable<Inquiry>> GetAllInquiriesAsync() =>
        await _hisContext.Inquiries.OrderByDescending(i => i.SentAt).ToListAsync();

    public async Task<Inquiry?> GetInquiryByIdAsync(int id) =>
        await _hisContext.Inquiries.FindAsync(id);

    public async Task<Inquiry> AddInquiryAsync(Inquiry inquiry)
    {
        inquiry.SentAt = DateTime.UtcNow;
        _hisContext.Inquiries.Add(inquiry);
        await _hisContext.SaveChangesAsync();
        return inquiry;
    }

    public async Task<bool> DeleteInquiryAsync(int id)
    {
        var inquiry = await _hisContext.Inquiries.FindAsync(id);
        if (inquiry == null) return false;
        _hisContext.Inquiries.Remove(inquiry);
        await _hisContext.SaveChangesAsync();
        return true;
    }
}
