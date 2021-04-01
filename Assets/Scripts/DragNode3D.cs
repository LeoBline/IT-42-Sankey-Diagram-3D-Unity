using UnityEngine;

public class DragNode3D : MonoBehaviour
{
    
    private Vector3 mOffset;
    private float mZCoord;
    void OnMouseDown()
    {
        
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //store offset = Gameobject world position - mouse world position
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        //z coordinate of game object on screen
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
}
