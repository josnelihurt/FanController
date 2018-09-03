
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace FanControl
{
    internal class TemperatureReader
    {
        public struct Sensor
        {
            public string Name;
            public float Value;
        }
        public delegate void OnNewTemperatureEH(float average, List<Sensor> sensors);
        public event OnNewTemperatureEH OnNewTemperature;
        private Timer _timer;
        public TemperatureReader()
        {
            _timer = new Timer(500);
            _timer.Elapsed += _Timer_Elapsed;
            
        }

        private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            Read();
            _timer.Start();
        }

        public void Start()
        {
            _timer.Start();

        }
        private void Read()
        {
            var computerHardware = new OpenHardwareMonitor.Hardware.Computer();
            computerHardware.CPUEnabled = true;
            computerHardware.Open();
            var hardwareCount = computerHardware.Hardware.Count();
            if (hardwareCount > 0)
            {
                var sensors = computerHardware.Hardware[0].Sensors.ToList();
                var temperatureSensors = sensors.FindAll(x => x.SensorType == OpenHardwareMonitor.Hardware.SensorType.Temperature);
                var validTemperatureSensors = temperatureSensors.FindAll(x => x.Value.HasValue);
                if(validTemperatureSensors.Count == 0)
                {
                    Debug.Write("ERROR Invalid Sensors");
                    return;
                }
                float average = validTemperatureSensors.Average(x => x.Value.Value);
                var sensorsOutput = new List<Sensor>();
                foreach (var sensor in validTemperatureSensors)
                {
                    sensorsOutput.Add(new Sensor() { Name= sensor.Name, Value = sensor.Value.Value});
                }
                OnNewTemperature?.Invoke(average, sensorsOutput);

            }
            
        }

        internal void Stop()
        {
            _timer.Stop();
        }
    }
}