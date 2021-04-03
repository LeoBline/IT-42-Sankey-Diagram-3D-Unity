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
    public static string showLinkName = "";
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
    //private void Update()
    //{
    //    if (ButtonBeauty.hoverOrdrag == false)
    //    {
    //        Recover();
    //    }
    //}

    public void Click()
    {
        if(ButtonBeauty.hoverOrdrag == false)
        {
            Recover();
        }
    }

    private void Recover()
    {
        showLink = true;
        List<GameObject> CubeArray = getCube();
        foreach (GameObject gb in CubeArray)
        {
            Debug.Log("--------------------------------");
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
        if (ButtonBeauty.hoverOrdrag)
        {
            if (lastNodeName == "" || lastNodeName.Equals(gameObject.name.ToString()) == false)
            {
                Debug.Log("comprision: " + lastNodeName.Equals(""));
                if (lastNodeName.Equals(gameObject.name.ToString()) == false && lastNodeName != "")
                {
                    Debug.Log("changechange");
                    lastNode = GameObject.Find(lastNodeName);
                    DownTheNode(lastNode);
                }
                showLink = false;
                float OrdXCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).x;
                float OrdYCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).y;
                float OrdZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                Debug.Log(gameObject.tag.ToString());
                if (gameObject.tag.ToString() == "Cube")
                {
                    gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(OrdXCoord, OrdYCoord + 100, OrdZCoord));
                    string NodeObjectName = gameObject.name;

                    lastNodeName = NodeObjectName;
                    Debug.Log("LastNodeName" + lastNodeName);
                    string NodeName = NodeObjectName.Split('@')[0];
                    List<GameObject> LinkArray = getLink();
                    string targetNodeName = "";
                    //up link
                    foreach (GameObject gb in LinkArray)
                    {
                        //Debug.Log("====================================================");
                        if (gb.name.ToString().Split('@')[0].Equals(NodeName))
                        {
                            showLinkName = gb.name.ToString();
                            float linkOrdXCoord = Camera.main.WorldToScreenPoint(gb.transform.position).x;
                            float linkOrdYCoord = Camera.main.WorldToScreenPoint(gb.transform.position).y;
                            float linkOrdZCoord = Camera.main.WorldToScreenPoint(gb.transform.position).z;
                            gb.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(linkOrdXCoord, linkOrdYCoord + 100, linkOrdZCoord));
                            int nameLength = gb.name.ToString().Split('@').Length;
                            string temptargetNodeName = gb.name.ToString().Split('@')[nameLength - 2];
                            targetNodeName = temptargetNodeName.Split('&')[1];

                            //Debug.Log(targetNodeName);
                        }

                    }
                    //up node
                    List<GameObject> CubeArray = getCube();
                    foreach (GameObject gb in CubeArray)
                    {

                        string targetName = gb.name.ToString().Split('@')[0];
                        // Debug.Log(targetName);
                        if (targetName.Equals(targetNodeName))
                        {
                            //Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++=");
                            float CubeOrdXCoord = Camera.main.WorldToScreenPoint(gb.transform.position).x;
                            float CubeOrdYCoord = Camera.main.WorldToScreenPoint(gb.transform.position).y;
                            float CubeOrdZCoord = Camera.main.WorldToScreenPoint(gb.transform.position).z;
                            gb.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(CubeOrdXCoord, CubeOrdYCoord + 100, CubeOrdZCoord));
                        }
                        else
                        {
                            //set the other cube be transparet
                            Debug.Log("--------------------------------");
                            Debug.Log("gbName: " + gb.name.ToString());
                            Debug.Log("NodeName: " + NodeName);
                            if (gb.name.ToString().Equals(NodeObjectName) == false)
                            {
                                Material material = new Material(Shader.Find("Transparent/Diffuse"));
                                material.color = new Color(1 / 255f, 1 / 255f, 1 / 255f, 10 / 255f);
                                gb.GetComponent<Renderer>().material = material;
                            }
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
        Debug.Log(CubeArray.Count);
        return CubeArray;
    }



    //当鼠标离开时三个组件都要放下
    private void DownTheNode(GameObject lastNode)
    {
        Debug.Log("111111111111111111111111111");
        showLink = true;
        float OrdXCoord = Camera.main.WorldToScreenPoint(lastNode.transform.position).x;
        float OrdYCoord = Camera.main.WorldToScreenPoint(lastNode.transform.position).y;
        float OrdZCoord = Camera.main.WorldToScreenPoint(lastNode.transform.position).z;
        Debug.Log(lastNode.tag.ToString());
        if (lastNode.tag.ToString() == "Cube")
        {
            lastNode.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(OrdXCoord, OrdYCoord - 100, OrdZCoord));
            string NodeObjectName = lastNode.name;
            string NodeName = NodeObjectName.Split('@')[0];
            List<GameObject> LinkArray = getLink();
            string targetNodeName = "";
            //up link
            foreach (GameObject gb in LinkArray)
            {
                //Debug.Log("====================================================");
                if (gb.name.ToString().Split('@')[0].Equals(NodeName))
                {
                    showLinkName = gb.name.ToString();
                    float linkOrdXCoord = Camera.main.WorldToScreenPoint(gb.transform.position).x;
                    float linkOrdYCoord = Camera.main.WorldToScreenPoint(gb.transform.position).y;
                    float linkOrdZCoord = Camera.main.WorldToScreenPoint(gb.transform.position).z;
                    gb.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(linkOrdXCoord, linkOrdYCoord - 100, linkOrdZCoord));
                    int nameLength = gb.name.ToString().Split('@').Length;
                    string temptargetNodeName = gb.name.ToString().Split('@')[nameLength - 2];
                    targetNodeName = temptargetNodeName.Split('&')[1];
                    //Debug.Log(targetNodeName);
                }

            }
            //up node
            List<GameObject> CubeArray = getCube();
            foreach (GameObject gb in CubeArray)
            {

                string targetName = gb.name.ToString().Split('@')[0];
                // Debug.Log(targetName);
                if (targetName.Equals(targetNodeName))
                {
                    //Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++=");
                    float CubeOrdXCoord = Camera.main.WorldToScreenPoint(gb.transform.position).x;
                    float CubeOrdYCoord = Camera.main.WorldToScreenPoint(gb.transform.position).y;
                    float CubeOrdZCoord = Camera.main.WorldToScreenPoint(gb.transform.position).z;
                    gb.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(CubeOrdXCoord, CubeOrdYCoord - 100, CubeOrdZCoord));
                }
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
                else if (gb .name.Contains("Oil"))
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
    }
}
