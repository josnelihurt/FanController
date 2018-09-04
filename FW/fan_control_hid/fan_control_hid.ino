#include <DigiUSB.h>
#include <avr/wdt.h>
#define LED_BUILTIN 1
#define MAGIC_RESET 0xFA
void(* softReset) (void) = 0; //declare reset function @ address 0
void blink(unsigned char times)
{
  for(int i = 0; i < times; ++i)
  {
    digitalWrite(LED_BUILTIN, LOW);   
    delay(500);                       
    digitalWrite(LED_BUILTIN, HIGH);    
    delay(500);                       
  }
}
void setup() 
{
  DigiUSB.begin();
  // initialize digital pin LED_BUILTIN as an output.
  pinMode(LED_BUILTIN, OUTPUT);  
  blink(5);
}

void set_pwm_value(unsigned char value)
{
  
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
        blink(1);
      }
      DigiUSB.write(lastRead);
  }
    
  // refresh the usb port for 10 milliseconds
  DigiUSB.delay(10);
}
