using System;

namespace RadiologyHIS.API.Models;

public class Appointment
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateTime AppointmentDate  { get; set; }
    public string Status { get; set; } = "pending";
    public string? Notes { get; set; }

    // Navigation Property
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}
