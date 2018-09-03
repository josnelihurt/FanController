#include <ESP8266WiFi.h>
#define OUT_PIN 2

//=======================================================================
//                    Power on setup
//=======================================================================
void setup() {
  Serial.begin(115200);
  pinMode(OUT_PIN, OUTPUT);
  Serial.println("START");
}
//=======================================================================
//                    Main Program Loop
//=======================================================================
void loop() {
  analogWrite(OUT_PIN, 1023);
  uint64_t lastUpdate = millis();
  while (1)
  {
    if (millis() - lastUpdate > 15000 )
    {
      analogWrite(OUT_PIN, 1023);
    }
    if (Serial.available() > 0) {
      lastUpdate = millis();
      delay(10);

      char value = Serial.read();
      char end_line = Serial.read();
      if (end_line != '\n')
      {
        Serial.println("ERR");
        continue;
      }
      //! Validate input value bw 0 - 100
      if (value > 100)
      {
        Serial.println("ERR");
      }
      else
      {
        int pwm_output = (value * 10) + 23;
        analogWrite(OUT_PIN, pwm_output);
        Serial.printf("OK%d\n", pwm_output);
      }
    }
  }
}

