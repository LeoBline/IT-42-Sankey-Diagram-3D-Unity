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
    void Start()
    {
        transform.GetComponent<Button_UI>().ClickFunc = () =>
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
        };
    }

    // Update is called once per frame 
    void Update()
    {
        
    }
}
