#include "SpeedController.h"
#include <string.h>
#include <lusb0_usb.h>    // this is libusb, see http://libusb.sourceforge.net/
#using <mscorlib.dll>

using namespace System;
using namespace std;
SpeedController::~SpeedController()
{
	if (_devHandle != nullptr)
	{
		usb_release_interface(_devHandle, _interface->bInterfaceNumber);
		usb_close(_devHandle);
	}
}
bool SpeedController::Init()
{
	struct usb_bus *bus{};
	// Initialize the USB library
	usb_init();

	// Enumerate the USB _device tree
	usb_find_busses();
	usb_find_devices();

	// Iterate through attached busses and _devices
	bus = usb_get_busses();
	while (bus != nullptr)
	{
		_device = bus->devices;
		while (_device != nullptr)
		{
			// Check to see if each USB _device matches the _digiSpark Vendor and Product IDs
			if ((_device->descriptor.idVendor == 0x16c0) && (_device->descriptor.idProduct == 0x05df))
			{
				_digiSpark = _device;
			}

			_device = _device->next;
		}

		bus = bus->next;
	}
	if (_digiSpark != nullptr)
	{
		_devHandle = usb_open(_digiSpark);
	}

	_initialized = _digiSpark != nullptr && _devHandle != nullptr;
	return _initialized;
}

bool SpeedController::Reset()
{
	return SendByte(0xFF);
}

bool SpeedController::SetSpeed(const uint8_t percentage)
{
	float computeValue = percentage;
	computeValue /= 100;
	computeValue *= 254;
	uint8_t value = static_cast<uint8_t>(computeValue);
	return SendByte(value);
}
bool SpeedController::SendByte(const uint8_t percentage)
{
	if (!_initialized)
	{
		return _initialized;
	}
	int result = 0;
	int i = 0;
	char value{};
	int numInterfaces = 0;

	_interface = &(_digiSpark->config->interface[0].altsetting[0]);
	// read into thechar result = usb_control_msg(_devHandle, (0x01 << 5) | 0x80, 0x01, 0, 0, &thechar, 1, 1000);
	result = usb_control_msg(_devHandle, (0x01 << 5), 0x09, 0, percentage, 0, 0, 1000);
	result = usb_control_msg(_devHandle, (0x01 << 5) | 0x80, 0x01, 0, 0, &value, 1, 1000);
	return result > 0 && percentage == value;
	
}
