using FluentAssertions;
using Xunit;

namespace LSP.Tests
{
    public class FillingStationShould
    {
        private const int Full = 100;
        private readonly FillingStation _fillingStation = new();

        [Fact]
        public void Refuel_A_Petrol_Car()
        {
            var car = new PetrolCar();

            _fillingStation.Refuel(car);

            car.FuelTankLevel
                .Should()
                .Be(Full);
        }

        [Fact]
        public void Not_Fail_Refueling_An_Electric_Car() =>
            _fillingStation
                .Invoking(_ => _.Refuel(new ElectricCar()))
                .Should()
                .NotThrow();

        [Fact]
        public void Recharge_An_Electric_Car()
        {
            var car = new ElectricCar();

            _fillingStation.Charge(car);

            car.BatteryLevel
                .Should()
                .Be(Full);
        }

        [Fact]
        public void Not_Fail_Recharging_A_Petrol_Car() =>
            _fillingStation
                .Invoking(_ => _.Charge(new PetrolCar()))
                .Should()
                .NotThrow();
    }
}