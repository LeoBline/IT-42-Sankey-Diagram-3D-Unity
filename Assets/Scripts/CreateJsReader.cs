using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
 * Class name:
 *      CreateJsReader
 *      
 * Class description:
 *      This class extends Editor class to get the ablity to recive JSon File as input
 */
public class CreateJsReader : Editor
{
    /*
     * Method name: 
     *      Start
     * Method description: 
     *      This method is for create a JSon data reader, so that the system can save JSon data into data structure.
     *      It will be called before the first frame update.
     */
    void Start()
    {

        GameObject jsreader = new GameObject();
        jsreader.name = "JsonReader";
        jsreader.AddComponent<JsonReaderTest>();
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}

/**
 * Class name:
 *      Test
 *      
 * Class description:
 *      Another way to create a JSon data reader, so that the system can create reader at any time it needs.
 * 
 */
public static class Test
{
[MenuItem("GameObject/JSonReader", priority = 11)]
    static void Init()
    {
        GameObject jsreader = new GameObject();
        jsreader.name = "JsonReader";
        jsreader.AddComponent<JsonReaderTest>();
    }
}


