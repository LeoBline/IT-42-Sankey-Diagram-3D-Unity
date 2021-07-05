using UnityEngine;
/**
 * Class name: 
 *      DragNode
 *      
 * Author: Yanxi Ke
 * 
 * Class description:
 *      This class improve drag function in 3D game scene
 */

public class DragNode3D : MonoBehaviour
{

    public static bool isClick = false;
    //Difference from previous position
    private Vector3 mOffset;
    private float mZCoord;
    //when the mous down on the node
    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //store offset = Gameobject world position - mouse world position
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        isClick = true;
    }
    //get mouse position
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        //z coordinate of game object on screen
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    // update the position
    void OnMouseDrag()
    {

        transform.position = GetMouseWorldPos() + mOffset;
    }
    // status flag
    private void OnMouseUp()
    {
        isClick = false;
    }
}
