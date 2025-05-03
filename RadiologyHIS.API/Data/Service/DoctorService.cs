using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service;

public class DoctorService : IDoctorService
{
    private readonly HISContext _hisContext;
    public DoctorService(HISContext hisContext){
        _hisContext = hisContext;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync(){
        return await _hisContext.Doctors.Include(d => d.User).ToListAsync();

    }

    public async Task<Doctor?> GetDoctorByIdAsync(int id){
        return await _hisContext.Doctors.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Doctor> AddDoctorAsync(Doctor doctor){
        _hisContext.Doctors.Add(doctor);
        await _hisContext.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor?> UpdateDoctorAsync(int id, Doctor doctor){
        var existingDoctor = await _hisContext.Doctors.FindAsync(id);
        if (existingDoctor == null){
            return null;
        }
        existingDoctor.Department = doctor.Department;
        existingDoctor.Specialization = doctor.Specialization;
        await _hisContext.SaveChangesAsync();
        return existingDoctor;
    }


    public async Task<bool> DeleteDoctorAsync(int id){
        var doctorToDelete = await _hisContext.Doctors.FindAsync(id);
        if (doctorToDelete == null){
            return false;
        }
        _hisContext.Remove(doctorToDelete);
        await _hisContext.SaveChangesAsync();
        return true;
    }
}
