using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController cc;
    public float moveSpeed;
    public float jumpSpeed;

    private float horizontalMove, verticalMove,yMove;
    private Vector3 dir;


    private void Start()
    {
        cc = GetComponent<CharacterController>();

    }
    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        verticalMove = Input.GetAxis("Vertical")* moveSpeed;
        dir = transform.forward * verticalMove + transform.right * horizontalMove;
        cc.Move(dir * Time.deltaTime);
        yMove = Input.GetAxis("Jump") * jumpSpeed;
        cc.Move(new Vector3(0, yMove * Time.deltaTime,0));
        
    }
}
