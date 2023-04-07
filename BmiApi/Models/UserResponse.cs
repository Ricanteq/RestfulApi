namespace BmiApi.Models;

public class UserResponse
{
    public int Id { get; set; }
    public int Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public string Name { get; set; }
    public double BMI { get; set; }
}