using System;

namespace FanControl
{
    public class FanControlHID : IFanControl
    {
        private SpeedController _speedController;
        public FanControlHID()
        {
            _speedController = new SpeedController();
            if(!_speedController.Init())
            {
                throw new NotSupportedException("Unable init HID device");
            }
        }
        public void SetSpeed(byte value)
        {
            _speedController.SetSpeed(value);
        }

        public void Reset()
        {
            _speedController.Reset();
        }
    }
}
