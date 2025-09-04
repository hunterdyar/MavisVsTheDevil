#include "LedControl.h"
#include <serial-readline.h>

LedControl lc=LedControl(4,2,3,1);
SerialLineReader reader(Serial);

void setup() {
  lc.shutdown(0,false);
  lc.setScanLimit(0, 8);
  lc.setIntensity(0,15);
  Serial.begin(9600);
  Serial.println("Starting.");
  for(int i = 0;i<8;i++){
      lc.setDigit(0,i,0,false);
      delay(5);
  }
}

void loop() {
  reader.poll();
  if(reader.available()) {
    int len = reader.len();
    if(len == 0){
      return;
    }
		char text[len];
		char vals[len];
    reader.read(text);
    Serial.print("Got: ");
		Serial.println(text);
    Serial.print("count: ");
    Serial.println(lc.getDeviceCount());
    // lc.shutdown(0,false);
    //reverse array
    for(int i = 0;i<len;i++){
      vals[len-i-1] = text[i];
    }

    for(int i = 0;i<8;i++){
      char val = '0';
      if(i < len){
        val = vals[i];
      }
      byte digit = getDigit(val);
      lc.setDigit(0,i,digit,false);
      Serial.println("set place "+String(i)+" to "+String(val));
      delay(5);
    }
    
	}

  delay(10);
}

byte getDigit(char v){
  switch(v){
    case '0':
      return 0;
    case '1':
      return 1;
    case '2':
      return 2;
    case '3':
      return 3;
    case '4':
      return 4;
    case '5':
      return 5;
    case '6':
      return 6;
    case '7':
    return 7;
    case '8':
    return 8;
    case '9':
    return 9;
  }
  return 5;
}

