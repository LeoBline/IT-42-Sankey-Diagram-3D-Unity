using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class Name: ButtonControl
/// Author: Boyan Wei
/// Description: use button click, to control the Sankey diaram display in 4 modes.
/// </summary>
public class ButtonControl : MonoBehaviour
{
    public Button justify;
    public Button right;
    public Button left;
    public Button center;
    public Button butObject;
    /// <summary>
    /// Function Name: Click
    /// Description: If one of button in RightButton, JustifyButton, CenterButton and LefButton is clicked,
    ///             the display of the Sankey diagram will change into the same align mode.
    /// </summary>
    public void Click()
    {
        string buttonName = butObject.name.ToString();
        switch (buttonName)
        {
            case "RightButton":
                justify.interactable = false;
                justify.interactable = true;
                GameObject.Find("JustifyButton").SendMessage("ChangeChoice");
                center.interactable = false;
                center.interactable = true;
                GameObject.Find("CenterButton").SendMessage("ChangeChoice");
                left.interactable = false;
                left.interactable = true;
                GameObject.Find("LefButton").SendMessage("ChangeChoice");
                break;
            case "JustifyButton":
                right.interactable = false;
                right.interactable = true;
                GameObject.Find("RightButton").SendMessage("ChangeChoice");
                center.interactable = false;
                center.interactable = true;
                GameObject.Find("CenterButton").SendMessage("ChangeChoice");
                left.interactable = false;
                left.interactable = true;
                GameObject.Find("LefButton").SendMessage("ChangeChoice");
                break;
            case "CenterButton":
                justify.interactable = false;
                justify.interactable = true;
                GameObject.Find("JustifyButton").SendMessage("ChangeChoice");
                right.interactable = false;
                right.interactable = true;
                GameObject.Find("RightButton").SendMessage("ChangeChoice");
                left.interactable = false;
                left.interactable = true;
                GameObject.Find("LefButton").SendMessage("ChangeChoice");
                break;
            case "LefButton":
                justify.interactable = false;
                justify.interactable = true;
                GameObject.Find("JustifyButton").SendMessage("ChangeChoice");
                center.interactable = false;
                center.interactable = true;
                GameObject.Find("CenterButton").SendMessage("ChangeChoice");
                right.interactable = false;
                right.interactable = true;
                GameObject.Find("RightButton").SendMessage("ChangeChoice");
                break;
        }
    }
}
