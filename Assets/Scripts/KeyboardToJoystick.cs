// SETUP INSTRUCTIONS
// ------------------
// 1. Put this file in your Scripts folder inside Assets
// 2. In the Hierarchy panel, find your XR Rig / Camera Rig object
//    (this is the object that represents your player in the scene)
// 3. With that object selected, click "Add Component" in the Inspector
// 4. Search for "KeyboardToJoystick" and select it
// 5. Press Play — WASD on the keyboard will now move you in the VR environment
//
// ALSO MAKE SURE:
// - This script is on your XR Rig, not just any GameObject
// ------------------

using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class KeyboardToJoystick : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f; // adjust to move faster or slower

    void Update()
    {
        // Read WASD keys
        float horizontal = 0f;
        float vertical   = 0f;

        if (Input.GetKey(KeyCode.W)) vertical   =  1f;
        if (Input.GetKey(KeyCode.S)) vertical   = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal =  1f;
        if (Input.GetKey(KeyCode.A)) horizontal = -1f;

        // Build a movement direction from the keyboard input
        Vector3 move = new Vector3(horizontal, 0, vertical);

        // Move the XR Rig in that direction
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.Self);
    }
}
