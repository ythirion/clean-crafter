using FluentAssertions;
using Xunit;
using static OCP.EmployeeType;

namespace OCP.Tests
{
    public class EmployeeShould
    {
        private const int Salary = 1000;
        private const int Bonus = 100;

        [Fact]
        public void Not_Add_Bonus_To_The_Engineer() =>
            AnEngineer()
                .Pay()
                .Should()
                .Be(Salary);

        private static Employee AnEngineer() => new(Salary, Bonus, Engineer);

        [Fact]
        public void Add_Bonus_To_The_Manager() =>
            AManager()
                .Pay()
                .Should()
                .Be(1100d);

        private static Employee AManager() => new(Salary, Bonus, Manager);
    }
}