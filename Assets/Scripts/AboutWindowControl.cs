using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutWindowControl : MonoBehaviour
{
    public GameObject button;
    public GameObject helppanel;
    private bool choice = false;
    // Start is called before the first frame update
    void Start()
    {
        helppanel.SetActive(false);
    }     

    /** 
     * button click action listener 
     */
    public void Click()
    {
        if(choice == false)
        {
            helppanel.SetActive(true);
            choice = true;
        }
        else
        {
            helppanel.SetActive(false);
            choice = false;
        }
    }
}
