using System.Collections.Generic;
using System.ServiceProcess;
using System.Diagnostics;

namespace FanControl
{
    class Service :  ServiceBase
    {
        float _temperature = 0;
        int _samplesCounter = 10;
        IFanControl _fan;
        TemperatureReader _reader = new TemperatureReader();
        private readonly int _min = 0;
        private readonly int _max = 0;
        private readonly string _sensorName;

        public Service(Settings settings)
        {
            _min = settings.MinimunTemperature;
            _max = settings.MaximunTemperature;
            _sensorName = settings.SensorName;

            if(settings.SerialPort != null)
            {
                _fan = new FanControlSerial(settings.SerialPort);
            }
            else
            {
                _fan = new FanControlHID();
            }
            _reader.OnNewTemperature += Reader_OnNewTemperature;
        }
        protected override void OnStart(string[] args)
        {
            Debug.WriteLine("Start", "Service");
            base.OnStart(args);
            Run();
        }

        internal void Run()
        {
            Debug.WriteLine("Run", "Service");
            _reader.Start();
            _fan.SetSpeed(0);
        }

        protected override void OnStop()
        {
            Debug.WriteLine("Stop", "Service");
            base.OnStop();
            _reader.Stop();
        }
        private void Reader_OnNewTemperature(float average, List<TemperatureReader.Sensor> sensors)
        {
            var value = GetCurrentTemperatureValue(average, sensors);
            UpdateTemperature(value);
            UpdateOutput();
        }

        private float GetCurrentTemperatureValue(float average, List<TemperatureReader.Sensor> sensors)
        {
            float value;
            if (_sensorName.ToLower() == "average")
            {
                value = average;
            }
            else
            {
                var sensor = sensors.Find(x => x.Name == _sensorName);
                value = sensor.Value;
            }

            return value;
        }

        private void UpdateTemperature(float value)
        {
            if (_temperature == 0)
            {
                _temperature = value;
            }
            else
            {
                _temperature = (_temperature + value) / 2;
            }
        }

        private void UpdateOutput()
        {
            if (_samplesCounter++ < 10)
            {
                return;
            }
            _samplesCounter = 0;

            byte internalValue = 100;
            if (_min == 0)
            {
                internalValue = (byte)(((int)_temperature >= _max) ? 0 : 100);
            }
            else
            {
                var speed = (_temperature - _min) / (_max - _min);
                if (speed < 0)
                {
                    internalValue = 0;
                }
                else if (speed > 1.0f)
                {
                    internalValue = 100;
                }
                else
                {
                    internalValue = (byte)(speed * 100);
                }
            }

            _fan.SetSpeed(internalValue);

        }
    }
}
