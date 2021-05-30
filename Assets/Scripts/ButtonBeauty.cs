using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBeauty : MonoBehaviour
{
    public GameObject panel;
    public GameObject buttonObj;
    public NodeShow nodeShow;
    public GameObject player;
    private Vector3 oldpostion;
    public static bool hoverFunction = false;
    //true: hover
    //false: drag
    public static bool hoverOrdrag = true;
    public bool choice = false;
    void Update()
    {
        if(choice == false)
        {
            buttonObj.GetComponent<Image>().color = new Color(222 / 255f, 222 / 255f, 222 / 255f, 255 / 255f);
        }
    }
    public void ChangeChoice()
    {
        if (choice)
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
            if (gameObject.transform.name.ToString().Equals("VerticalViewingAngle"))
            {
                Debug.Log("veerticalview");
                oldpostion = player.transform.position;
                player.transform.position = new Vector3(346, 83, -378);
                
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
            if (gameObject.transform.name.ToString().Equals("VerticalViewingAngle"))
            {
                
                player.transform.position = oldpostion;

            }
            choice = false;
        }
    }
    
}
