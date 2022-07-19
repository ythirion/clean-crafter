using static OCP.EmployeeType;

namespace OCP
{
    public record Employee(double Salary, double Bonus, EmployeeType Type)
    {
        public double Pay() =>
            Type switch
            {
                Engineer => Salary,
                Manager => Salary + Bonus,
                _ => 0
            };
    }
}