using System.Collections.Generic;
using System.ServiceProcess;
using System.Diagnostics;

namespace FanControl
{
    class Service :  ServiceBase
    {
        float _average = 0;
        int _samplesCounter = 10;
        IFanControl _fan;
        TemperatureReader _reader = new TemperatureReader();
        int _min = 0;
        int _max = 0;
        public Service(Settings settings)
        {
            _min = settings.MinimunTemperature;
            _max = settings.MaximunTemperature;
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
            if (_average == 0)
            {
                _average = average;
            }
            else
            {
                _average = (_average + average) / 2;
            }
            if (_samplesCounter++ > 10)
            {
                _samplesCounter = 0;
                var speed = (_average - _min) / (_max - _min);
                byte internalValue = 0;
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
                
                _fan.SetSpeed(internalValue);
            }
        }

    }
}
