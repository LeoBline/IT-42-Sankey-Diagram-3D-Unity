using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeShow : MonoBehaviour
{
    [SerializeField] private Sprite dotSprite;
    public GameObject JsonReaderObject;
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
    private List<GameObject> GameLineObjectList;
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
    public bool continulFlag = false;
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
        //GameObject obj1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ////设置物体的位置Vector3三个参数分别代表x,y,z的坐标数
        //obj1.transform.position = new Vector3(1, 1, 1);
        //obj1.transform.localScale = new Vector3(500,2,500);
        nodesStructures = JsonReaderObject.GetComponent<JsonReaderTest>().NodesStructures;
        linksStructures = JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures;
        barlist = new GameObject[nodesStructures.Length];
        linklist = new GameObject[JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures.Length];
        instance = this;
        groupContainer = transform.GetComponent<RectTransform>();
        GameObjectList = new List<GameObject>();
        GameLineObjectList = new List<GameObject>();
        showGraph(nodesStructures, linksStructures);
    }


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
        gameObject.AddComponent<Transform>();
        gameObject.GetComponent<MeshRenderer>().material = null;
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
        gameObject.transform.SetParent(groupContainer, false);
        gameObject.AddComponent<Text>().text =Node.name ;
        gameObject.AddComponent<DragNode3D>();

        return gameObject;
    }
    void OnGUI()
    {
        // Make a multiline text area that modifies stringToEdit.
        stringToEdit = GUI.TextArea(new Rect(10, 400, 200, 100), stringToEdit, 200);
    }


    //LAST STAGE
    //Texture2D GenerateTexture(float barWidth, float barHight, float BarZ)
    //{
    //    // 创建一个 128*128 的二维纹理
    //    var texture = new Texture2D(128, 128, TextureFormat.ARGB32, false);

    //    // 定义一个颜色数组
    //    var colors = new Color[(int)(barWidth) * (int)(barHight)];
    //    for (int i = 0; i < colors.Length; ++i)
    //    {
    //        colors[i] = RandomColor1();
    //    }

    //    // 在纹理左下角 32*32 的范围绘制一块黑色区域
    //    texture.SetPixels(0, 0, 31, 31, colors);

    //    // Apply 使设置生效
    //    texture.Apply(false, false);
    //    return texture;
    //}


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
            //areaWidth = areaHigh;




            GameObjectList.AddRange(AddGraphVisual(new Vector3(xPosition * PositionScale, barHight / 2, yPosition * PositionScale), 10, barHight, 10, "name:" + name + " Value:" + Value + " Depth: " + nodesStructures[i].depth.ToString() + " layer: " + nodesStructures[i].layer.ToString(), nodesStructures[i], i));
        }
        for (int i = 0; i < links.Length; i++)
        {
            GameLineObjectList.AddRange(AddGraphLineVisual("Value:" + (float)links[i].value, links[i]));
            /*            CreateLink(links[i]);*/
        }


    }


    private void Update()
    {
        if(GameLineObjectList != null)
        {
            foreach (GameObject line in GameLineObjectList)
            {
                Destroy(line);
            }
            for (int i = 0; i < linksStructures.Length; i++)
            {
                GameLineObjectList.AddRange(AddGraphLineVisual("Value:" + (float)linksStructures[i].value, linksStructures[i]));
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





    private GameObject CreateLink(LinksStructure link)
    {
        GameObject lineobject = new GameObject("line");
        MeshFilter meshFilter = lineobject.AddComponent<MeshFilter>();
        lineobject.AddComponent<MeshRenderer>();
        lineobject.AddComponent<LineRenderer>();
        // lineobject.transform.SetParent(groupContainer, false);
        LineRenderer line = lineobject.GetComponent<LineRenderer>();
        Color color = new Color(1, 1, 1, lineAlpha);
        lineobject.GetComponent<MeshRenderer>().material = linkMaterial;
        lineobject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        line.alignment = LineAlignment.TransformZ;
        line.sortingOrder = -9;
        line.useWorldSpace = false;
        line.motionVectors = false;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetColors(new Color(1, 1, 1, lineAlpha), new Color(1, 1, 1, lineAlpha));
        string SourceNodeName = link.SourceNode.name + "@" + link.SourceNode.value;
        GameObject SourceNode = GameObject.Find(SourceNodeName);
        float y0_3D = SourceNode.transform.position.y + (float)link.width/2;
        //float y0_3D = (float)link.y0_3D + (float)link.width / 2;
        string TargetNodeName = link.TargetNode.name + "@" + link.TargetNode.value;
        GameObject TargetNode = GameObject.Find(TargetNodeName);
        float y1_3D = TargetNode.transform.position.y + (float)link.width / 2;
        //float y1_3D = (float)link.y1_3D + (float)link.width / 2;
        float width = (float)link.width;

        float z0 = (float)(SourceNode.transform.position.z) * PositionScale - 10 / 2;

        float x0 = (float)(SourceNode.transform.position.x) * PositionScale + 10 / 2;
        float z1 = (float)(TargetNode.transform.position.z) * PositionScale - 10 / 2;
        float x1 = (float)(TargetNode.transform.position.x) * PositionScale - 10 / 2;


        //float z0 = (float)(link.SourceNode.y0 + ((float)link.SourceNode.y1 - (float)link.SourceNode.y0) / 2) * PositionScale - 10 / 2;
        
        //float x0 = (float)(link.SourceNode.x0 + ((float)link.SourceNode.x1 - (float)link.SourceNode.x0) / 2) * PositionScale + 10 / 2;
        //float z1 = (float)(link.TargetNode.y0 + ((float)link.TargetNode.y1 - (float)link.TargetNode.y0) / 2) * PositionScale - 10 / 2;
        //float x1 = (float)(link.TargetNode.x0 + ((float)link.TargetNode.x1 - (float)link.TargetNode.x0) / 2) * PositionScale - 10 / 2;
        //Debug.Log("x0" + x0 + "y0" + y0_3D + "z0" + z0 + "x1" + x1 + "y1" + y1_3D + "z1" + z1);
        //n1 is left vector
        Vector3 n1 = new Vector3(x0, y0_3D, z0);
        Vector3 n2 = new Vector3((x0 + x1) / 2, y0_3D, (z0 + z1) / 2);
        //n4 is right vector
        Vector3 n3 = new Vector3((x0 + x1) / 2, y1_3D, (z0 + z1) / 2);
        Vector3 n4 = new Vector3(x1, y1_3D, z1);
        //future need to change the node width
        DrawLinearCurve(meshFilter, line, n1, n2, n3, n4, width, 10, 10);
        return lineobject;

    }

    

    //Draw the Bizare curve of Link
    private void DrawLinearCurve(MeshFilter lineobject, LineRenderer lineRenderer, Vector3 position1, Vector3 position2, Vector3 position3, Vector3 position4, float width, float LDepth, float RDepth)
    {
        List<Vector3> curveDataList = new List<Vector3>();
        curveDataList.AddRange(Bezier_CubicCurvePoints(position1, position2, position3, position4, splitCount));

        int numPoints = curveDataList.Count * 8;
        Vector3[] verts = new Vector3[numPoints];
        Vector2[] uvs = new Vector2[numPoints];
        float widthInterval = (RDepth - LDepth) / (curveDataList.Count - 1);
        float curvelength = 0;
        for (int i = 0; i < curveDataList.Count - 1; i++)
        {
            curvelength += Vector3.Distance(curveDataList[i], curveDataList[i + 1]);
        }

        // Vertex DATA Setup
        float u1, u2, covered = 0;
        for (int i = 0; i < curveDataList.Count - 1; i++)
        {
            float meshLDepth = LDepth + i * widthInterval;
            float meshRDepth = LDepth + (i + 1) * widthInterval;
            verts[i * 8 + 0] = curveDataList[i];
            verts[i * 8 + 1] = curveDataList[i] + Vector3.down * width;
            verts[i * 8 + 2] = curveDataList[i + 1];
            verts[i * 8 + 3] = curveDataList[i + 1] + Vector3.down * width;

            verts[i * 8 + 4] = curveDataList[i] + Vector3.forward * meshLDepth;
            verts[i * 8 + 5] = curveDataList[i + 1] + Vector3.forward * meshRDepth;

            verts[i * 8 + 6] = curveDataList[i] + Vector3.forward * meshLDepth + Vector3.down * width;
            verts[i * 8 + 7] = curveDataList[i + 1] + Vector3.forward * meshRDepth + Vector3.down * width;

            //uvs[i * 8 + 0] = Vector3.zero;
            //uvs[i * 8 + 1] = Vector3.zero;
            //uvs[i * 8 + 2] = Vector3.zero;
            //uvs[i * 8 + 3] = Vector3.zero;
            //uvs[i * 8 + 4] = Vector3.zero;
            //uvs[i * 8 + 5] = Vector3.zero;
            //uvs[i * 8 + 6] = Vector3.zero;
            //uvs[i * 8 + 7] = Vector3.zero;
            u1 = (curveDataList[i].x - curveDataList[0].x) / (curveDataList[curveDataList.Count - 1].x - curveDataList[0].x);
            u2 = (curveDataList[i + 1].x - curveDataList[0].x) / (curveDataList[curveDataList.Count - 1].x - curveDataList[0].x);
            uvs[i * 8 + 0] = new Vector2(u1, 1);
            uvs[i * 8 + 1] = new Vector2(u1, 0);
            uvs[i * 8 + 2] = new Vector2(u2, 1);
            uvs[i * 8 + 3] = new Vector2(u2, 0);
            uvs[i * 8 + 4] = new Vector2(u1, 0);
            uvs[i * 8 + 5] = new Vector2(u2, 0);
            //switch (uvType)
            //{
            //    case UVType.None:

            //        break;
            //    case UVType.VertexBased:
            //        uvs[i * 8 + 0] = verts[i * 8 + 0];
            //        uvs[i * 8 + 1] = verts[i * 8 + 1];
            //        uvs[i * 8 + 2] = verts[i * 8 + 2];
            //        uvs[i * 8 + 3] = verts[i * 8 + 3];
            //        uvs[i * 8 + 4] = new Vector2(verts[i * 8 + 4].x, verts[i * 8 + 4].z);
            //        uvs[i * 8 + 5] = new Vector2(verts[i * 8 + 5].x, verts[i * 8 + 5].z);
            //        break;
            //    case UVType.AxisBased:
            //        u1 = (curveDataList[i].x - curveDataList[0].x) / (curveDataList[curveDataList.Count - 1].x - curveDataList[0].x);
            //        u2 = (curveDataList[i + 1].x - curveDataList[0].x) / (curveDataList[curveDataList.Count - 1].x - curveDataList[0].x);
            //        uvs[i * 8 + 0] = new Vector2(u1, 1);
            //        uvs[i * 8 + 1] = new Vector2(u1, 0);
            //        uvs[i * 8 + 2] = new Vector2(u2, 1);
            //        uvs[i * 8 + 3] = new Vector2(u2, 0);
            //        uvs[i * 8 + 4] = new Vector2(u1, 0);
            //        uvs[i * 8 + 5] = new Vector2(u2, 0);
            //        break;
            //    case UVType.LengthBased:
            //        u1 = covered / curvelength;
            //        covered += Vector3.Distance(curveDataList[i], curveDataList[i + 1]);
            //        u2 = covered / curvelength;
            //        uvs[i * 8 + 0] = new Vector2(u1, 1);
            //        uvs[i * 8 + 1] = new Vector2(u1, 0);
            //        uvs[i * 8 + 2] = new Vector2(u2, 1);
            //        uvs[i * 8 + 3] = new Vector2(u2, 0);
            //        uvs[i * 8 + 4] = new Vector2(u1, 0);
            //        uvs[i * 8 + 5] = new Vector2(u2, 0);

            //        break;
            //    default:
            //        break;
            //}
        }

        // Indices Setup
        int numTris = numPoints - 2 - 2;
        int[] indices = new int[numTris * 3];
        //Debug.Log(curveDataList.Count);

        for (int i = 0; i < curveDataList.Count - 1; i++)
        {
            indices[i * 24 + 0] = i * 8 + 0;
            indices[i * 24 + 1] = i * 8 + 2;
            indices[i * 24 + 2] = i * 8 + 1;

            indices[i * 24 + 3] = i * 8 + 1;
            indices[i * 24 + 4] = i * 8 + 2;
            indices[i * 24 + 5] = i * 8 + 3;

            indices[i * 24 + 6] = i * 8 + 0;
            indices[i * 24 + 7] = i * 8 + 4;
            indices[i * 24 + 8] = i * 8 + 5;

            indices[i * 24 + 9] = i * 8 + 5;
            indices[i * 24 + 10] = i * 8 + 2;
            indices[i * 24 + 11] = i * 8 + 0;
            //-----------------------------------------
            indices[i * 24 + 12] = i * 8 + 4;
            indices[i * 24 + 13] = i * 8 + 6;
            indices[i * 24 + 14] = i * 8 + 5;

            indices[i * 24 + 15] = i * 8 + 5;
            indices[i * 24 + 16] = i * 8 + 6;
            indices[i * 24 + 17] = i * 8 + 7;

            indices[i * 24 + 18] = i * 8 + 6;
            indices[i * 24 + 19] = i * 8 + 1;
            indices[i * 24 + 20] = i * 8 + 7;

            indices[i * 24 + 21] = i * 8 + 7;
            indices[i * 24 + 22] = i * 8 + 1;
            indices[i * 24 + 23] = i * 8 + 3;
        }


        Mesh mesh = new Mesh();

        mesh.Clear();
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = indices;
        mesh.RecalculateBounds();
        lineobject.mesh = mesh;


        //lineRenderer.SetWidth(width,width);
        //for (int i = 0; i < LineReadererPointsCount + 1; i++)
        //{
        //    float t = i / (float)LineReadererPointsCount;
        //    Vector3 pixel = CalculateLinearBezierPoint(t, position1, position2, position3, position4);
        //    Vector3 a = new Vector3(pixel.x, pixel.y, pixel.z);
        //    lineRenderer.SetVertexCount(i + 1);
        //    lineRenderer.SetPosition(i, a);
        //}
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
        return new List<GameObject>() { lineGameObject };
    }







}

