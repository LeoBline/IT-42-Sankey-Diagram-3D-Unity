using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Class Name: NodeShow
/// Author: Hongcong Zhu, Yanxi Ke, Yidan Lou
/// Description: This class can be regarded as the controller in MVC architecture.
/// It manipulate the data from the nodes instance array, and display the node on Unity game scene.
/// </summary>
public class NodeShow : MonoBehaviour
{
    [SerializeField] private Sprite dotSprite;
    public GameObject JsonReaderObject;
    public GameObject Text1;
    public List<GameObject> textlist;
    private RectTransform groupContainer;
    private RectTransform window_Graph;
    [SerializeField] int splitCount = 15;
    public int LineReadererPointsCount = 200;
    public float PositionScale = 0.25f;
    public float RatioHighAndArea = 64;
    public Material linkMaterial;
    private static NodeShow instance;
    private GameObject tooltipGameObject;
    private List<GameObject> GameObjectList;
    public List<GameObject> GameLineObjectList;
    private GameObject[] barlist;
    private GameObject[] linklist;
    public Material Bio;
    public Material Elec;
    public Material Hy;
    public Material Coal;
    public Material Solar;
    public Material Oil;
    public Material others;
    int linkindex = 0;
    [Range(1, 16)]
    public int textFontSize = 10;
    [Range((float)0.01, (float)0.99)]
    public float lineAlpha = 0.69f;
    public static bool continulFlag = false;
    public bool dragFlag = false;
    public bool reloadFlag = false;
    public String dragNode;
    public string stringToEdit = "Hello World\nI've got 2 lines...";
    private double updateTime = 0;
    private NodesStructure[] nodesStructures;
    private LinksStructure[] linksStructures;

    // Start is called before the first frame update
    void Start()
    {
        AddTag("Cube", gameObject);
        AddTag("Link", gameObject);
        Debug.Log(JsonReaderObject.GetComponent<JsonReaderTest>().align);
        nodesStructures = JsonReaderObject.GetComponent<JsonReaderTest>().NodesStructures;
        linksStructures = JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures;
        instance = this;
        barlist = new GameObject[nodesStructures.Length];
        linklist = new GameObject[JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures.Length];
        groupContainer = transform.GetComponent<RectTransform>();
        GameObjectList = new List<GameObject>();
        GameLineObjectList = new List<GameObject>();
        showGraph(nodesStructures, linksStructures);

    }
    public void Align(string align)
    {
        if (align == "left")
        {
            JsonReaderObject.GetComponent<JsonReaderTest>().align = JsonReaderTest.aligns.left;
            JsonReaderObject.SetActive(true);

            continulFlag = true;

        }
        if (align == "right")
        {
            JsonReaderObject.GetComponent<JsonReaderTest>().align = JsonReaderTest.aligns.right;
            JsonReaderObject.SetActive(true);
            continulFlag = true;

        }
        if (align == "center")
        {
            JsonReaderObject.GetComponent<JsonReaderTest>().align = JsonReaderTest.aligns.center;
            JsonReaderObject.SetActive(true);
            continulFlag = true;

        }
        if (align == "justify")
        {
            JsonReaderObject.GetComponent<JsonReaderTest>().align = JsonReaderTest.aligns.justify;
            JsonReaderObject.SetActive(true);
            continulFlag = true;

        }
    }

    #region addtag
    void AddTag(string tag, GameObject obj)
    {
        if (!isHasTag(tag))
        {
            SerializedObject tagManager = new SerializedObject(obj);//序列化物体
            SerializedProperty it = tagManager.GetIterator();//序列化属性
            while (it.NextVisible(true))//下一属性的可见性
            {
                if (it.name == "m_TagString")
                {
                    Debug.Log(it.stringValue);
                    it.stringValue = tag;
                    tagManager.ApplyModifiedProperties();
                }
            }
        }
    }
    bool isHasTag(string tag)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.tags[i].Equals(tag))
                return true;
        }
        return false;
    }
    #endregion

    public Color RandomColor1()
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);
        Color color = new Color(r, g, b);
        return color;
    }

    private GameObject CreateBar(Vector3 graphPosition, float barWidth, float barHight, float Zposition, NodesStructure Node, int i)
    {
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gameObject.GetComponent<MeshRenderer>().material = null;
        //Set coal, Hy and other materials in the material library of unity
        //Use the contain method to map the name to the specific cube
        if (Node.name.Contains("Coal"))
        {
            gameObject.GetComponent<MeshRenderer>().material = Coal;
        }
        else if (Node.name.Contains("H") || Node.name.Contains("Gas") || Node.name.Contains("Wind"))
        {
            gameObject.GetComponent<MeshRenderer>().material = Hy;
        }
        else if (Node.name.Contains("Solar"))
        {
            gameObject.GetComponent<MeshRenderer>().material = Solar;
        }
        else if (Node.name.Contains("Oil"))
        {
            gameObject.GetComponent<MeshRenderer>().material = Oil;
        }
        else if (Node.name.Contains("Elec"))
        {
            gameObject.GetComponent<MeshRenderer>().material = Elec;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = others;
        }
        gameObject.name = Node.name + "@" + Node.value.ToString();
        //Use this method to facilitate subsequent string splitting operations
        gameObject.transform.tag = "Cube";
        gameObject.transform.SetParent(groupContainer, false);
        //Add the corresponding script to the cube
        gameObject.AddComponent<DragNode3D>();
        gameObject.AddComponent<ClearlyShow>();
        GameObject prototype = Resources.Load("3DTextPrefab") as GameObject;
        GameObject Prefabtest = Instantiate(prototype);
        Prefabtest.transform.parent = gameObject.transform;
        Prefabtest.GetComponent<TextScript>().EnterTextHere = Node.name;
        Prefabtest.GetComponent<TextScript>().TextAppearingPosRot1 = gameObject;
        return gameObject;
    }

    private void showGraph(NodesStructure[] nodesStructures, LinksStructure[] links)
    {
        for (int i = 0; i < nodesStructures.Length; i++)
        {

            //float xPosition = 0.0f;
            float xPosition = (float)nodesStructures[i].x0;

            float yPosition = (float)nodesStructures[i].y0;

            float Width = (float)nodesStructures[i].x1 - xPosition;
            float barHight = (float)nodesStructures[i].y1 - yPosition;
            string Value = nodesStructures[i].value.ToString();
            string name = nodesStructures[i].name;

            yPosition += barHight / 2;
            xPosition += Width / 2;
            //float ZPosition = (float)Math.Pow(Convert.ToDouble(barHight / 4), Convert.ToDouble(1) / 3);
            //float areaWidth = (float)Math.Pow(barHight, 0.3333);
            //float areaHigh = areaWidth * (float)Math.Pow(RatioHighAndArea, 0.5);
            //barHight = areaWidth * RatioHighAndArea;
            //areaWidth = areaHigh;,
            GameObjectList.AddRange(AddGraphVisual(new Vector3(xPosition, barHight / 2, yPosition), 10, barHight, getMinLength(nodesStructures), "name:" + name + " Value:" + Value + " Depth: " + nodesStructures[i].depth.ToString() + " layer: " + nodesStructures[i].layer.ToString(), nodesStructures[i], i));
        }
        for (int i = 0; i < links.Length; i++)
        {
            GameLineObjectList.AddRange(AddGraphLineVisual("Value:" + (float)links[i].value, links[i]));
            /*            CreateLink(links[i]);*/
        }


    }
    public static IEnumerator WaitForSeconds(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }


    private void Update()
    {
        //when change the display mode or rebuild the graph
        if (continulFlag == true)
        {
            //delete something don't need
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Contains("(Clone)"))
                {
                    Destroy(transform.GetChild(i).gameObject);
                }

                if (transform.GetChild(i).name.Contains("@"))
                {
                    Debug.Log(transform.GetChild(i).gameObject.name);
                    Destroy(transform.GetChild(i).gameObject);
                }

            }

            //wait some time to restart
            StartCoroutine(WaitForSeconds(0.05f, () =>
            {
                Start();
            }));


            continulFlag = false;


        }
        else
        {

            ///when drag the node it will update the link .
            if (DragNode3D.isClick == true || ClearlyShow.hover == true)
            {

                if (GameLineObjectList != null)
                {
                    Debug.Log("DELET");
                    foreach (GameObject line in GameLineObjectList)
                    {
                        Destroy(line);
                    }
                    for (int i = 0; i < linksStructures.Length; i++)
                    {
                        GameLineObjectList.AddRange(AddGraphLineVisual("Value:" + (float)linksStructures[i].value, linksStructures[i]));
                    }
                }
                //debug test
                if (ClearlyShow.hover == true)
                {

                    Debug.Log("LInkList number: " + ClearlyShow.linkList.Count);
                    for (int i = 0; i < ClearlyShow.linkList.Count; i++)
                    {
                        Debug.Log("11111" + ClearlyShow.linkList[i]);
                        //GameObject.Find(ClearlyShow.linkList[i]).GetComponent<Renderer>().material.color = new Color(255 / 255f, 95 / 255f, 205 / 255f, 1 / 255f);
                    }
                }
                //change the flag 
                if (ClearlyShow.hover == true)
                {
                    ClearlyShow.hover = false;
                }
            }
        }

    }



    private interface IGraphVisual
    {
        List<GameObject> AddGraphVisual(Vector3 graphPosition, float barWidth, float barHight, float Zposition, string tooltipText);
    }
    private interface IGraphVisualObject
    {
        void SetGraphVisualObjectInfo(Vector3 graphPosition, float barWidth, float barHight, float Zposition, string tooltipText);
    }
    public class BarChartVisualObject : IGraphVisualObject
    {
        private GameObject barGameObject;

        private float barWidth;
        private float barHight;
        private float Zposition;
        public BarChartVisualObject(GameObject barGameObject, float barWidth, float barHight, float Zposition)
        {
            this.barGameObject = barGameObject;
            this.barWidth = barWidth;
            this.barHight = barHight;
            this.Zposition = Zposition;
        }



        public void SetGraphVisualObjectInfo(Vector3 graphPosition, float barWidth, float barHight, float Zposition, string tooltipText)
        {
            barGameObject.GetComponent<Transform>().position = graphPosition;
            barGameObject.GetComponent<Transform>().localScale = new Vector3(barWidth, barHight, Zposition);
            Change change1 = barGameObject.AddComponent<Change>();
            change1.setGameObject(barGameObject);
            change1.setLocal(new Vector3(barWidth, barHight, Zposition));
        }

    }


    public List<GameObject> AddGraphVisual(Vector3 graphPosition, float Width, float barHight, float barZStation, string tooltipText, NodesStructure a, int i)
    {
        GameObject barGameObject = CreateBar(graphPosition, Width, barHight, barZStation, a, i);
        BarChartVisualObject barChartVisualObject = new BarChartVisualObject(barGameObject, Width, barHight, barZStation);
        barChartVisualObject.SetGraphVisualObjectInfo(graphPosition, Width, barHight, barZStation, tooltipText);
        Mouse barButtonUI = barGameObject.AddComponent<Mouse>();
        return new List<GameObject>() { barGameObject };
    }





    //create link method input the link data
    private GameObject CreateLink(LinksStructure link)
    {
        GameObject lineobject = new GameObject("line");
        lineobject.transform.parent = this.transform;
        // create mesh to  show the link
        //add some component 
        MeshFilter meshFilter = lineobject.AddComponent<MeshFilter>();
        lineobject.AddComponent<MeshRenderer>();
        lineobject.AddComponent<LineRenderer>();
        lineobject.AddComponent<MeshCollider>();

        // lineobject.transform.SetParent(groupContainer, false);
        LineRenderer line = lineobject.GetComponent<LineRenderer>();
        lineobject.AddComponent<LineHighLight>();
        Color color = new Color(1, 1, 1, lineAlpha);
        //Debug.Log(ClearlyShow.showLink);
        if (ClearlyShow.showLink)
        {
            lineobject.GetComponent<MeshRenderer>().material = linkMaterial;
            lineobject.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 1.0f, 0.3f);
        }
        else
        {
            lineobject.GetComponent<MeshRenderer>().material = linkMaterial;
            lineobject.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 1.0f, 0.1f);
        }
        line.alignment = LineAlignment.TransformZ;
        line.sortingOrder = -9;
        line.useWorldSpace = false;
        line.motionVectors = false;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetColors(new Color(1, 1, 1, lineAlpha), new Color(1, 1, 1, lineAlpha));
        string SourceNodeName = link.SourceNode.name + "@" + link.SourceNode.value;
        GameObject SourceNode = GameObject.Find(SourceNodeName);
        float y0_3D = SourceNode.transform.position.y + (float)link.y0_3D + (float)link.width / 2 - SourceNode.transform.localScale.y / 2;
        //float y0_3D = (float)link.y0_3D + (float)link.width / 2;
        string TargetNodeName = link.TargetNode.name + "@" + link.TargetNode.value;
        GameObject TargetNode = GameObject.Find(TargetNodeName);
        float y1_3D = TargetNode.transform.position.y + (float)link.y1_3D + (float)link.width / 2 - TargetNode.transform.localScale.y / 2;
        //float y1_3D = (float)link.y1_3D + (float)link.width / 2;
        float width = (float)link.width;
        //Specific values of left and right up and down

        float z0 = (float)(SourceNode.transform.position.z) * PositionScale - 10 / 2;

        float x0 = (float)(SourceNode.transform.position.x) * PositionScale + 10 / 2;
        float z1 = (float)(TargetNode.transform.position.z) * PositionScale - 10 / 2;
        float x1 = (float)(TargetNode.transform.position.x) * PositionScale - 10 / 2;
        //n1 is left vector
        //Bezier curve control point
        Vector3 n1 = new Vector3(x0, y0_3D, z0);
        Vector3 n2 = new Vector3((x0 + x1) / 2, y0_3D, (z0 + z1) / 2);
        //n4 is right vector
        Vector3 n3 = new Vector3((x0 + x1) / 2, y1_3D, (z0 + z1) / 2);
        Vector3 n4 = new Vector3(x1, y1_3D, z1);
        //acconding the data to draw the link
        DrawLinearCurve(meshFilter, line, n1, n2, n3, n4, width, 10, 10);
        lineobject.name = SourceNode.name + "&" + TargetNode.name + "/" + link.value;
        lineobject.transform.tag = "Link";

        // when hover node it need to change the link color.
        if (ClearlyShow.showLinkName.Contains(lineobject.name.ToString()))
        {
            lineobject.GetComponent<MeshRenderer>().material = linkMaterial;
            lineobject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        }
        return lineobject;

    }

    private float getMinLength(NodesStructure[] nodes)
    {
        //The method dynamically calculates the maximum base area x value
        //Algorithm idea: Calculate the number of elements in the column with the most elements
        //The column spacing with the most elements in the 2d version is fixed
        int max = 0;
        for (int i = 0; i < nodes.Length; i++)
        {
            if (max < nodes[i].layer)
            {
                max = nodes[i].layer;
            }
        }

        int[] ay = new int[max];
        for (int m = 0; m < max; m++)
        {
            int number = 0;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].layer == m)
                {
                    number += 1;
                }

            }
            ay[m] = number;

        }
        int m1 = 0;
        int index = 0;
        for (int y = 0; y < ay.Length; y++)
        {
            if (ay[y] > m1)
            {
                m1 = ay[y];
                index = y;
            }
        }
        Debug.Log("index" + index);
        List<NodesStructure> node1 = new List<NodesStructure>();
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].layer == index)
            {
                node1.Add(nodes[i]);
            }
        }
        //Y0 of the second element of the column with the most elements-y1 of the first element
        float ymin = 100;
        float ymax = 100;
        for (int i = 0; i < node1.Count; i++)
        {
            if (node1[i].y1 < ymin)
            {
                ymin = (float)node1[i].y1;
            }
        }
        for (int i = 0; i < node1.Count; i++)
        {
            if (node1[i].y0 < ymax && node1[i].y0 > ymin)
            {
                ymax = (float)node1[i].y0;
            }
        }
        //When x is greater than the spacing, there must be a bottom area overlap
        //So the x value of the required bottom area is the spacing
        return (float)(ymax - ymin);


    }

    //Draw the Bizare curve of Link
    private void DrawLinearCurve(MeshFilter lineobject, LineRenderer lineRenderer, Vector3 position1, Vector3 position2, Vector3 position3, Vector3 position4, float width, float LDepth, float RDepth)
    {
        List<Vector3> curveDataList = new List<Vector3>();
        //input the bezier data to the datalist
        curveDataList.AddRange(Bezier_CubicCurvePoints(position1, position2, position3, position4, splitCount));

        //Vertex data.
        int numPoints = curveDataList.Count * 8;
        Vector3[] verts = new Vector3[numPoints];
        // set the uv  but we dont use the texture.
        //the RDepth and LDepth is 10
        Vector2[] uvs = new Vector2[numPoints];
        float widthInterval = (RDepth - LDepth) / (curveDataList.Count - 1);
        //this is use in another type of uv 
        float curvelength = 0;

        for (int i = 0; i < curveDataList.Count - 1; i++)
        {
            curvelength += Vector3.Distance(curveDataList[i], curveDataList[i + 1]);
        }

        // Vertex DATA Setup
        float u1, u2, covered = 0;
        int vertexSpace = 8;
        for (int i = 0; i < curveDataList.Count - 1; i++)
        {
            float meshLDepth = LDepth + i * widthInterval;
            float meshRDepth = LDepth + (i + 1) * widthInterval;
            verts[i * vertexSpace + 0] = curveDataList[i];
            verts[i * vertexSpace + 1] = curveDataList[i] + Vector3.down * width;
            verts[i * vertexSpace + 2] = curveDataList[i + 1];
            verts[i * vertexSpace + 3] = curveDataList[i + 1] + Vector3.down * width;

            verts[i * vertexSpace + 4] = curveDataList[i] + Vector3.forward * meshLDepth;
            verts[i * vertexSpace + 5] = curveDataList[i + 1] + Vector3.forward * meshRDepth;

            verts[i * vertexSpace + 6] = curveDataList[i] + Vector3.forward * meshLDepth + Vector3.down * width;
            verts[i * vertexSpace + 7] = curveDataList[i + 1] + Vector3.forward * meshRDepth + Vector3.down * width;

            
            u1 = (curveDataList[i].x - curveDataList[0].x) / (curveDataList[curveDataList.Count - 1].x - curveDataList[0].x);
            u2 = (curveDataList[i + 1].x - curveDataList[0].x) / (curveDataList[curveDataList.Count - 1].x - curveDataList[0].x);
            uvs[i * vertexSpace + 0] = new Vector2(u1, 1);
            uvs[i * vertexSpace + 1] = new Vector2(u1, 0);
            uvs[i * vertexSpace + 2] = new Vector2(u2, 1);
            uvs[i * vertexSpace + 3] = new Vector2(u2, 0);
            uvs[i * vertexSpace + 4] = new Vector2(u1, 0);
            uvs[i * vertexSpace + 5] = new Vector2(u2, 0);
        }

        // Indices Setup
        int numTris = numPoints - 2 - 2;
        int[] indices = new int[numTris * 3];
        int indiceSpace = 24;
        //detail trangle data
        for (int i = 0; i < curveDataList.Count - 1; i++)
        {
            indices[i * indiceSpace + 0] = i * vertexSpace + 0;
            indices[i * indiceSpace + 1] = i * vertexSpace + 2;
            indices[i * indiceSpace + 2] = i * vertexSpace + 1;

            indices[i * indiceSpace + 3] = i * vertexSpace + 1;
            indices[i * indiceSpace + 4] = i * vertexSpace + 2;
            indices[i * indiceSpace + 5] = i * vertexSpace + 3;

            indices[i * indiceSpace + 6] = i * vertexSpace + 0;
            indices[i * indiceSpace + 7] = i * vertexSpace + 4;
            indices[i * indiceSpace + 8] = i * vertexSpace + 5;

            indices[i * indiceSpace + 9] = i * vertexSpace + 5;
            indices[i * indiceSpace + 10] = i * vertexSpace + 2;
            indices[i * indiceSpace + 11] = i * vertexSpace + 0;
            //-----------------------------------------
            indices[i * indiceSpace + 12] = i * vertexSpace + 4;
            indices[i * indiceSpace + 13] = i * vertexSpace + 6;
            indices[i * indiceSpace + 14] = i * vertexSpace + 5;

            indices[i * indiceSpace + 15] = i * vertexSpace + 5;
            indices[i * indiceSpace + 16] = i * vertexSpace + 6;
            indices[i * indiceSpace + 17] = i * vertexSpace + 7;

            indices[i * indiceSpace + 18] = i * vertexSpace + 6;
            indices[i * indiceSpace + 19] = i * vertexSpace + 1;
            indices[i * indiceSpace + 20] = i * vertexSpace + 7;

            indices[i * indiceSpace + 21] = i * vertexSpace + 7;
            indices[i * indiceSpace + 22] = i * vertexSpace + 1;
            indices[i * indiceSpace + 23] = i * vertexSpace + 3;
        }


        Mesh mesh = new Mesh();

        mesh.Clear();
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = indices;
        mesh.RecalculateBounds();
        lineobject.mesh = mesh;
        lineobject.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private Vector3[] Bezier_CubicCurvePoints(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, int splits)
    {
        Vector3[] res = new Vector3[splits];
        float delta = 1f / (splits - 1);
        float dist = 0;
        for (int i = 0; i < (splits - 1); i++)
        {
            res[i] = Bezier_CubicCurvePoint(p1, p2, p3, p4, dist);
            dist += delta;
        }
        res[splits - 1] = Bezier_CubicCurvePoint(p1, p2, p3, p4, 1);
        return res;
    }

    private Vector3 Bezier_CubicCurvePoint(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
        Vector3 res = Vector3.zero;
        res.x = (1 - t) * (1 - t) * (1 - t) * p1.x + 3 * (1 - t) * (1 - t) * t * p2.x + 3 * (1 - t) * t * t * p3.x + t * t * t * p4.x;
        res.y = (1 - t) * (1 - t) * (1 - t) * p1.y + 3 * (1 - t) * (1 - t) * t * p2.y + 3 * (1 - t) * t * t * p3.y + t * t * t * p4.y;
        res.z = (1 - t) * (1 - t) * (1 - t) * p1.z + 3 * (1 - t) * (1 - t) * t * p2.z + 3 * (1 - t) * t * t * p3.z + t * t * t * p4.z;
        return res;
    }


    private Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;
        return p;
    }

    public List<GameObject> AddGraphLineVisual(string tooltipText, LinksStructure a)
    {
        GameObject lineGameObject = CreateLink(a);
        if (!DragNode3D.isClick == true && !ClearlyShow.hover == true)
        {
            lineGameObject.SetActive(false);
        }
        return new List<GameObject>() { lineGameObject };
    }







}

