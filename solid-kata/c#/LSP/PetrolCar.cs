namespace LSP
{
    public class PetrolCar : Vehicle
    {
        private const int FuelTankFull = 100;
        public int FuelTankLevel { get; private set; }

        public override void FillUpWithFuel() => FuelTankLevel = FuelTankFull;

        public override void ChargeBattery() => throw new InvalidOperationException("A petrol car cannot be recharged");
    }
}