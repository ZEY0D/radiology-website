using System;
using Microsoft.EntityFrameworkCore;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service;

public class AppointmentService : IAppointmentService
{
    private readonly HISContext _hisContext;

    public AppointmentService(HISContext context)
    {
        _hisContext = context;
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _hisContext.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .ToListAsync();
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(int id)
    {
        return await _hisContext.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
    {
        _hisContext.Appointments.Add(appointment);
        await _hisContext.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> UpdateAppointmentAsync(int id, Appointment appointment)
    {
        var existing = await _hisContext.Appointments.FindAsync(id);
        if (existing == null) return null;

        existing.AppointmentDate = appointment.AppointmentDate;
        existing.Status = appointment.Status;
        existing.Notes = appointment.Notes;
        existing.DoctorId = appointment.DoctorId;
        existing.PatientId = appointment.PatientId;

        await _hisContext.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        var appointment = await _hisContext.Appointments.FindAsync(id);
        if (appointment == null) return false;

        _hisContext.Appointments.Remove(appointment);
        await _hisContext.SaveChangesAsync();
        return true;
    }
}
