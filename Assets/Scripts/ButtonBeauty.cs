using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class Name: ButtonBeauty
/// Author: Boyan Wei, Yidan Lou
/// Description: Set the apperance of the button 
/// </summary>

public class ButtonBeauty : MonoBehaviour
{
    public GameObject panel; // a blue panel on the button, when the button is clicked, the panel will show
    public GameObject buttonObj; // button object
    public NodeShow nodeShow; // an instance of NodeShow class
    public GameObject player; // player object
    private Vector3 oldpostion;
    public static bool hoverFunction = false; // whether the hover model is open

    public static bool hoverOrdrag = true; // which model is open, hover or drag, true: hover; false: drag
    public bool choice = false; // record the state of the button, true: button is choosen; false: button is not chosen

    public Camera playerCamera;
    public Camera VerticalCamera; 

    void Update()
    {
        //Set the color of button if the button is not clicked
        if (choice == false)
        {
            buttonObj.GetComponent<Image>().color = new Color(222 / 255f, 222 / 255f, 222 / 255f, 255 / 255f);
        }
    }

    public void ChangeChoice()
    {
        if (choice) // if the button is not chosen 
        {
            //set the apperance of button
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
            //set the apperance
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
            //set the align of Sankey diagram
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
            if (gameObject.transform.name.ToString().Equals("VarticalView"))
            {
                playerCamera.gameObject.SetActive(false); // close player camera
                VerticalCamera.gameObject.SetActive(true); // open vertical camera
                this.transform.parent.parent.GetComponent<Canvas>().worldCamera = VerticalCamera; // set UI to vertical camera, so that vertical view has the same control UI
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
            if (gameObject.transform.name.ToString().Equals("VarticalView"))
            {
                Debug.Log("veerticalview");
                VerticalCamera.gameObject.SetActive(false);
                playerCamera.gameObject.SetActive(true);
                this.transform.parent.parent.GetComponent<Canvas>().worldCamera = playerCamera;


            }
            choice = false;
        }
    }

}
