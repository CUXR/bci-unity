# Muse to keyboard prototype
import time
import subprocess
from pynput.keyboard import Key, Controller

DELAY = 1.0
# Change target key depending on what we need
TARGET_KEY = "W"

keyboard = Controller()  # from the pynput.keyboard library

# detect a "click" in the keyboard
def click_keyboard(target):
    # handling special cases lilikeek spaces, enters, and backspaces
    if target == "<SPACE>":
        keyboard.press(Key.space) 
        keyboard.release(Key.space)
    elif target == "<ENTER>":
        keyboard.press(Key.enter)
        keyboard.release(Key.enter)
    elif target == "<BKSP>":
        keyboard.press(Key.backspace)
        keyboard.release(Key.backspace)
    else:
        keyboard.type(target)
    print(f"Keypress sent: {target}")

# This works on Windows, unsure if it'll work on Mac
def open_notepad():
    try:
        subprocess.Popen(["notepad.exe"])
        print("Opening Notepad...")
        time.sleep(1.5) 
    except Exception as e:
        print("Could not open Notepad:", e)

while True:
    inp = input("Press Enter to arm a keypress (or type 'q' to quit): ")
    if inp.strip().lower() == "q":
        break

    # open Notepad automatically
    open_notepad()

    print(f"Notepad open, keypress in {DELAY} s…")
    time.sleep(DELAY)
    click_keyboard(TARGET_KEY)
    print("Sent key.")
