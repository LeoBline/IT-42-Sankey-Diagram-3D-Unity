using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class Name: CameraController
/// Author: Yidan Lou
/// Description: make the player camera movement can controled by the keyboard 
/// </summary>
public class CameraController : MonoBehaviour
{
    public Transform player;
    private float mouseX, mouseY;
    public float mouseSensitivity;
    public float xRotation;
    private void Update()
    {
        if(Controlcursor.Cursorvisiable == true)
        {
            // get mouse movement
            mouseX = Input.GetAxis("Mouse X") * 0 * 0;
            mouseY = Input.GetAxis("Mouse Y") * 0 * 0;
            // set current movement rotation
            xRotation -= mouseY; 
            xRotation = Mathf.Clamp(xRotation, -70f, 70f);
            player.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
        else // if the cursor is not visiable
        {
            // get mouse movement
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            // set current movement rotation
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -70f, 70f);
            player.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
        
    }
    /// <summary>
    /// Function name: PlayerVertical
    /// Description: A function for reset the player camera to position(0,0,0), when vertical camera is open.
    ///              It is not used right now.
    /// </summary>
    public void PlayerVertical()
    {
        player.position = new Vector3(0,0,0);
    }
}
