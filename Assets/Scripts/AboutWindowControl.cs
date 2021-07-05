
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class Name: AboutWindowControl 
/// Author Nmae: Boyan Wei
/// Class Function: This class control whether the about window shows.
/// help panel: help panel is the game object of about window
/// button: button is the button in canvas that control the about window
/// choice: the state of the about window button 
/// </summary>
public class AboutWindowControl : MonoBehaviour
{
    public GameObject button;
    public GameObject helppanel;
    private bool choice = false;
    void Start()
    {
        //At begining, the about window do not show.
        //So, setting the state of about window to false (not show).
        helppanel.SetActive(false);
    }

    /// <summary>
    /// Function Nmae: Click
    /// Description: If the AboutButton in ButtonPanel is clicked, 
    /// the AboutPanel will appear or disappear
    /// </summary>
    public void Click()
    {
        if (choice == false)
        {
            //when about window does not show, after clicking the button, setting the state to true, and show the window.
            helppanel.SetActive(true);
            //set the global variables
            choice = true;
        }
        else
        {
            //when the about window have shown, after clicking the button, setting the state to false, and close the window.
            helppanel.SetActive(false);
            choice = false;
        }
    }
}
