using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class Name: ClearlyShow
/// Author: Boyan Wei
/// Description: This class is to control the cursor appear and disappear.
/// When the cursor is hiden, the view prespective can move with the cursor.
/// </summary>
public class Controlcursor : MonoBehaviour
{
    public static bool Cursorvisiable = true;
    void Start()
    {

    }
    void Update()
    {
        //Controlling the move view function of the cursor
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Cursorvisiable == false)
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
