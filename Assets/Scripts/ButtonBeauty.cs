﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBeauty : MonoBehaviour
{
    public GameObject panel;
    public GameObject buttonObj;
    public NodeShow nodeShow;
    public static bool hoverFunction = false;
    //true: hover
    //false: drag
    public static bool hoverOrdrag = true;
    private bool choice = false;
    void Start()
    {
    }
    void Update()
    {
        if(choice == false)
        {
            buttonObj.GetComponent<Image>().color = new Color(222 / 255f, 222 / 255f, 222 / 255f, 255 / 255f);
        }
    }
    public void Click()
    {
        if (choice == false)
        {
            Debug.Log("Click   ...");
            panel.GetComponent<Image>().color = new Color(0 / 255f, 113 / 255f, 255 / 255f, 255 / 255f);
            buttonObj.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            if (gameObject.transform.name.ToString().Equals("DragButton"))
            {
                //become drag from hover function
                hoverOrdrag = false;
            }
            if (gameObject.transform.name.ToString().Equals("HoverButton"))
            {
                //become drag from hover function
                hoverFunction = true;
            }
            if (gameObject.transform.name.ToString().Equals("LefButton"))
            {
                nodeShow.Align("left");
            }
            if (gameObject.transform.name.ToString().Equals("RightButton"))
            {
                nodeShow.Align("right");
            }
            if (gameObject.transform.name.ToString().Equals("CenterButton"))
            {
                nodeShow.Align("center");
            }
            if (gameObject.transform.name.ToString().Equals("JustifyButton"))
            {
                nodeShow.Align("justify");
            }
            choice = true;
        }
        else
        {
            Debug.Log("Unclick   ......");
            panel.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);
            buttonObj.GetComponent<Image>().color = new Color(222 / 255f, 222 / 255f, 222 / 255f, 255 / 255f);
            if (gameObject.transform.name.ToString().Equals("DragButton"))
            {
                //become hover;
                hoverOrdrag = true;
            }
            if (gameObject.transform.name.ToString().Equals("HoverButton"))
            {
                //become drag from hover function
                hoverFunction = false;
            }
            choice = false;
        }
    }
    
}
