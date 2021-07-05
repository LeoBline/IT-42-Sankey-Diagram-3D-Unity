using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class Name: ControltheNameAndInformation
/// Author: Boyan Wei
/// Description: This function control the Name and Information window. 
/// The name and information window is used to show the relative information of Sankey Nodes and Links.
/// </summary>
public class ControltheNameAndInformation : MonoBehaviour
{
    public GameObject NamePanel; // The window shows the name of nodes and links
    public GameObject InformationPanel; // The window shows the entire information of nodes and links. It includes the flow of the links, or the begin node and end node of nodes.
    private bool choice = false; // It records the state of button. false: button is not chosen; true: button is chosen

    void Start()
    {

    }
    void Update()
    {

    }
    public void Click()
    {
        if (choice == false) // is button is not chosen, then make it chosen
        {
            //when clicking the button and the button state is false, in other words, the window is showing, the function close windows.
            NamePanel.SetActive(false);
            InformationPanel.SetActive(false);
            //And record the state of windows.
            choice = true;
        }
        else
        {
            //show windows (name and information)
            NamePanel.SetActive(true);
            InformationPanel.SetActive(true);
            //record the state of windows
            choice = false;
        }
    }

}
