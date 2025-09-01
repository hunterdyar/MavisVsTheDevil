# /// script
# requires-python = ">=3.13"
# dependencies = [
#     "watchdog",
# ]
# ///
import sys
import time
import logging
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

path = "test.txt";

def sendLevel(level):
    l = i = int(level) if level.isdecimal() else None
    if(i is None):
        return
    print(l)

def readData():
    read=False
    tries = 0
    while(not read and tries < 4):
        print("reading file")
        tries += 1
        try:
            f = open("test.txt", "r", encoding="utf-8")
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
    logging.basicConfig(level=logging.INFO,
                        format='%(asctime)s - %(message)s',
                        datefmt='%Y-%m-%d %H:%M:%S')
    path = sys.argv[1] if len(sys.argv) > 1 else '.'
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
    
   