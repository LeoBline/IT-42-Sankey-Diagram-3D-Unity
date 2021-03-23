using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    // Start is called before the first frame update
    public Transform player;
    private float mouseX, mouseY;
    public float mouseSensitivity;
    public float xRotation;
    private void Update()
    {
        if(Controlcursor.Cursorvisiable == true)
        {
            mouseX = Input.GetAxis("Mouse X") * 0 * 0;

            mouseY = Input.GetAxis("Mouse Y") * 0 * 0;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -70f, 70f);
            player.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
        else
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -70f, 70f);
            player.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
        
    }
}
