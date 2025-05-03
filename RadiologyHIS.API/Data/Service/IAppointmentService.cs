using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
        Task<Appointment?> UpdateAppointmentAsync(int id, Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}
