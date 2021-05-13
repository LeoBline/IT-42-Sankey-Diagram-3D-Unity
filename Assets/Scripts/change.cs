using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    float i;
    float y;
    GameObject GameObject;
    List<GameObject> GameLineObjectList;
    Vector3 localscal;
    Vector3 localscal1;
    bool flog;


    // Start is called before the first frame update
    void Start()
    {
        localscal1 = new Vector3(localscal.x, 0, localscal.z);
        y = localscal.y;
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
                    GameObject.GetComponent<Transform>().localScale = localscal1;
                    i = 0;
                }
                else
                {
                    i += Time.deltaTime;
                    if (i >= 4.0f)
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



