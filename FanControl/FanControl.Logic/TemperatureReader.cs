
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace FanControl
{

    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
                subHardware.Accept(this);
        }

        public void VisitSensor(ISensor sensor) { }

        public void VisitParameter(IParameter parameter) { }
    }
    public class TemperatureReader
    {
        public struct Sensor
        {
            public string Name;
            public float Value;
        }
        public delegate void OnNewTemperatureEH(float average, List<Sensor> sensors);
        public event OnNewTemperatureEH OnNewTemperature;
        private Timer _timer;
        private Computer _computerHardware;
        private IVisitor _visitor;

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
            _computerHardware = new Computer();
            _computerHardware.HardwareAdded += ComputerHardware_HardwareAdded;
            _computerHardware.HardwareRemoved += ComputerHardware_HardwareRemoved;
            _computerHardware.Open();
            _computerHardware.GPUEnabled = true;
            _computerHardware.CPUEnabled = true;            
            _visitor = new UpdateVisitor();
            _timer.Start();

        }

        private void ComputerHardware_HardwareRemoved(IHardware hardware)
        {
        }

        private void Read()
        {
            _computerHardware.Accept(_visitor);
            var hardwareCount = _computerHardware.Hardware.Count();
            if (hardwareCount > 0)
            {
                var sensors = _computerHardware.Hardware 
                    .SelectMany(x=>x.Sensors)
                    .Where(x => x.SensorType == SensorType.Temperature)
                    .Where(x => x.Value.HasValue)
                    .ToList().ConvertAll(x => new Sensor() { Name = x.Name, Value = x.Value.Value });
                
                float average = sensors.Average(x => x.Value);
                OnNewTemperature?.Invoke(average, sensors);
            }            
        }

        private void ComputerHardware_HardwareAdded(IHardware hardware)
        {
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}