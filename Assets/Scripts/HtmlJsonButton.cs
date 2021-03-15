using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HtmlJsonButton : MonoBehaviour
{
    public GameObject Diague;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        Diague.SetActive(false);
        button.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Call when click
    void onClick()
    {
        if (Diague.active == true)
        {
            Diague.SetActive(false);
            Debug.Log("Click to close Dialog");
        }
        else
        {
            Debug.Log("Click to open Dialog");
            Diague.SetActive(true);
        }

    }

}