using System;
using Microsoft.EntityFrameworkCore;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service;

public class PatientService : IPatientService
{
    private readonly HISContext _hisContext;
    public PatientService(HISContext hISContext){
        _hisContext = hISContext;
    }


// self managment part
    public async Task<User?> GetPatientProfileAsync(int patientId){
        var patient = await _hisContext.Patients
        .Include(p => p.User)
        .FirstOrDefaultAsync(p => p.Id == patientId);
        if (patient == null || patient.User == null)
{
    Console.WriteLine("Patient or user is null!");
    return null;
}

        return patient?.User;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentAsync(int patientId){
        return await _hisContext.Appointments
        .Where(ap => ap.PatientId == patientId)
        .Include(ap => ap.Doctor)
        .ToListAsync();
    }

    public async Task<Appointment> BookAppointmentAsync(int patientId, Appointment appointment){
        appointment.PatientId = patientId;
        appointment.Status = "pending";
        await _hisContext.Appointments.AddAsync(appointment);
        await _hisContext.SaveChangesAsync();
        return appointment;        
}

        public async Task<IEnumerable<Scan>> GetScanAsync(int patientId){
        return await _hisContext.Scans
        .Where(sc => sc.PatientId == patientId)
        .Include(sc => sc.Doctor)
        .ToListAsync();
    }




// admin managment part
public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return await _hisContext.Patients.Include(p => p.User).ToListAsync();
    }

    public async Task<Patient?> GetPatientByIdAsync(int id)
    {
        return await _hisContext.Patients.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Patient> AddPatientAsync(Patient patient)
    {
        _hisContext.Patients.Add(patient);
        await _hisContext.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient?> UpdatePatientAsync(int id, Patient patient)
    {
        var existing = await _hisContext.Patients.FindAsync(id);
        if (existing == null) {
                return null;}

        existing.Gender = patient.Gender;
        existing.BloodType = patient.BloodType;
        existing.DateOfBirth = patient.DateOfBirth;

        await _hisContext.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        var patient = await _hisContext.Patients.FindAsync(id);
        if (patient == null) {
        return false;
        }

        _hisContext.Patients.Remove(patient);
        await _hisContext.SaveChangesAsync();
        return true;
    }


}
