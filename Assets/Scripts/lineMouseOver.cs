using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lineMouseOver : MonoBehaviour
{
    public string text="";
    public Vector2 position;
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseExit()
    {
        if (a != null)
        {
            a.SetActive(false);
        }
        LineRenderer line = gameObject.transform.parent.GetComponent<LineRenderer>(); ;
        line.SetColors(new Color(1, 1, 1, 0.15f), new Color(1, 1, 1, 0.15f));
    }
    private void OnMouseOver()
    {
        if (a != null)
        {
            a.SetActive(true);
        }
        LineRenderer line = gameObject.transform.parent.GetComponent<LineRenderer>(); ;
        line.SetColors(new Color(1, 1, 1, 0.70f), new Color(1, 1, 1, 0.70f));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
