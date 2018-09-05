#include <DigiUSB.h>

#define LED_BUILTIN 1
#define MAGIC_RESET 0xFF
void(* softReset) (void) = 0; //declare reset function @ address 0
void blink(unsigned char times)
{
  for(int i = 0; i < times; ++i)
  {
    digitalWrite(LED_BUILTIN, HIGH);   
    delay(200);                       
    digitalWrite(LED_BUILTIN, LOW);    
    delay(200);                       
  }
}
void setup() 
{
  DigiUSB.begin();
  // initialize digital pin LED_BUILTIN as an output.
  pinMode(LED_BUILTIN, OUTPUT);  
  blink(1);  
  set_pwm_value(5);
}

void set_pwm_value(unsigned char value)
{
 analogWrite(LED_BUILTIN, value);
}

void loop() 
{
  int lastRead;
  if (DigiUSB.available()) 
  {
      // something to read
      lastRead = DigiUSB.read();
      if(lastRead == MAGIC_RESET) //! Magic value, 0-100 is a valid value
      {
        blink(2);
        softReset();
      }
      else
      {
        set_pwm_value(lastRead);
      }
      DigiUSB.write(lastRead);
  }
    
  // refresh the usb port for 10 milliseconds
  DigiUSB.delay(10);
}


/*
 Hardware PWM
To use hardware PWM in your sketch, use the analogWrite() function.
In the default implementation of PWM in arduino IDE, hardware PWM frequency is quite low:
Digispark @ 16.5MHz
Digispark Pin PWM Frequency (Hz)
Pin0  504
Pin1  504
Pin4  1007
By setting FAVOR_PHASE_CORRECT_PWM to 0 in arduino-1.0x/hardware/digispark/cores/tiny/core_build_options.h file, it's possible to double the frequency on Pin1:
Digispark @ 16.5MHz
+ FAVOR_PHASE_CORRECT_PWM set to 0
Digispark Pin PWM Frequency (Hz)
Pin0  504
Pin1  1007
Pin4  1007
But for some applications, this may be not sufficient. For example, if the Digispark is used as Electronic Speed Controller for brushed motors, using hardware PWM which such a low frequency is not perfect: the ESC will be noisy (audible) since PWM frequency is within the audio range.
Another application where “high” frequency is required: Digital Analog Converter. Using a PWM pin followed with a simple RC low pass filter, it's very easy to build a DAC. Using a high PWM frequency will increase the response time and will reduce the ripple.
How to increase Hardware PWM Frequency?
The usual way to increase PWM frequency consists in changing the assigned timer prescaler. This is not sufficient: micros(), millis() and delay() will be broken since these functions rely on the timer which is also used for PWM.
New Digispark IDE release (may 2013) introduces a new capability: Hardware PWM frequency adjustment without breaking micros(), millis() and delay().
Simply by setting the new MS_TIMER_TICK_EVERY_X_CYCLES symbol in arduino-1.0x/hardware/digispark/cores/tiny/wiring.c file to a value lower than 64 (the default arduino value), it's possible to increase the hardware PWM frequency.
The maximum reachable frequency for Pin1 and Pin4 is obtained with:

The maximum Digispark clock frequency: 16.5MHz
FAVOR_PHASE_CORRECT_PWM set to 0
MS_TIMER_TICK_EVERY_X_CYCLES set to 1
Digispark @ 16.5MHz
+ FAVOR_PHASE_CORRECT_PWM set to 0
+ MS_TIMER_TICK_EVERY_X_CYCLES set to 1
Digispark Pin PWM Frequency (Hz)
Pin0  32227
Pin1  64453
Pin4  64453
Note: Frequencies for other MS_TIMER_TICK_EVERY_X_CYCLES values are easy to compute: Take the above frequency values and divide them with MS_TIMER_TICK_EVERY_X_CYCLES.
For example, if MS_TIMER_TICK_EVERY_X_CYCLES is set to 8, the obtained frequencies are 8 time smaller.
 */
