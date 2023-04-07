namespace BmiApi.Services.BMICalculatorService;

public class BMICalculator : IBMICalculator
{
    public double CalculateBMI(double weight, double height)
    {
        if (height == 0)
            throw new DivideByZeroException();

        var bmi = weight / (height * height);
        return Math.Round(bmi, 2);
    }
}