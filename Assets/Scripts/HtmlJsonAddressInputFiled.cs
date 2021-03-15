using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Text;
using LitJson;
using System;
/**
 * Class Name :  
 *     HtmlJsonAddressInputFiled

 * Class Description : 
 *     This class will get Json data from website through URL. 
 *     After getting data, it will store the Json data into the system data structure.
 */
public class HtmlJsonAddressInputFiled : MonoBehaviour
{

    public GameObject JsonReader;
    public GameObject graph;
    public string htmlAddress = "";
    public System.IO.StreamReader myReader;
    
    private void Awake()
    {
        var input = this.GetComponent<InputField>();
        if (input)
        {
            input.onEndEdit.AddListener(OnEndEdit);
        }
    }

    private System.IO.StreamReader Get(string url)
    {
        HttpWebRequest request;
        request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
        return myreader;
    }

    /*
     * Method name: OnEndEdit
     * Argument: string htmlAddress
     * Method description: catch static resource from input HTML address
     */
    private void OnEndEdit(string arg0)
    {
        this.htmlAddress = this.GetComponent<InputField>().text;
        if (this.htmlAddress != "" && this.htmlAddress.EndsWith(".json"))// when request file is valid
        {
            this.myReader = this.Get(htmlAddress);
            string content = myReader.ReadToEnd();// read in json data as string
            this.JsonReader.GetComponent<JsonReaderTest>().loadHtmlData(content);// use method in JsonReaderTest to read json data into system
            this.graph.GetComponent<NodeShow>().continulFlag = true;
        }
        else// when request file is invalid
        {
            Debug.Log("This Online file is not a Json file or the url is empty");
        }
    }
}
