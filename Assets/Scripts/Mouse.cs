using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           Screen.lockCursor = true;
        
    }
    private void OnMouseEnter()
    {
        string s = gameObject.name;
        string[] sArray = s.Split('@');

        transform.parent.parent.parent.Find("cameraCanvas").Find("Name").Find("NameText").GetComponent<Text>().text = sArray[0];
        transform.parent.parent.parent.Find("cameraCanvas").Find("Information").Find("EnergyL").GetComponent<Text>().text = sArray[1];
    }
    private void OnMouseExit()
    {
        transform.parent.parent.parent.Find("cameraCanvas").Find("Name").Find("NameText").GetComponent<Text>().text = "";
        transform.parent.parent.parent.Find("cameraCanvas").Find("Information").Find("EnergyL").GetComponent<Text>().text ="";

    }
}
