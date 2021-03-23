using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControltheNameAndInformation : MonoBehaviour
{
    public GameObject NamePanel;
    public GameObject InformationPanel;
    private bool choice =  false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        if(choice == false)
        {
            NamePanel.SetActive(false);
            InformationPanel.SetActive(false);
            choice = true;
        }
        else
        {
            NamePanel.SetActive(true);
            InformationPanel.SetActive(true);
            choice = false;
        }
    }

}
