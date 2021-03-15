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
    public float lineAlpha = 0.15f;
    public bool continulFlag = false;
    public bool dragFlag = false;
    public bool reloadFlag =false;
    public String dragNode;


    private double updateTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        textlist = new List<GameObject>();
        NodesStructure[] nodesStructures = JsonReaderObject.GetComponent<JsonReaderTest>().NodesStructures;
        LinksStructure[] linksStructures = JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures;
        barlist = new GameObject[nodesStructures.Length];
        linklist = new GameObject[JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures.Length];
        graphContainer = transform.GetComponent<RectTransform>();
        instance = this;
        tooltipGameObject = graphContainer.Find("tooltip").gameObject;
        tooltipGameObject.SetActive(false);
        GameObjectList = new List<GameObject>();
        GameLineObjectList = new List<GameObject>();
        showGraph(nodesStructures, linksStructures);
        transform.Find("AboutWindow").Find("Panel").GetComponent<CanvasGroup>().alpha = 0;
        transform.Find("Left").GetComponent<Button_UI>().ClickFunc = () =>
        {
            JsonReaderObject.GetComponent<JsonReaderTest>().align = JsonReaderTest.aligns.left;
            JsonReaderObject.SetActive(true);
            continulFlag = true;
            Update();
        };
        transform.Find("Right").GetComponent<Button_UI>().ClickFunc = () =>
        {
            JsonReaderObject.GetComponent<JsonReaderTest>().align = JsonReaderTest.aligns.right;
            JsonReaderObject.SetActive(true);
            continulFlag = true;
            Update();
        };
        transform.Find("Center").GetComponent<Button_UI>().ClickFunc = () =>
        {
            JsonReaderObject.GetComponent<JsonReaderTest>().align = JsonReaderTest.aligns.center;
            JsonReaderObject.SetActive(true);
            continulFlag = true;
            Update();
        };
       
            transform.Find("Justify").GetComponent<Button_UI>().ClickFunc = () =>
        {
            JsonReaderObject.GetComponent<JsonReaderTest>().align = JsonReaderTest.aligns.justify;
            JsonReaderObject.SetActive(true);
            continulFlag = true;
            Update();
        };
         transform.Find("Download").GetComponent<Button_UI>().ClickFunc = () =>
        {
         
            GameObject gameObject = new GameObject("MainCamera", typeof(Camera));
            ScreenshotHandler screenshot1 = gameObject.AddComponent<ScreenshotHandler>();
            gameObject.transform.SetParent(window_Graph, false);
            gameObject.AddComponent<RectTransform>();
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition3D = new Vector3(0, 0, -720);
            Camera camera = gameObject.GetComponent<Camera>();
           // camera.clearFlags = CameraClearFlags.Nothing;
            ScreenshotHandler screenshot = new ScreenshotHandler(camera);
            screenshot.TakeScreenshot_Static(1070, 800);
        };
        transform.Find("Help and about").GetComponent<Button_UI>().ClickFunc = () =>
         {
             transform.Find("AboutWindow").Find("Panel").GetComponent<CanvasGroup>().alpha = 1;
         };
        transform.Find("AboutWindow").Find("Panel").Find("C1").Find("Button").GetComponent<Button_UI>().ClickFunc = () =>
        {
            transform.Find("AboutWindow").Find("Panel").GetComponent<CanvasGroup>().alpha = 0;
        };

    }
    


    public List<GameObject> AddCollider(int part, GameObject lineobject, string tool)
    {
        List<GameObject> temp = new List<GameObject>();
        try
        {
            LineRenderer line = lineobject.GetComponent<LineRenderer>();
            var start = line.GetPosition(40);
            var end = line.GetPosition(line.positionCount - 1);
            var a = (line.positionCount - 1) / part;
            GameObject test = new GameObject("text", typeof(Text));
            test.GetComponent<Text>().fontSize = textFontSize;
            test.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            test.GetComponent<Text>().color = Color.black;
            test.GetComponent<Text>().text = tool;
            test.transform.SetParent(line.transform);
            test.SetActive(false);
            test.transform.localPosition = new Vector3(start.x + 60, start.y - 50, 0);
            textlist.Add(test);
            for (int i = 1; i <= part; i++)
            {
                if (i == 1)
                    temp.Add(AddColliderToLine(start, line.GetPosition(Mathf.CeilToInt(a * 1)), line, test));
                else if (i == part)
                    temp.Add(AddColliderToLine(line.GetPosition(Mathf.CeilToInt(a * (i - 1))), end, line, test));
                else
                    temp.Add(AddColliderToLine(line.GetPosition(Mathf.CeilToInt(a * (i - 1))), line.GetPosition(Mathf.CeilToInt(a * i)), line, test));
            }
        }
        catch
        {
            Destroy(gameObject);
        }
        return temp;
    }

    private GameObject AddColliderToLine(Vector3 start, Vector3 end, LineRenderer line, GameObject test)
    {
        var startPos = start;
        var endPos = end;
        GameObject a = new GameObject("Collider");
        a.AddComponent<lineMouseOver>();
        a.GetComponent<lineMouseOver>().a = test;
        BoxCollider col = a.AddComponent<BoxCollider>();
        col.transform.parent = line.transform;
        float lineLength = Vector3.Distance(startPos, endPos);
        col.size = new Vector3(lineLength, line.startWidth, 0);
        Vector3 midPoint = new Vector3((startPos.x + endPos.x) / 2, (startPos.y + endPos.y) / 2, 0);
        col.transform.localPosition = midPoint;
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, 0, angle);
        return a;
    }

    void Update()
    {
        if (continulFlag)
        {
            if(reloadFlag)
            {

                for (int i = 0; i < transform.childCount; i++)
                {

                    if (transform.GetChild(i).name.Contains("node") || transform.GetChild(i).name.Contains("line"))
                    {
                        Debug.Log(transform.GetChild(i).gameObject.name);
                        Destroy(transform.GetChild(i).gameObject);
                    }
                }
                reloadFlag = false;
                Start();
                
            }


                
            updateTime += 0.1;
            if (updateTime == 0.4)
            {          
                linkindex = 0;
                NodesStructure[] nodesStructures = JsonReaderObject.GetComponent<JsonReaderTest>().NodesStructures;
                LinksStructure[] linksStructures = JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures;
                graphContainer = transform.GetComponent<RectTransform>();
                updateGraph(nodesStructures, linksStructures);
                continulFlag = false;
                updateTime = 0;
            }
            
        }
        if(dragFlag && dragNode!=null)
        {
            linkindex = 0;
            NodesStructure[] nodesStructures = JsonReaderObject.GetComponent<JsonReaderTest>().NodesStructures;
            LinksStructure[] linksStructures = JsonReaderObject.GetComponent<JsonReaderTest>().LinksStructures;
            graphContainer = transform.GetComponent<RectTransform>();
            DrugUpdateGraph(nodesStructures, linksStructures);
            dragFlag = false;
        }    
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
            GameObjectList.AddRange(AddGraphVisual(new Vector2(xPosition, yPosition), Width, barHight, "name:" + name + " Value:" + Value + " Depth: " + nodesStructures[i].depth.ToString() + " layer: " + nodesStructures[i].layer.ToString(), nodesStructures[i], i));
        }
        for (int i = 0; i < links.Length; i++)
        {
            GameLineObjectList.AddRange(AddGraphLineVisual("Value:" + (float)links[i].value, links[i]));
            /*            CreateLink(links[i]);*/
        }
    }
    private void DrugUpdateGraph(NodesStructure[] nodesStructures, LinksStructure[] links)
    {
        for (int i = 0; i < nodesStructures.Length; i++)
        {
            if (nodesStructures[i].name == dragNode)
            {
                float xPosition = (float)nodesStructures[i].x0;
                float yPosition = (float)nodesStructures[i].y0;
                float Width = (float)nodesStructures[i].x1 - xPosition;
                float barHight = (float)nodesStructures[i].y1 - yPosition;
                string Value = nodesStructures[i].value.ToString();
                string name = nodesStructures[i].name;
                float depth = nodesStructures[i].yDepth;
                float layer = nodesStructures[i].layer;

                yPosition += barHight / 2;
                xPosition += Width / 2;
                updateBarAndLink(new Vector2(xPosition, yPosition), Width, barHight, nodesStructures[i], i,true);
                Button_UI barButtonUI = GameObjectList[i].gameObject.GetComponent<Button_UI>();
                barButtonUI.MouseOverOnceFunc += () =>
                {
                    ShowTooltip_Static("name:" + name + "Value:" + Value + " yDepth: " + depth + " layer: " + layer, (new Vector2(xPosition, yPosition)));
                };
            }
        }
        for (int i = 0; i < links.Length; i++)
        {
            if (links[i].TargetNode.name == dragNode || links[i].SourceNode.name == dragNode)
            {
                upDateLine("Value:" + (float)links[i].value, links[i], i, nodesStructures);
            }
        }
    }

    private void updateGraph(NodesStructure[] nodesStructures, LinksStructure[] links)
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
            updateBarAndLink(new Vector2(xPosition, yPosition), Width, barHight, nodesStructures[i], i,false);
            Button_UI barButtonUI = GameObjectList[i].gameObject.GetComponent<Button_UI>();
            barButtonUI.MouseOverOnceFunc += () => {
                ShowTooltip_Static("name:" + name + "Value:" + Value, (new Vector2(xPosition, yPosition)));
            };
        }
        for (int i = 0; i < links.Length; i++)
        {
            upDateLine("Value:" + (float)links[i].value, links[i], i,nodesStructures);
            /*            CreateLink(links[i]);*/
        }
    }

    private void upDateLine(string tooltipText, LinksStructure a, int i,NodesStructure[] NodesStructures)
    {
        GameObject lineobject = GameLineObjectList[i].gameObject;
        LineRenderer line = lineobject.GetComponent<LineRenderer>();
        foreach (var node in NodesStructures)
        {
            double ya = node.y0;
            double yb = ya;
            foreach (var link in node.SourceLinks)
            {
                link.y0 = ya + link.width / 2;
                ya += link.width;
            }
            foreach (var link in node.TargetLinks)
            {
                link.y1 = yb + link.width / 2;
                yb += link.width;
            }
        }




        float x0 = (float)a.SourceNode.x1;
        float x1 = (float)a.TargetNode.x0;
        float y0 = (float)a.y0;
        float y1 = (float)a.y1;
        float width = (float)a.width;
        /*Debug.Log(width + " " + Node.TargetLinks[z].SourceNode.name);*/
        Vector2 n1 = new Vector2(x0, y0);
        Vector2 n2 = new Vector2((x0 + x1) / 2, y0);
        Vector2 n3 = new Vector2((x0 + x1) / 2, y1);
        Vector2 n4 = new Vector2(x1, y1);
        DrawLinearCurve(line, n1, n2, n3, n4, width);
        updateCollider(6, lineobject, tooltipText);
    }

    public void updateCollider(int part, GameObject lineobject, string tool)
    {
        try
        {
            LineRenderer line = lineobject.GetComponent<LineRenderer>();
            var start = line.GetPosition(0);
            var end = line.GetPosition(line.positionCount - 1);
            var a = (line.positionCount - 1) / part;
            GameObject test = line.transform.Find("text").gameObject;
            test.GetComponent<Text>().text = tool;
            test.transform.localPosition = new Vector3(start.x + 60, start.y - 50, 0);
            for (int i = 1; i <= 6; i++)
            {
                if (line.transform.GetChild(i).name == "Collider")
                {
                    if (i == 1)
                        UpdateColliderToLine(start, line.GetPosition(Mathf.CeilToInt(a * 1)), line, test, i);
                    else if (i == part)
                        UpdateColliderToLine(line.GetPosition(Mathf.CeilToInt(a * (i - 1))), end, line, test, i);
                    else
                        UpdateColliderToLine(line.GetPosition(Mathf.CeilToInt(a * (i - 1))), line.GetPosition(Mathf.CeilToInt(a * i)), line, test, i);
                }
            }
        }
        catch
        {
            Destroy(gameObject);
        }
    }
    private GameObject UpdateColliderToLine(Vector3 start, Vector3 end, LineRenderer line, GameObject test, int i)
    {
        var startPos = start;
        var endPos = end;
        GameObject a = line.transform.GetChild(i).gameObject;
        a.GetComponent<lineMouseOver>().a = test;
        BoxCollider col = a.GetComponent<BoxCollider>();
        float lineLength = Vector3.Distance(startPos, endPos);
        col.size = new Vector3(lineLength, line.startWidth, 0);
        Vector3 midPoint = new Vector3((startPos.x + endPos.x) / 2, (startPos.y + endPos.y) / 2, 0);
        col.transform.localPosition = midPoint;
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.localEulerAngles = new Vector3(0, 0, 0);
        col.transform.Rotate(0, 0, angle);
        return a;
    }
    public Color RandomColor1()
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);
        Color color = new Color(r, g, b);
        return color;
    }
    private void updateBarAndLink(Vector2 graphPosition, float barWidth, float barHight, NodesStructure Node, int i,bool drag)
    {
        GameObject gameObject = GameObjectList[i].gameObject;
        if (drag == false)
        {
            gameObject.GetComponent<Image>().color = RandomColor1();
        }
        else{
            
        }
        gameObject.transform.SetParent(graphContainer, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = graphPosition;
        rectTransform.sizeDelta = new Vector2(barWidth, barHight);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.anchorMin = new Vector2(0, 0);

        DragNode node = gameObject.GetComponent<DragNode>();
        node.setRectTransform(gameObject.GetComponent<RectTransform>(),this.gameObject);
        node.setNodeStructure(Node, barWidth, barHight);
    }
    private GameObject CreateBar(Vector2 graphPosition, float barWidth, float barHight, NodesStructure Node, int i)
    {
        GameObject gameObject = new GameObject("node:" + Node.name, typeof(Image));
        gameObject.GetComponent<Image>().color = RandomColor1();
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.transform.SetSiblingIndex(0);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = graphPosition;
        rectTransform.sizeDelta = new Vector2(barWidth, barHight);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.anchorMin = new Vector2(0, 0);
        barlist[i] = gameObject;

        GameObject test = new GameObject("text", typeof(Text));
        test.GetComponent<Text>().fontSize = textFontSize;
        test.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        test.GetComponent<Text>().color = Color.green;
        test.GetComponent<Text>().text = Node.name;
        test.transform.SetParent(gameObject.transform);
        test.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 20);
        test.transform.localPosition = new Vector3(60 , 0, 0);

        return gameObject;
    }

   

    private GameObject CreateLink(LinksStructure link)
    {

            GameObject lineobject = new GameObject("line");
            lineobject.AddComponent<LineRenderer>();
            lineobject.transform.SetParent(graphContainer, false);
            LineRenderer line = lineobject.GetComponent<LineRenderer>();
            line.sortingOrder = -9;
            line.useWorldSpace = false;
            line.motionVectors = false;
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.SetColors(new Color(1, 1, 1, lineAlpha), new Color(1, 1, 1, lineAlpha));

            


            float x0 = (float)link.SourceNode.x1;
            float x1 = (float)link.TargetNode.x0;
            float y0 = (float)link.y0;
            float y1 = (float)link.y1;
            float width = (float)link.width;
            /*Debug.Log(width + " " + Node.TargetLinks[z].SourceNode.name);*/
            Vector2 n1 = new Vector2(x0, y0);
            Vector2 n2 = new Vector2((x0 + x1) / 2, y0);
            Vector2 n3 = new Vector2((x0 + x1) / 2, y1);
            Vector2 n4 = new Vector2(x1, y1);
            DrawLinearCurve(line, n1, n2, n3, n4, width);
        return lineobject;


        
    }

    private void DrawLinearCurve(LineRenderer lineRenderer,Vector2 position1, Vector2 position2, Vector2 position3, Vector2 position4, float width)
    {
        lineRenderer.SetWidth(width, width);
        for (int i = 0; i < LineReadererPointsCount + 1; i++)
        {
            float t = i / (float)LineReadererPointsCount;
            Vector2 pixel = CalculateLinearBezierPoint(t, position1, position2, position3, position4);
            Vector3 a = new Vector3(pixel.x, pixel.y, 0);
            lineRenderer.SetVertexCount(i + 1);
            lineRenderer.SetPosition(i, a);



        }
    }


    private Vector3 CalculateLinearBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        Vector2 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;
        return p;

    }




    private interface IGraphVisual
    {
        List<GameObject> AddGraphVisual(Vector2 graphPosition, float barWidth, float barHight, string tooltipText);
    }
    private interface IGraphVisualObject
    {
        void SetGraphVisualObjectInfo(Vector2 graphPosition, float barWidth, float barHight, string tooltipText);
    }
    public class BarChartVisualObject : IGraphVisualObject
    {
        private GameObject barGameObject;
        private float barWidth;
        private float barHight;
        public BarChartVisualObject(GameObject barGameObject, float barWidth, float barHight)
        {
            this.barGameObject = barGameObject;
            this.barWidth = barWidth;
            this.barHight = barHight;
        }

      

        public void SetGraphVisualObjectInfo(Vector2 graphPosition, float barWidth, float barHight, string tooltipText)
        {

            RectTransform rectTransform = barGameObject.GetComponent<RectTransform>();
            rectTransform.SetParent(barGameObject.GetComponent<RectTransform>().parent);
            rectTransform.anchoredPosition = graphPosition;
            rectTransform.sizeDelta = new Vector2(barWidth, barHight);
        }


    }
    public static void ShowTooltip_Static(string tooltipText, Vector2 anchoredPosition)
    {
        instance.ShowTooltip(tooltipText, anchoredPosition);
    }
    private void ShowTooltip(string tooltipText, Vector2 anchoredPosition)
    {

        tooltipGameObject.SetActive(true);
        tooltipGameObject.GetComponent<RectTransform>().SetParent(graphContainer, false);
        tooltipGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        Text tooltipUIText = tooltipGameObject.transform.Find("text").GetComponent<Text>();
        tooltipUIText.text = tooltipText;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2
        (
         tooltipUIText.preferredWidth + textPaddingSize * 2f,
        tooltipUIText.preferredHeight + textPaddingSize * 2f

        );

        tooltipGameObject.transform.Find("background").GetComponent<RectTransform>().sizeDelta = backgroundSize;
        tooltipGameObject.transform.SetAsLastSibling();
    }
    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }

    private void HideTooltip()
    {
        tooltipGameObject.SetActive(false);
    }
    public List<GameObject> AddGraphVisual(Vector2 graphPosition, float Width, float barHight, string tooltipText,NodesStructure a,int i) 
    {
        GameObject barGameObject = CreateBar(graphPosition, Width, barHight,a,i);
        BarChartVisualObject barChartVisualObject = new BarChartVisualObject(barGameObject, Width, barHight);
        barChartVisualObject.SetGraphVisualObjectInfo(graphPosition, Width, barHight, tooltipText);
        Button_UI barButtonUI = barGameObject.AddComponent<Button_UI>();
        barButtonUI.MouseOverOnceFunc += () => {
           /* ShowTooltip_Static(tooltipText, graphPosition);*/
        };
        

        // Hide Tooltip on Mouse Out
        barButtonUI.MouseOutOnceFunc += () => {

            HideTooltip_Static();
        };
        
      DragNode node =barGameObject.AddComponent<DragNode>();
        node.setRectTransform(barGameObject.GetComponent<RectTransform>(),this.gameObject);
        node.setNodeStructure(a,Width,barHight);
        return new List<GameObject>() { barGameObject };
    }

   
    public List<GameObject> AddGraphLineVisual(string tooltipText, LinksStructure a)
    {
        GameObject lineGameObject = CreateLink(a);
        List<GameObject> temp= AddCollider(6,lineGameObject,tooltipText);
        return new List<GameObject>() { lineGameObject };
    }

   
}
