namespace LSP
{
    public class FillingStation
    {
        public void Refuel(Vehicle vehicle)
        {
            if (vehicle is PetrolCar)
                vehicle.FillUpWithFuel();
        }

        public void Charge(Vehicle vehicle)
        {
            if (vehicle is ElectricCar)
            {
                vehicle.ChargeBattery();
            }
        }
    }
}