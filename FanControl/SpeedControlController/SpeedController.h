#pragma once
#include <cstdint>

public ref class  SpeedController
{
	bool _initialized{};
	struct usb_device* _digiSpark{};
	struct usb_device* _device{};
	struct usb_dev_handle* _devHandle{};
	struct usb_interface_descriptor* _interface{};

public:
	~SpeedController();
	bool Init();
	bool Reset();
	bool SetSpeed(const std::uint8_t percentage);
private:
	bool SendByte(const std::uint8_t byte);
};