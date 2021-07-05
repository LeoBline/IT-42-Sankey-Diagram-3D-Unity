using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class Name: Change
/// Author: Hongcong Zhu
/// Description: Control the animation of the nodes
/// </summary>
public class Change : MonoBehaviour
{
    float i;
    float y;
    Vector3 texty;
    GameObject GameObject;
    List<GameObject> GameLineObjectList;
    Vector3 localscal;
    Vector3 localscal1;
    Vector3 localposition;
    float hight;
    bool flog;

    void Start()
    {
        //Find the coordinates of the corresponding gameobject,let hight =0
        localscal1 = new Vector3(localscal.x, 0, localscal.z);
        localposition = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        hight = this.transform.position.y;
        Debug.Log("Position " + hight);
        y = localscal.y;
        texty = this.transform.GetChild(0).GetChild(1).transform.localScale;
        GameLineObjectList = this.transform.parent.GetComponent<NodeShow>().GameLineObjectList;
        flog = true;
    }

    void Update()
    {
        if (flog)
        {
            if (i >= 0.02f)
            {
                //When y has not reached the set height, complete y will increase proportionally
                if (localscal1.y < y)
                {
                    localscal1.y += 1;
                    if (localscal1.y > y)
                    {
                        localscal1.y = y;
                    }
                    localposition.y += 0.5f;
                    GameObject.GetComponent<Transform>().localScale = localscal1;
                    for (int i = 1; i < this.transform.GetChild(0).transform.childCount; i++)
                    {
                        this.transform.GetChild(0).GetChild(i).localScale = texty;
                    }
                    GameObject.GetComponent<Transform>().position = localposition;
                    i = 0;
                }
                else
                {
                    i += Time.deltaTime;
                    //When the animation of the bar gameobject is completed,
                    //set the gameobject of the corresponding link to be visible
                    if (i >= 5.0f)
                    {
                        for (int i = 0; i < GameLineObjectList.Count; i++)
                        {
                            GameLineObjectList[i].SetActive(true);
                        }
                        flog = false;
                    }
                }

            }

            i += Time.deltaTime;
        }
    }
    public void setGameObject(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    public void setLocal(Vector3 local)
    {
        localscal = local;
    }
}