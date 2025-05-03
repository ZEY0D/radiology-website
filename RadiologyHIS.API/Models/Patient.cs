using System;

namespace RadiologyHIS.API.Models;

public class Patient
{
    public int Id { get; set; }
    public int UserId { get; set; }      
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = null!;
    public string BloodType { get; set; } = null!;

    // Navigation Property
    public User? User { get; set; }
}
