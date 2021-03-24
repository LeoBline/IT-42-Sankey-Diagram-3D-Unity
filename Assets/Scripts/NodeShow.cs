using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeShow : MonoBehaviour {
    [SerializeField] private Sprite dotSprite;
    public GameObject JsonReaderObject;
    public List<GameObject> textlist;
    private RectTransform graphContainer;
    private RectTransform window_Graph;
    public int LineReadererPointsCount = 200;
    public float PositionScale = 0.25f;
    public float RatioHighAndArea = 64;

    private static NodeShow instance;

    private GameObject tooltipGameObject;
    private List<GameObject> GameObjectList;
    private List<GameObject> GameLineObjectList;
    private GameObject[] barlist;
    private GameObject[] linklist;
    int linkindex = 0;
    [Range(1, 16)]
    public int textFontSize =10;
    [Range((float)0.01, (float)0.99)]
    public float lineAlpha = 0.99f;
    public bool continulFlag = false;
    public bool dragFlag = false;
    public bool reloadFlag =false;
    public String dragNode;


    private double updateTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject obj1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ////设置物体的位置Vector3三个参数分别代表x,y,z的坐标数
        //obj1.transform.position = new Vector3(1, 1, 1);
        //obj1.transform.localScale = new Vector3(500,2,500);
       
        textlist = new List<GameObject>();
        NodesStructure[] nodesStructures = JsonReaderObject.GetComponent<JsonReaderTest>().NodesStructures;
        LinksStructure[] linksStructures = JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures;
        barlist = new GameObject[nodesStructures.Length];
        linklist = new GameObject[JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures.Length];
        instance = this;
      
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
    
    
    
    private GameObject CreateBar(Vector2 graphPosition, float barWidth, float barHight,float Zposition, NodesStructure Node, int i)
    {
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gameObject.AddComponent<Transform>();
        gameObject.name = "node" + Node.name;
        return gameObject;
    }
   

        private void showGraph(NodesStructure[] nodesStructures, LinksStructure[] links)
    {
        for (int i = 0; i < nodesStructures.Length; i++)
        {
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




            GameObjectList.AddRange(AddGraphVisual(new Vector3(xPosition * PositionScale, barHight/2,yPosition* PositionScale), 10, barHight, 10, "name:" + name + " Value:" + Value + " Depth: " + nodesStructures[i].depth.ToString() + " layer: " + nodesStructures[i].layer.ToString(), nodesStructures[i], i));
        }
        for (int i = 0; i < links.Length; i++)
        {
            GameLineObjectList.AddRange(AddGraphLineVisual("Value:" + (float)links[i].value, links[i]));
            /*            CreateLink(links[i]);*/
        }


    }
    private interface IGraphVisual
    {
        List<GameObject> AddGraphVisual(Vector3 graphPosition, float barWidth, float barHight,float Zposition, string tooltipText);
    }
    private interface IGraphVisualObject
    {
        void SetGraphVisualObjectInfo(Vector3 graphPosition, float barWidth, float barHight,float Zposition, string tooltipText);
    }
    public class BarChartVisualObject : IGraphVisualObject
    {
        private GameObject barGameObject;
        private float barWidth;
        private float barHight;
        private float Zposition;
        public BarChartVisualObject(GameObject barGameObject, float barWidth, float barHight,float Zposition)
        {
            this.barGameObject = barGameObject;
            this.barWidth = barWidth;
            this.barHight = barHight;
            this.Zposition = Zposition;
        }



        public void SetGraphVisualObjectInfo(Vector3 graphPosition, float barWidth, float barHight,float Zposition, string tooltipText)
        {
            barGameObject.GetComponent<Transform>().position = graphPosition;
            barGameObject.GetComponent<Transform>().localScale = new Vector3(barWidth, barHight, Zposition);

        }
    
    }
  
    public List<GameObject> AddGraphVisual(Vector3 graphPosition, float Width, float barHight,float barZStation, string tooltipText, NodesStructure a, int i)
    {
        GameObject barGameObject = CreateBar(graphPosition, Width, barHight,barZStation,a, i);
        BarChartVisualObject barChartVisualObject = new BarChartVisualObject(barGameObject, Width, barHight,barZStation);
        barChartVisualObject.SetGraphVisualObjectInfo(graphPosition, Width, barHight,barZStation,tooltipText);
        return new List<GameObject>() { barGameObject };
    }


   


    private GameObject CreateLink(LinksStructure link)
    {

        GameObject lineobject = new GameObject("line");
        lineobject.AddComponent<LineRenderer>();
        lineobject.transform.SetParent(graphContainer, false);
        LineRenderer line = lineobject.GetComponent<LineRenderer>();
        line.alignment = LineAlignment.TransformZ;
        line.sortingOrder = -9;
        line.useWorldSpace = false;
        line.motionVectors = false;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetColors(new Color(1, 1, 1, lineAlpha), new Color(1, 1, 1, lineAlpha));
        float y0_3D = (float)link.y0_3D;
        float y1_3D = (float)link.y1_3D;
        float width = (float)link.width;
        float z0 = (float)(link.SourceNode.y0 + ((float)link.SourceNode.y1 - (float)link.SourceNode.y0) / 2) * PositionScale;
        float x0 = (float)(link.SourceNode.x0 + ((float)link.SourceNode.x1 - (float)link.SourceNode.x0) / 2) * PositionScale;
        float z1 = (float)(link.TargetNode.y0 + ((float)link.TargetNode.y1 - (float)link.TargetNode.y0) / 2) * PositionScale;
        float x1 = (float)(link.TargetNode.x0 + ((float)link.TargetNode.x1 - (float)link.TargetNode.x0) / 2) * PositionScale;
        Debug.Log("x0" + x0 + "y0" + y0_3D + "z0" + z0 + "x1" + x1 + "y1" + y1_3D + "z1" + z1);
        Vector3 n1 = new Vector3(x0, y0_3D, z0);
        Vector3 n2 = new Vector3((x0 + x1) / 2, y0_3D, (z0 + z1) / 2);
        Vector3 n3 = new Vector3((x0 + x1) / 2, y1_3D, (z0 + z1) / 2);
        Vector3 n4 = new Vector3(x1, y1_3D, z1);
        DrawLinearCurve(line, n1, n2, n3, n4, (float)link.width);
        return lineobject;



    }


    private void DrawLinearCurve(LineRenderer lineRenderer,Vector3 position1, Vector3 position2, Vector3 position3, Vector3 position4, float width)
    {
        lineRenderer.SetWidth(width,width);
        for (int i = 0; i < LineReadererPointsCount + 1; i++)
        {
            float t = i / (float)LineReadererPointsCount;
            Vector3 pixel = CalculateLinearBezierPoint(t, position1, position2, position3, position4);
            Vector3 a = new Vector3(pixel.x, pixel.y, pixel.z);
            lineRenderer.SetVertexCount(i + 1);
            lineRenderer.SetPosition(i, a);
        }
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

