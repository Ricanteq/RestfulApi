using BmiApi.Models;

namespace BmiApi.Services.BMICalculatorService;

public class BMICalculatorService
{
    private static readonly List<UserResponse> _userList = new();
    private readonly IBMICalculator _bmiCalculator;

    public BMICalculatorService(IBMICalculator bmiCalculator)
    {
        _bmiCalculator = bmiCalculator;
    }

    public Task<UserResponse> CreateUserCalculateBmi(User user)
    {
        var bmi = _bmiCalculator.CalculateBMI(user.Weight, user.Height);
        user.Id = _userList.Count + 1;
        var userResponse = new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Age = user.Age,
            Weight = user.Weight,
            Height = user.Height,
            BMI = bmi
        };
        _userList.Add(userResponse);
        return Task.FromResult(userResponse);
    }


    public Task<List<UserResponse>> GetUsers()
    {
        return Task.FromResult(_userList);
    }


    public UserResponse GetUserById(int id)
    {
        return _userList.FirstOrDefault(u => u.Id == id);
    }

    public async Task DeleteUser(UserResponse user)
    {
        await Task.Run(() => _userList.Remove(user));
    }

    public double CalculateBMI(double weight, double height)
    {
        if (height == 0)
            throw new DivideByZeroException();

        var bmi = weight / (height * height);
        return Math.Round(bmi, 2);
    }
}