using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

// when save button clicked, generate a picture for the cammara view
public class SaveButtonControl : MonoBehaviour
{
    public Camera myCamera;
    private static ScreenshotHandler instance;
    private string filePath;
    private int height;
    private int width;

    public void Click()
    {
        Debug.Log("SaveButton clicked");
        screenShots(myCamera.rect);      
    }

    private void screenShots(Rect rect)
    {
        // get screenshot into picture
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, -1);
       
        myCamera.targetTexture = rt;// set camera's targetTexture as rt
        myCamera.Render();// render camera's targetTexture
        RenderTexture.active = rt;// active renderTexture
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        // generate the picture into png form
        byte[] bytes = screenShot.EncodeToPNG();


        // save the picture
        string filePath = UnityEditor.EditorUtility.SaveFilePanel("Load Json File", UnityEngine.Application.streamingAssetsPath, "CameraScreenshot.png", "png");
        System.IO.File.WriteAllBytes(filePath, bytes);
        // reset the value of attributes above
        myCamera.targetTexture = null;
        RenderTexture.active = null;
        GameObject.Destroy(rt);
    }
}

