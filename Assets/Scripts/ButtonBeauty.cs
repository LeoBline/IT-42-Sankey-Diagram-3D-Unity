using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBeauty : MonoBehaviour
{
    public GameObject panel;
    public GameObject buttonObj;
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
            choice = true;
        }
        else
        {
            Debug.Log("Unclick   ......");
            panel.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);
            buttonObj.GetComponent<Image>().color = new Color(222 / 255f, 222 / 255f, 222 / 255f, 255 / 255f);
            choice = false;
        }
    }
    
}
