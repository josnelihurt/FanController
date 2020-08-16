using FanControl;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UI
{
    public partial class MainForm : Form
    {
        private Timer _timer;
        private TemperatureReader _temperatureReader;
        public MainForm()
        {
            InitializeComponent();
            SetConnectionState(false);
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Tick += Timer_Tick;
            _temperatureReader = new TemperatureReader();
        }

        private FanControlHID _fan;
        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            _fan.SetSpeed((byte)_trackBar.Value);
        }

        private void _connectBtn_Click(object sender, EventArgs e)
        {
            try
            {

                _fan = new FanControlHID();
                _trackBar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("HID Device not found" + ex.ToString());
            }
        }

        private void _disconnect_Click(object sender, EventArgs e)
        {
            SetConnectionState(false);
        }
        private void SetConnectionState(bool state)
        {
            _trackBar.Enabled = state;
            if(!state)
            {
                
                _fan = null;
            }
        }
        private void _trackBar_Scroll(object sender, EventArgs e)
        {
            _timer.Stop();
            _timer.Start();
        }

        private void _reset_Click(object sender, EventArgs e)
        {
            if (_fan != null)
            {
                _fan.Reset();
            }
            SetConnectionState(false);
        }

        private void _startRead_Click(object sender, EventArgs e)
        {
            _temperatureReader.Start();
            _temperatureReader.OnNewTemperature += temperatureReader_OnNewTemperature;
        }

        private void temperatureReader_OnNewTemperature(float average, List<TemperatureReader.Sensor> sensors)
        {
            var gpu = sensors.Find(x => x.Name == "GPU Core");
            var cpu = sensors.Find(x => x.Name == "CPU Package");
            this.Invoke((MethodInvoker)delegate
            {
                _gpuValue.Text = gpu.Value.ToString();
                _cpuValue.Text = cpu.Value.ToString();
            });
        }
    }
}
