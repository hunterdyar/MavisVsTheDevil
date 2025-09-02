#include <LiquidCrystal.h>

//rs, enable, d4-7
LiquidCrystal lcd(12, 11, 5, 4, 3, 2);

void setup() {
  // put your setup code here, to run once:
  lcd.begin(16, 2);
  Serial.begin(115200);
  Serial.println("Starting.");
  lcd.print("start");
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available() > 0) {
    // read the incoming byte:
    int number = Serial.parseInt();
    if (number == 0) {
      return;
    }
    // say what you got:
    Serial.print("I received: ");
        lcd.print("hello");

    Serial.println(number, DEC);
    lcd.setCursor(0, 0);
    lcd.print(number);
    lcd.setCursor(0, 1);
  }
  delay(10);
}
