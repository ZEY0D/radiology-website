using System;

namespace RadiologyHIS.API.Models;

public class Doctor
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Specialization  { get; set; } = null!;
    public string Department { get; set; } = "Radiology";
    
    // Navigation property
    public User? User { get; set; }
}
