using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Class Name :  
 *     HtmlJsonButton
 *     
 * Author: Boyan Wei
 * 
 * Class Description : 
 *     to control the url input window disappear and appear.
 */

public class HtmlJsonButton : MonoBehaviour
{
    public GameObject Diague;
    public Button button;
    void Start()
    {
        Diague.SetActive(false);
        button.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Function Name: onClick
    /// Description: Call when click htmlJsonButton, to control the url input window disappear and appear
    /// </summary>
    void onClick()
    {
        if (Diague.active == true) // Click to close Dialog
        {
            Diague.SetActive(false);
        }
        else // Click to open Dialog
        {
            Diague.SetActive(true);
        }

    }

}