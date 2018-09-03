using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Diagnostics;

namespace FanControl
{
    class Service :  ServiceBase
    {
        float _average = 0;
        int _samplesCounter = 10;
        FanControlSerial _fan;
        TemperatureReader _reader = new TemperatureReader();
        string _serialPort = "COM0";
        int _min = 0;
        int _max = 0;
        public Service(Settings settings)
        {
            _min = settings.MinimunTemperature;
            _max = settings.MaximunTemperature;
            _serialPort = settings.SerialPort;
            _fan = new FanControlSerial(_serialPort);
            _reader.OnNewTemperature += _Reader_OnNewTemperature;
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
        private void _Reader_OnNewTemperature(float average, List<TemperatureReader.Sensor> sensors)
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
                _fan.SetSpeed(speed);
            }
        }

    }
}
