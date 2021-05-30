using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.EventSystems;
public class ClearlyShow : MonoBehaviour
{
    //This class is designed to when the cursor hover a node or a link, the relative node and link will become more clearly
    //It aims to make the Sankey Diagram more easy to read
    //[DllImport("user32.dll")]
    //public static extern int SetCursorPos(int x, int y);
    public static bool showLink = true;
    public Material Bio;
    public Material Elec;
    public Material Hy;
    public Material Coal;
    public Material Solar;
    public Material Oil;
    public Material others;
    private GameObject lastNode;
    public static bool hover = false;
    public static List<String> targetNameList = new List<String>();
    public static List<String> linkList = new List<string>();
    public static List<String> showLinkName = new List<string>();
    public static string lastNodeName = "";
    public void Start()
    {
        UnityEngine.Object obj1 = AssetDatabase.LoadAllAssetsAtPath("Assets/image/BioMaterial.mat")[0];
        Bio = obj1 as Material;
        UnityEngine.Object obj2 = AssetDatabase.LoadAllAssetsAtPath("Assets/image/ElecMaterial.mat")[0];
        Elec = obj2 as Material;
        UnityEngine.Object obj3 = AssetDatabase.LoadAllAssetsAtPath("Assets/image/HyMaterial.mat")[0];
        Hy = obj3 as Material;
        UnityEngine.Object obj4 = AssetDatabase.LoadAllAssetsAtPath("Assets/image/CoalMaterial.mat")[0];
        Coal = obj4 as Material;
        UnityEngine.Object obj5 = AssetDatabase.LoadAllAssetsAtPath("Assets/image/SolarMaterial.mat")[0];
        Solar = obj5 as Material;
        UnityEngine.Object obj6 = AssetDatabase.LoadAllAssetsAtPath("Assets/image/OilMaterial.mat")[0];
        Oil = obj6 as Material;
        UnityEngine.Object obj7 = AssetDatabase.LoadAllAssetsAtPath("Assets/image/OtherMaterial.mat")[0];
        others = obj7 as Material;
    }

    public void Click()
    {
        
        if(ButtonBeauty.hoverOrdrag == false)
        {
            Recover();
            
        }
        if (gameObject.transform.name.ToString().Equals("HoverButton") && ButtonBeauty.hoverFunction == false)
        {
            Debug.Log("recover");
            
            GameObject a = GameObject.Find("graphContainer");
            if (lastNode = GameObject.Find(lastNodeName))
            {
                DownTheNode(lastNode);
                lastNodeName = "";
            }
            for (int i=0;i<a.transform.childCount;i++)
            {
                if(a.transform.GetChild(i).name.Contains("&"))
                {
                    a.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                    Debug.Log("Chile "+ a.transform.GetChild(i).name + "   "+ a.transform.GetChild(i).GetComponent<MeshRenderer>().material.color);
                }
            }
            Recover();

        }
    }

    private void Recover()
    {
        showLink = true;
        List<GameObject> CubeArray = getCube();
        foreach (GameObject gb in CubeArray)
        {
            //Debug.Log("--------------------------------");
            ////set the cube to the orignal material
            if (gb.name.Contains("Coal"))
            {
                gb.GetComponent<MeshRenderer>().material = Coal;
            }
            else if (gb.name.Contains("H") || gb.name.Contains("Gas") || gb.name.Contains("Wind"))
            {
                gb.GetComponent<MeshRenderer>().material = Hy;
            }
            else if (gb.name.Contains("Solar"))
            {
                gb.GetComponent<MeshRenderer>().material = Solar;
            }
            else if (gb.name.Contains("Oil"))
            {
                gb.GetComponent<MeshRenderer>().material = Oil;
            }
            else if (gb.name.Contains("Elec"))
            {
                gb.GetComponent<MeshRenderer>().material = Elec;
            }
            else
            {
                gb.GetComponent<MeshRenderer>().material = others;
            }
        }
    }
    //当鼠标悬停时两个node和一个link高亮并且抬高显示
    public void OnMouseOver()
    {
        if (ButtonBeauty.hoverOrdrag && ButtonBeauty.hoverFunction)
        {
            hover = true;
            if (lastNodeName == "" || lastNodeName.Equals(gameObject.name.ToString()) == false)
            {
               // Debug.Log("comprision: " + lastNodeName.Equals(""));
                if (lastNodeName.Equals(gameObject.name.ToString()) == false && lastNodeName != "")
                {
                    //Debug.Log("changechange");
                    lastNode = GameObject.Find(lastNodeName);
                    DownTheNode(lastNode);
                    showLinkName.Clear();
                }
                showLink = false;
                float OrdXCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).x;
                float OrdYCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).y;
                float OrdZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                //Debug.Log(gameObject.tag.ToString());
                if (gameObject.tag.ToString() == "Cube")
                {
                    gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(OrdXCoord, OrdYCoord + 100, OrdZCoord));
                    string NodeObjectName = gameObject.name;
                    lastNodeName = NodeObjectName;
                    //Debug.Log("LastNodeName" + lastNodeName);
                    string NodeName = NodeObjectName.Split('@')[0];
                    if (NodeName.Contains("Coal"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material = Coal;
                    }
                    else if (NodeName.Contains("H") || NodeName.Contains("Gas") || NodeName.Contains("Wind"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material = Hy;
                    }
                    else if (NodeName.Contains("Solar"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material = Solar;
                    }
                    else if (NodeName.Contains("Oil"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material = Oil;
                    }
                    else if (NodeName.Contains("Elec"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material = Elec;
                    }
                    else
                    {
                        gameObject.GetComponent<MeshRenderer>().material = others;
                    }
                    List<GameObject> LinkArray = getLink();
                    string targetNodeName = "";
                    //up link
                    foreach (GameObject gb in LinkArray)
                    {
                        if (gb.name.ToString().Split('@')[0].Equals(NodeName))
                        {
                            linkList.Add(gb.name);
                            showLinkName.Add(gb.name.ToString());
                            int nameLength = gb.name.ToString().Split('@').Length;
                            string temptargetNodeName = gb.name.ToString().Split('@')[nameLength - 2];
                            targetNodeName = temptargetNodeName.Split('&')[1];
                            targetNameList.Add(targetNodeName);
                        }
                    }
                    Debug.Log("The first targetNodeList: " + targetNameList.Count);
                    //up node
                    List<GameObject> CubeArray = getCube();
                    foreach (GameObject gb in CubeArray)
                    {
                        string targetName = gb.name.ToString().Split('@')[0];
                        bool flagUp = false;
                        for(int i =0; i< targetNameList.Count; i++)
                        {
                            if (targetName.Equals(targetNameList[i]))
                            {
                                flagUp = true;
                                Debug.Log("UP End NODE +100"+ gb.name.ToString().Split('@')[0]);
                                float CubeOrdXCoord = Camera.main.WorldToScreenPoint(gb.transform.position).x;
                                float CubeOrdYCoord = Camera.main.WorldToScreenPoint(gb.transform.position).y;
                                float CubeOrdZCoord = Camera.main.WorldToScreenPoint(gb.transform.position).z;
                                gb.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(CubeOrdXCoord, CubeOrdYCoord + 100, CubeOrdZCoord));
                                if (targetName.Contains("Coal"))
                                {
                                    gb.GetComponent<MeshRenderer>().material = Coal;
                                }
                                else if (targetName.Contains("H") || targetName.Contains("Gas") || targetName.Contains("Wind"))
                                {
                                    gb.GetComponent<MeshRenderer>().material = Hy;
                                }
                                else if (targetName.Contains("Solar"))
                                {
                                    gb.GetComponent<MeshRenderer>().material = Solar;
                                }
                                else if (targetName.Contains("Oil"))
                                {
                                    gb.GetComponent<MeshRenderer>().material = Oil;
                                }
                                else if (targetName.Contains("Elec"))
                                {
                                    gb.GetComponent<MeshRenderer>().material = Elec;
                                }
                                else
                                {
                                    gb.GetComponent<MeshRenderer>().material = others;
                                }
                            }
                        }
                            if(!flagUp)
                            {
                                //Debug.Log("--------------------------------");
                                //Debug.Log("gbName: " + gb.name.ToString());
                                //Debug.Log("NodeName: " + NodeName);
                                //targetNameList
                                if (gb.name.ToString().Equals(NodeObjectName) == false)
                                {
                                    Material material = new Material(Shader.Find("Transparent/Diffuse"));
                                    material.color = new Color(1 / 255f, 1 / 255f, 1 / 255f, 10 / 255f);
                                    gb.GetComponent<Renderer>().material = material;
                                }
                            //gb.GetComponent<Renderer>().material.color = new Color(1 / 255f, 1 / 255f, 1 / 255f, 10 / 255f);
                        }

                        
                    }
                }
            }
        }
    }

    private List<GameObject> getLink()
    {
        List<GameObject> LinkArray = new List<GameObject>();
        GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (GameObject gb in all)
        {
            if (gb.tag.ToString().Equals("Link"))
            {
                LinkArray.Add(gb);
            }
        }
        // Debug.Log(LinkArray.Count);
        return LinkArray;
    }

    private List<GameObject> getCube()
    {
        List<GameObject> CubeArray = new List<GameObject>();
        GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (GameObject gb in all)
        {
            if (gb.tag.ToString().Equals("Cube"))
            {
                CubeArray.Add(gb);
            }
        }
        //Debug.Log(CubeArray.Count);
        return CubeArray;
    }



    //当鼠标移动到下一个node的时候把上一次移动上去的节点移下来
    private void DownTheNode(GameObject lastNode)
    {
        Debug.Log("The second list number" + targetNameList.Count);
        showLink = false;
        float OrdXCoord = Camera.main.WorldToScreenPoint(lastNode.transform.position).x;
        float OrdYCoord = Camera.main.WorldToScreenPoint(lastNode.transform.position).y;
        float OrdZCoord = Camera.main.WorldToScreenPoint(lastNode.transform.position).z;
        //Debug.Log(lastNode.name.ToString());
        if (lastNode.tag.ToString() == "Cube")
        {
            lastNode.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(OrdXCoord, OrdYCoord - 100, OrdZCoord));
            string NodeObjectName = lastNode.name;
            string NodeName = NodeObjectName.Split('@')[0];
            List<GameObject> LinkArray = getLink();
            string targetNodeName = "";
            //up link
            hover = true;
            //foreach (GameObject gb in LinkArray)
            //{
            //    if (gb.name.ToString().Split('@')[0].Equals(NodeName))
            //    {
            //        showLinkName = gb.name.ToString();
            //        int nameLength = gb.name.ToString().Split('@').Length;
            //        string temptargetNodeName = gb.name.ToString().Split('@')[nameLength - 2];
            //        targetNodeName = temptargetNodeName.Split('&')[1];
            //    }
            //}
            //down target node
            List<GameObject> CubeArray = getCube();
            //Debug.Log(targetNodeName);
            Debug.Log("=====================================");
            foreach (GameObject gb in CubeArray)
            {
                string targetName = gb.name.ToString().Split('@')[0];
                for(int i = 0; i < targetNameList.Count; i++)
                {
                    Debug.Log("Orignal: "+targetNameList[i]);
                    if (targetName.Equals(targetNameList[i]))
                    {
                        Debug.Log("the targetName equals with the node's name");
                        Debug.Log("targetName:  "+targetName);
                        float CubeOrdXCoord = Camera.main.WorldToScreenPoint(gb.transform.position).x;
                        float CubeOrdYCoord = Camera.main.WorldToScreenPoint(gb.transform.position).y;
                        float CubeOrdZCoord = Camera.main.WorldToScreenPoint(gb.transform.position).z;
                        gb.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(CubeOrdXCoord, CubeOrdYCoord - 100, CubeOrdZCoord));
                    }
                }
            }
            targetNameList.Clear();
            linkList.Clear();
        }
    }
}
