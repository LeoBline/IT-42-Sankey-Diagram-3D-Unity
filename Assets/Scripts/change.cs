using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        localscal1 = new Vector3(localscal.x, 0, localscal.z);
        localposition = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        hight = this.transform.position.y;
        Debug.Log("Position "+hight);
        y = localscal.y;
        texty = this.transform.GetChild(0).GetChild(1).transform.localScale;
        GameLineObjectList = this.transform.parent.GetComponent<NodeShow>().GameLineObjectList;
        flog = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flog)
        {
            if (i >= 0.02f)
            {
                if (localscal1.y < y)
                {
                    localscal1.y += 1;
                    if(localscal1.y > y)
                    {
                        localscal1.y = y;
                    }
                    localposition.y += 0.5f;
                    GameObject.GetComponent<Transform>().localScale = localscal1;
                    for (int i = 1;  i < this.transform.GetChild(0).transform.childCount; i++)
                    {
                        this.transform.GetChild(0).GetChild(i).localScale = texty;
                    }
                    GameObject.GetComponent<Transform>().position = localposition;
                    i = 0;
                }
                else
                {
                    i += Time.deltaTime;
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



