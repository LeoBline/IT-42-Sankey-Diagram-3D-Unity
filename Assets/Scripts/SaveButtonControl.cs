using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

// when save button clicked, generate a picture for the cammara view
public class SaveButtonControl : MonoBehaviour
{
    public Camera myCamera;
    private string filePath;
    private bool isEnableAlpha = false;

    public void Click()
    {
        Debug.Log("SaveButton clicked");
        int resolutionX = (int)Handles.GetMainGameViewSize().x;
        int resolutionY = (int)Handles.GetMainGameViewSize().y;
        RenderTexture rt = new RenderTexture(resolutionX, resolutionY, 24);



    }
}
