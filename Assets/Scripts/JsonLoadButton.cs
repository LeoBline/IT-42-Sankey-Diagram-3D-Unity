using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Net;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text;
/**
 * Class Name :  
 *     JsonLoadButton
 *     
 * Author: Boyan Wei
 * 
 * Class Description : 
 *     to control the local storage window open
 */
public class JsonLoadButton : MonoBehaviour
{
    public GameObject JsonReader;
    /// <summary>
    /// Function Name: Click
    /// Description: Call when click JsonLoadButton, open the local storage window and let user import Json data from the local storage
    /// </summary>
    public void Click()
    {
        //open file panel
        string filePath = EditorUtility.OpenFilePanel("Load Json File", Application.streamingAssetsPath, "json");
        if (filePath.Length != 0)
        {
            JsonReader.GetComponent<JsonReaderTest>().loadDate(filePath);
            if (filePath.EndsWith(".json"))
            {
                NodeShow.continulFlag = true;
            }

        }
    }


}
