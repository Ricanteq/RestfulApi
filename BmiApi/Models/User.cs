using System.Text.Json.Serialization;

namespace BmiApi.Models; 
public class User 
{
   [JsonIgnore] public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
}