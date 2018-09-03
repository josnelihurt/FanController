namespace FanControl
{
    internal struct Settings
    {
        public int MinimunTemperature;
        public int MaximunTemperature;
        public string SerialPort;
        public override string ToString()
        {
            return "max=" + MaximunTemperature + ";min=" + MinimunTemperature + ";Port=" + SerialPort;
        }
    }
}