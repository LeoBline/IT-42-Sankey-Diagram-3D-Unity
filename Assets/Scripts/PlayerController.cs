using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class Name: PlayerController
/// Author: Yidan Lou
/// Description: This class is to allow the user control the player camera movement. The user 
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController cc;
    public float moveSpeed;
    public float jumpSpeed;
    private bool verticalFlag = false;
    int time = 0;

    private float horizontalMove, verticalMove, yMove;
    private Vector3 dir;


    private void Start()
    {
        cc = GetComponent<CharacterController>();

    }
    private void Update()
    {

        if (verticalFlag) // the verticalFlag
        { 
            changepostion();
            time++;
            if (time == 3)
            {
                verticalFlag = false;
                time = 0;
            }
        }
        else
        {
            // get keyboard input: move back, forward, left, right
            horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
            verticalMove = Input.GetAxis("Vertical") * moveSpeed;

            // use input information to calculate transform 
            dir = transform.forward * verticalMove + transform.right * horizontalMove;
            cc.Move(dir * Time.deltaTime);

            // get keyboard input: move raise up and down
            yMove = Input.GetAxis("Jump") * jumpSpeed;
            cc.Move(new Vector3(0, yMove * Time.deltaTime, 0));
        }
    }
    
    /**
     * reset the positon of the player camera for debug
     */
    public void changepostion()
    {
        this.transform.position = new Vector3(333, -150, -373);
    }

    /**
     * set for debug
     */
    public void changeFlag()
    {
        verticalFlag = true;
    }

}
