using System;

namespace FanControl
{
    class FanControlHID : IFanControl
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
        public void SetSpeed(float value)
        {
            int internalValue = 0;
            if (value < 0)
            {
                internalValue = 0;
            }
            else if (value > 1.0f)
            {
                internalValue = 100;
            }
            else
            {
                internalValue = (int)(value * 100);
            }

            byte buffer = (byte)internalValue;
            _speedController.SetSpeed(buffer);
        }
    }
}
