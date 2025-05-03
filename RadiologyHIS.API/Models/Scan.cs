using System;

namespace RadiologyHIS.API.Models;

public class Scan
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public string FilePath { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    // Navigation Property
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}
