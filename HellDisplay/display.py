# /// script
# requires-python = ">=3.13"
# dependencies = [
#     "watchdog",
#     "pyserial",
# ]
# ///
import sys
import time
import logging
from os import set_inheritable
from tokenize import String

import serial
from serial.tools import list_ports
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

path = "test.txt"
LastSent = 0
SerialObj = None  # COMxx  format on Windows

def connect():
    global SerialObj
    ports = list( list_ports.comports() )
    p = None
    for port in ports:
        if "Uno" not in port.description:
            print("Can't Use " + str(port))
            continue
        try:
            print("Connecting to " + port.description)
            SerialObj = serial.Serial(port.device, 9600)

            SerialObj.bytesize = 8  # Number of data bits = 8
            SerialObj.parity = 'N'  # No parity
            SerialObj.stopbits = 1  # Number of Stop bits = #
            SerialObj.open()
            p = port
        except (OSError, serial.SerialException):
            pass
    # ttyUSBx format on Linux

    time.sleep(1)
    if SerialObj is None:
        print("Failure. port in use?")
        return
    if SerialObj.isOpen():
        SerialObj.write(b'01234567\n')
    else:
        SerialObj.open()
    # SerialObj.close()  # Close the port

def sendLevel(level):
    global SerialObj, LastSent
    if level != LastSent:
        LastSent = level
    else:
        return
    l = i = int(level) if level.isdecimal() else None
    if i is None:
        LastSent = -1
        return
    if SerialObj is not None:
        print("Sending Level " + str(l))
        if SerialObj.isOpen():
            val = ('{0}\n'.format(level)).encode()
            SerialObj.write(val)  # transmit 'A' (8bit) to micro/Arduino
        else:
            LastSent = -1


def readData():
    read=False
    tries = 0
    while(not read and tries < 2):
        tries += 1
        try:
            f = open("../MavisVsTheDevil/bin/debug/net9.0/Resources/LayerOfHell.txt", "r", encoding="utf-8")
            a = f.read()
            sendLevel(a)
            read = True
            #f.close()
        except PermissionError:
            print("permission error. Trying again "+str(tries))
            time.sleep(.2)

class hellLevelWatcher(FileSystemEventHandler):
    def on_modified(self, event):
        print(event.src_path)
        print("modified")
        readData();



if __name__ == "__main__":
    connect()
    path = "../MavisVsTheDevil/bin/debug/net9.0/Resources/";
    event_handler = hellLevelWatcher()
    observer = Observer()
    observer.schedule(event_handler, path, recursive=True)
    observer.start()
    try:
        while True:
            time.sleep(1)
    except KeyboardInterrupt:
        observer.stop()
    observer.join()
    
   