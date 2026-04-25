// SETUP INSTRUCTIONS
// ------------------
// 1. In the Project panel (bottom of Unity), look inside your Assets folder
// 2. If you have a Scripts folder, put this file in there
//    If you don't, right click Assets → Create → Folder → name it "Scripts" → put this file in there
// 3. In the Hierarchy panel (left side), right click → Create Empty → name it "ControllerMapper"
// 4. With "ControllerMapper" selected, look at the Inspector panel (right side)
// 5. Click "Add Component" → search for "QuestButtonToSpace" → select it
// 6. Press Play in Unity — the A button on your Quest will now trigger Space in PsychoPy
//                        — the B button on your Quest will now trigger B in PsychoPy
//
// ALSO MAKE SURE:
// - Your Quest is connected via Link cable or AirLink before pressing Play
// - Your PsychoPy experiment is already running before pressing Play
// ------------------

using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class QuestButtonToSpace : MonoBehaviour
{
    private InputDevice _rightController;
    private bool _aWasPressed = false;
    private bool _bWasPressed = false;

    [DllImport("user32.dll")] private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    private const byte VK_SPACE = 0x20;  // Space key
    private const byte VK_B     = 0x42;  // B key
    private const uint KEYEVENTF_KEYDOWN = 0x0000;
    private const uint KEYEVENTF_KEYUP   = 0x0002;

    void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        RefreshDevice();
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
    }

    void OnDeviceConnected(InputDevice device) => RefreshDevice();

    void RefreshDevice()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0) _rightController = devices[0];
    }

    void Update()
    {
        if (!_rightController.isValid) return;

        // --- A Button → Space ---
        bool aIsPressed = false;
        _rightController.TryGetFeatureValue(CommonUsages.primaryButton, out aIsPressed);

        if (aIsPressed && !_aWasPressed)
        {
            Debug.Log("A button pressed → sending Space to PsychoPy");
            keybd_event(VK_SPACE, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event(VK_SPACE, 0, KEYEVENTF_KEYUP, 0);
        }
        _aWasPressed = aIsPressed;

        // --- B Button → B key ---
        bool bIsPressed = false;
        _rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bIsPressed);

        if (bIsPressed && !_bWasPressed)
        {
            Debug.Log("B button pressed → sending B to PsychoPy");
            keybd_event(VK_B, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event(VK_B, 0, KEYEVENTF_KEYUP, 0);
        }
        _bWasPressed = bIsPressed;
    }
}
