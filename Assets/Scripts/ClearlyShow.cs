using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.EventSystems;
/// <summary>
/// Class Name: ClearlyShow
/// Author: Boyan Wei
/// Description: This class is designed to when the cursor hover a node or a link, the relative node and link will become more clearly
/// It aims to make the Sankey Diagram more easy to read.
/// </summary>
public class ClearlyShow : MonoBehaviour
{
    public static bool showLink = true; // the state of link. When a node have been hover on, the other links that do not have relationship with this node change the color. 
                                        // Bio, Elec, Hy, Coal, Solar, Oil, others: Materials of nodes
    public Material Bio;
    public Material Elec;
    public Material Hy;
    public Material Coal;
    public Material Solar;
    public Material Oil;
    public Material others;
    private GameObject lastNode;
    public static bool hover = false; // this variable record the state that node whether be hovered.
    public static List<String> targetNameList = new List<String>();
    public static List<String> linkList = new List<string>();
    public static List<String> showLinkName = new List<string>();
    public static string lastNodeName = "";

    /// <summary>
    /// Function Name: Start
    /// Description: Once the project start, set the material object to each material variable.
    /// </summary>
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

    /// <summary>
    /// Function Name: Click
    /// This function controls the button of hover function.
    /// When the state of button is clicked, the hover model will be open. 
    /// The cursor moves on the node, the node and the relative nodes and links will be highlighted.
    /// </summary>
    public void Click()
    {
        //when close the hover on model, the Sankey diagram will recover.
        //ButtonBeauty.hoverOrdrag is a boolean value that record the model state.
        if (ButtonBeauty.hoverOrdrag == false)
        {
            //recover the material of nodes
            Recover();
        }
        if (gameObject.transform.name.ToString().Equals("HoverButton") && ButtonBeauty.hoverFunction == false)
        {
            GameObject a = GameObject.Find("graphContainer");
            //if there is last node that have been move up, the node and relative objects will be recovered.
            if (lastNode = GameObject.Find(lastNodeName))
            {
                DownTheNode(lastNode);
                //clear the variable
                lastNodeName = "";
            }
            //Find all links in the graph container
            for (int i = 0; i < a.transform.childCount; i++)
            {
                //Only the links' name include "&"
                if (a.transform.GetChild(i).name.Contains("&"))
                {
                    //Change the links' color
                    a.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                }
            }
            Recover();
        }
    }

    /// <summary>
    /// Function Name: Recover
    /// Recover the materials of each node.
    /// Because the other nodes is transparent, after close the model or change the node that highlighted, 
    /// the others have to recover the material.
    /// </summary>
    private void Recover()
    {
        showLink = true;
        List<GameObject> CubeArray = getCube();
        foreach (GameObject gb in CubeArray)
        {
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
    /// <summary>
    /// Function Name: OnMouseOver
    /// Description: When cursor hover on a node, the node and link will highlight
    /// </summary>

    public void OnMouseOver()
    {
        //When the hover model is open
        if (ButtonBeauty.hoverOrdrag && ButtonBeauty.hoverFunction)
        {
            hover = true;
            //when the node is firstly hightlight or the node is different with the last node
            if (lastNodeName == "" || lastNodeName.Equals(gameObject.name.ToString()) == false)
            {

                if (lastNodeName.Equals(gameObject.name.ToString()) == false && lastNodeName != "")
                {

                    //when the node is different with the last node.
                    //At first, the function should down the last node and relative links.
                    lastNode = GameObject.Find(lastNodeName);
                    DownTheNode(lastNode);
                    showLinkName.Clear();
                }
                showLink = false;
                //Get the position(X,Y,Z) of the node.
                float OrdXCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).x;
                float OrdYCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).y;
                float OrdZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                //Debug.Log(gameObject.tag.ToString());
                //This function uses the tag to indentify the class of object
                if (gameObject.tag.ToString() == "Cube")
                {

                    //Move up the object
                    gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(OrdXCoord, OrdYCoord + 100, OrdZCoord));
                    string NodeObjectName = gameObject.name;
                    //change lastnodename string
                    lastNodeName = NodeObjectName;
                    // recover the highlight node's material
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
                    //Get all links
                    List<GameObject> LinkArray = getLink();
                    string targetNodeName = "";
                    //up link
                    foreach (GameObject gb in LinkArray)
                    {
                        //Find the link that source node' name is equal with the node name that highlight
                        if (gb.name.ToString().Split('@')[0].Equals(NodeName))
                        {
                            //Append the link to LinkList
                            linkList.Add(gb.name);
                            //Append the link to Show Link Name
                            showLinkName.Add(gb.name.ToString());
                            //Get target node name
                            int nameLength = gb.name.ToString().Split('@').Length;
                            string temptargetNodeName = gb.name.ToString().Split('@')[nameLength - 2];
                            targetNodeName = temptargetNodeName.Split('&')[1];
                            targetNameList.Add(targetNodeName);
                        }
                    }

                    //up node
                    //clearly show the relative node
                    List<GameObject> CubeArray = getCube();
                    foreach (GameObject gb in CubeArray)
                    {
                        string targetName = gb.name.ToString().Split('@')[0];
                        bool flagUp = false;
                        for (int i = 0; i < targetNameList.Count; i++)
                        {
                            if (targetName.Equals(targetNameList[i]))
                            {
                                flagUp = true;
                                //Debug.Log("UP End NODE +100"+ gb.name.ToString().Split('@')[0]);
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
                        if (!flagUp)
                        {
                           
                            if (gb.name.ToString().Equals(NodeObjectName) == false)
                            {
                                //The others node is transparent
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

    /// <summary>
    /// Function Name: getLink
    /// Description: return a list of GameObject instances that are all the links in Sankey diagram.
    ///              Get the all links in the graph container
    /// </summary>
    /// <returns> an array of all the link instence </returns>
    private List<GameObject> getLink()
    {
        List<GameObject> LinkArray = new List<GameObject>();
        GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (GameObject gb in all)
        {
            //use tag to identify
            if (gb.tag.ToString().Equals("Link"))
            {
                LinkArray.Add(gb);
            }
        }
        return LinkArray;
    }

    /// <summary>
    /// Function Name: getLink
    /// Description: return a list of GameObject instances that are all the nodes in Sankey diagram.
    ///              Get all node
    /// </summary>
    /// <returns> an array of all the node instence </returns>
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

    /// <summary>
    /// Function Name: DownTheNode
    /// Description: When cursor move to next node, the last node should be move down
    /// </summary>
    /// <param name="lastNode"> the last target node </param>
    private void DownTheNode(GameObject lastNode)
    {
        showLink = false;
        //Get the position
        float OrdXCoord = Camera.main.WorldToScreenPoint(lastNode.transform.position).x;
        float OrdYCoord = Camera.main.WorldToScreenPoint(lastNode.transform.position).y;
        float OrdZCoord = Camera.main.WorldToScreenPoint(lastNode.transform.position).z;
        //Debug.Log(lastNode.name.ToString());
        if (lastNode.tag.ToString() == "Cube")
        {
            //Down
            lastNode.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(OrdXCoord, OrdYCoord - 100, OrdZCoord));
            string NodeObjectName = lastNode.name;
            string NodeName = NodeObjectName.Split('@')[0];
            List<GameObject> LinkArray = getLink();
            string targetNodeName = "";
            //up link
            hover = true;
            //down target node
            List<GameObject> CubeArray = getCube();
            //Debug.Log(targetNodeName);
            Debug.Log("=====================================");
            foreach (GameObject gb in CubeArray)
            {
                string targetName = gb.name.ToString().Split('@')[0];
                //find the target node
                for (int i = 0; i < targetNameList.Count; i++)
                {
                    Debug.Log("Orignal: " + targetNameList[i]);
                    if (targetName.Equals(targetNameList[i]))
                    {
                        Debug.Log("the targetName equals with the node's name");
                        Debug.Log("targetName:  " + targetName);
                        float CubeOrdXCoord = Camera.main.WorldToScreenPoint(gb.transform.position).x;
                        float CubeOrdYCoord = Camera.main.WorldToScreenPoint(gb.transform.position).y;
                        float CubeOrdZCoord = Camera.main.WorldToScreenPoint(gb.transform.position).z;
                        gb.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(CubeOrdXCoord, CubeOrdYCoord - 100, CubeOrdZCoord));
                    }
                }
            }
            //clear the list
            targetNameList.Clear();
            linkList.Clear();
        }
    }
}
