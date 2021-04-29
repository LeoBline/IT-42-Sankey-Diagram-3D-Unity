using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlcursor : MonoBehaviour
{
    
    public static bool Cursorvisiable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        //Controlling the move view function of the cursor
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(Cursorvisiable == false)
            {
                Cursor.visible = true;
                Debug.Log("visiable");
                Cursorvisiable = true;
                Screen.lockCursor = false;

            }
            else
            {
                Cursor.visible = false;
                Debug.Log("Unvisiable");
                Cursorvisiable = false;
                Screen.lockCursor = true;
            }
        }
    }
}
