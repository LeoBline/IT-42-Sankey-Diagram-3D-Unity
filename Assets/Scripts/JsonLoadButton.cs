using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Net;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text;

public class JsonLoadButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void Click()
    {
        //open file panel
        string filePath = EditorUtility.OpenFilePanel("Load Json File", Application.streamingAssetsPath, "json");
        if (filePath.Length != 0)
        {
            gameObject.transform.parent.Find("JsonReader").GetComponent<JsonReaderTest>().loadDate(filePath);
            if (filePath.EndsWith(".json"))
            {
                gameObject.transform.parent.GetComponent<NodeShow>().continulFlag = true;
            }

        }
    }


}
