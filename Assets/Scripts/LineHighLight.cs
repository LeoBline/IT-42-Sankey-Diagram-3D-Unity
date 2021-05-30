using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineHighLight : MonoBehaviour
{
    Color oldcolor;
    bool stilloverflag = false;
    /// <summary>
    /// 鼠标指向模型时触发
    /// </summary>
    private void OnMouseOver()
    {
        if (!stilloverflag)
        {
            oldcolor = this.GetComponent<MeshRenderer>().material.color;

            Debug.Log("mouse over line");
            this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0.5f, 1.0f, 0.99f);

            string s = gameObject.name;
            string[] sArray = s.Split('/');

            transform.parent.parent.parent.Find("cameraCanvas").Find("Name").Find("NameText").GetComponent<Text>().text = sArray[0];
            transform.parent.parent.parent.Find("cameraCanvas").Find("Information").Find("EnergyL").GetComponent<Text>().text = sArray[1];
            stilloverflag = true;

        }
        
    }

    /// <summary>
    /// 鼠标离开模型时触发
    /// </summary>
    private void OnMouseExit()
    {
        stilloverflag = false;
        //关闭外发光
        this.GetComponent<MeshRenderer>().material.color = oldcolor;


        transform.parent.parent.parent.Find("cameraCanvas").Find("Name").Find("NameText").GetComponent<Text>().text = "";
        transform.parent.parent.parent.Find("cameraCanvas").Find("Information").Find("EnergyL").GetComponent<Text>().text = "";
    }
}
