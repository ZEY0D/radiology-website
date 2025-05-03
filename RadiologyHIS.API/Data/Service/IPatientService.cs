using System;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service;

public interface IPatientService
{
        // for patient (self)
    Task <User?> GetPatientProfileAsync (int patientId);
    Task <IEnumerable<Appointment>> GetAppointmentAsync (int patientId);
    Task <Appointment> BookAppointmentAsync (int patientId, Appointment appointment);
    Task <IEnumerable<Scan>> GetScanAsync (int patientId);

    // For Admins managment
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<Patient?> GetPatientByIdAsync(int id);
    Task<Patient> AddPatientAsync(Patient patient);
    Task<Patient?> UpdatePatientAsync(int id, Patient patient);
    Task<bool> DeletePatientAsync(int id);
}

