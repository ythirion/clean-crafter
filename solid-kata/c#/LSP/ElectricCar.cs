namespace LSP
{
    public class ElectricCar : Vehicle
    {
        private const int BatteryFull = 100;
        public int BatteryLevel { get; private set; }

        public override void FillUpWithFuel()
            => throw new InvalidOperationException("It's an electric car");

        public override void ChargeBattery() => BatteryLevel = BatteryFull;
    }
}