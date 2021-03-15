using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//简单来说,贝塞尔曲线就是对多个线段同时做差值
public class DrawLine : MonoBehaviour
{

    public LineRenderer lineReaderer;
    public GameObject a;
    private RectTransform graphContainer;
    // Start is called before the first frame update
    private int numPoints = 200;
    private Vector2[] positions = new Vector2[201];
    private int curveCount = 1;

    [System.Obsolete]
    void Start()
    {
        if (lineReaderer == null)
        {
            lineReaderer = this.gameObject.AddComponent<LineRenderer>();
        }
        lineReaderer.sortingOrder =10;
        lineReaderer.useWorldSpace = false;
        
        LinksStructure[] linksStructures = a.GetComponent<JsonReaderTest>().LinksStructures;
        
        float NodeWeidth = (float)a.GetComponent<JsonReaderTest>().nodeWidth / 2;
        Debug.Log(linksStructures.Length);
        graphContainer = transform.GetComponent<RectTransform>();
        Vector2 localposition = transform.position;
        for(int i = 0; i < linksStructures.Length; i++)
        {
            float x0 = (float)linksStructures[i].SourceNode.x1;
            float x1 = (float)linksStructures[i].TargetNode.x0 ;
            float y0 = (float)linksStructures[i].y0;
            float y1 = (float)linksStructures[i].y1 ;
            float width = (float)linksStructures[i].width;
            Debug.Log(width+" "+ linksStructures[i].SourceNode.name);
            Vector2 n1 = new Vector2(x0, y0);
            Vector2 n2 = new Vector2((x0 + x1) / 2, y0 );
            Vector2 n3 = new Vector2((x0 + x1) / 2, y1);
            Vector2 n4 = new Vector2(x1, y1);
            DrawLinearCurve(n1, n2, n3, n4,width);
            Debug.Log(30.30);
        }
      


       
    }

    [System.Obsolete]
    private void DrawLinearCurve(Vector2 position1,Vector2 position2,Vector2 position3,Vector2 position4,float width)
    {
        lineReaderer.SetWidth(width,width);
        lineReaderer.material= new Material(Shader.Find("Sprites/Default"));
        lineReaderer.material.SetColor("_TintColor", new Color(0, 0, 0, 0.3f));
        for(int i =0;i<numPoints+1; i++)
        {
            float t = i / (float)numPoints;
            Vector2 pixel = CalculateLinearBezierPoint(t,position1,position2,position3,position4);
            Vector3 a = new Vector3(pixel.x,pixel.y,0);
            lineReaderer.SetVertexCount(i+1);
            lineReaderer.SetPosition(i, a);
            


        }
/*        for(int m = 0; m < positions.Length; m++)
        {
            if (m + 1 < positions.Length)
            {
                CreateDotConnection(positions[m], positions[m + 1],width);
            }
        }*/
       
        
    }
    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB,float width)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float x = dir.x;
        float y = dir.y;
        float z = 0;
        Vector3 dr1 = new Vector3(x, y, z);
        Vector3 d1 = new Vector3(1, 0, 0);
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, width<1? 1:width+1);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        float angle;
        if (dr1.y > 0)
        {
            angle = Vector3.Angle(dr1, d1);
        }

        else
        {
            angle = 360 - Vector3.Angle(dr1, d1);
        }
        rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
    private Vector3 CalculateLinearBezierPoint(float t,Vector2 p0,Vector2 p1,Vector2 p2,Vector2 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        Vector2 p = uuu * p0;
        p += 3* uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;
        return p;

    }
 

}



