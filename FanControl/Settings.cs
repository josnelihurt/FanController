namespace FanControl
{
    internal struct Settings
    {
        public int MinimunTemperature;
        public int MaximunTemperature;
        public string SensorName;
        public string SerialPort;
        public override string ToString()
        {
            return "max=" + MaximunTemperature + ";min=" + MinimunTemperature + ";Port=" + SerialPort + ";SensorName=" + SensorName;
        }
    }
}