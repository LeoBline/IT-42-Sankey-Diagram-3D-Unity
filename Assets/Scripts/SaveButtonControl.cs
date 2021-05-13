using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

// when save button clicked, generate a picture for the cammara view
public class SaveButtonControl : MonoBehaviour
{
 
    public void Click()
    {
        Debug.Log(GameObject.Find("Camera"));
        Camera camera = GameObject.Find("Camera").GetComponent<Camera>();

        // camera.clearFlags = CameraClearFlags.Nothing;
        ScreenshotHandler screenshot = new ScreenshotHandler(camera);
        screenshot.TakeScreenshot_Static(1070, 800);     
    }

   
}

