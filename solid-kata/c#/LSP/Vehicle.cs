namespace LSP
{
    public abstract class Vehicle
    {
        private bool _engineStarted;

        public void Start() => _engineStarted = true;
        public void Stop() => _engineStarted = false;

        public abstract void FillUpWithFuel();
        public abstract void ChargeBattery();
    }
}