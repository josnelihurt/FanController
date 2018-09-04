using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;

namespace FanControl
{
    public class FanControlSerial : IFanControl
    {
        string _port;
        SerialPort _serialPort;
        const int _baudrate = 115200;
        public FanControlSerial(string port)
        {
            if(port == "auto")
            {
                if(!FindPolo())
                {
                    throw new NotSupportedException("Unable to find Serial port");
                }

            }
            else
            {
                _port = port;
                _serialPort = new SerialPort(_port, _baudrate, Parity.None);
                _serialPort.Open();
            }
            SetSpeed(1);
        }

        private bool FindPolo()
        {
            bool result = false;
            foreach(var portName in SerialPort.GetPortNames())
            {
                try
                {
                    Debug.WriteLine("Serial port " + portName);
                    _serialPort = new SerialPort(portName, _baudrate, Parity.None);
                    _serialPort.Open();
                    var buffer = new byte[2];
                    int i = 0;
                    buffer[i++] = 99;
                    buffer[i++] = (byte)'\n';
                    _serialPort.Write(buffer, 0, buffer.Length);
                    Thread.Sleep(500);

                    var bufferRead = new byte[1024];
                    if (_serialPort.BytesToRead > 0 && _serialPort.BytesToRead < bufferRead.Length)
                    {
                        _serialPort.Read(bufferRead, 0, _serialPort.BytesToRead);
                        var debug = System.Text.Encoding.Default.GetString(bufferRead);
                        Debug.WriteLine(debug);
                        result = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Serial port error == " + ex.ToString());
                }
            }
            return result;
        }

        /// <summary>
        /// Set the value for speed output
        /// </summary>
        /// <param name="value"> value bw 0.0 and 1.0, 1.0 is max</param>
        public void SetSpeed(byte value)
        {
            var buffer = new byte[2];
            int i = 0;
            buffer[i++] = (byte)value;
            buffer[i++] = (byte)'\n';
            _serialPort.Write(buffer, 0, buffer.Length);
            Thread.Sleep(500);

            var bufferRead = new byte[1024];
            if (_serialPort.BytesToRead > 0 && _serialPort.BytesToRead < bufferRead.Length)
            {
                _serialPort.Read(bufferRead, 0, _serialPort.BytesToRead);
                var debug = System.Text.Encoding.Default.GetString(bufferRead);
                Console.WriteLine(debug);
            }
        }
    }
}