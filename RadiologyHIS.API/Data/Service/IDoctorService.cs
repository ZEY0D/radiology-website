using System;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service;

public interface IDoctorService
{   
    Task <IEnumerable<Doctor>> GetAllDoctorsAsync();
    Task <Doctor?> GetDoctorByIdAsync(int id);
    Task <Doctor> AddDoctorAsync(Doctor doctor);
    Task <Doctor?> UpdateDoctorAsync(int id, Doctor doctor);
    Task <bool> DeleteDoctorAsync(int id);
}
