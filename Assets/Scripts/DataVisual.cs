using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/**
 * Class Name: 
 *     DateVisual
 
 * Class Description :
 *     This class store the json data from files and saves them as objects 
 *     in two arrays. An nested class (Window_Graph) handles the two arrays
 *     generate before, and draws the data in the user interface.  
 **/
public class DateVisual : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject a;
    void Start()
    {
        NodesStructure[] nodesStructures = a.GetComponent<JsonReaderTest>().NodesStructures;
        LinksStructure[] linksStructures = a.GetComponent<JsonReaderTest>().LinksStructures;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}


    /**
    * Nested Class: 
    *       Window_Graph
    * 
    * Class description:
    *       This class is for Tutorial. It can generate a node and show it in the graphic
    */
   
    public class Window_Graph : MonoBehaviour
    {
        [SerializeField] private Sprite dotSprite;
        private RectTransform graphContainer;
        private RectTransform labelTemplateX;
        private RectTransform labelTemplateY;
        private void Awake()
        {
            graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
            labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
            labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
            List<int> valueList = new List<int>() { 5, 98, 59, 30, 33 };
            showGraph(valueList);

        }

        /*
         * Method name: CreatDot
         * Argument: Vector2 （x,y)position of the node
         * Method description: return a node(GameObject) with input position(Vector2). The method will set the atrributes for the node.
         */
        private GameObject CreatDot(Vector2 anchoredPosition)
        {
            // create a GameObject as the node
            GameObject gameObject = new GameObject("dot", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().sprite = dotSprite;

            // set attributes for the node
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.anchorMin = new Vector2(0, 0);

            return gameObject;
        }

        /*
         * Method name: showGraph
         * Argument: 
         */
        private void showGraph(List<int> valueList)
        {
            // set width and height of the graphic,so that the component will not out of the boundary
            float graphWidth = graphContainer.sizeDelta.x;
            float xSize = graphWidth / (valueList.Count + 1);
            float yMaxinum = 100f;
            float graphHeight = graphContainer.sizeDelta.y;

            GameObject lastDotGameObject = null;
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = i * xSize;
                float yPosition = (valueList[i] / yMaxinum) * graphHeight;
                CreatBar(new Vector2(xPosition, yPosition), xSize);
                CreatDot(new Vector2(xPosition, yPosition));
                GameObject dotGameObject = CreatDot(new Vector2(xPosition, yPosition));
                if (lastDotGameObject != null)
                {
                    CreateDotConnection(lastDotGameObject.GetComponent<RectTransform>().anchoredPosition,
                        dotGameObject.GetComponent<RectTransform>().anchoredPosition);
                }
                lastDotGameObject = dotGameObject;

                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(graphContainer, false);
                labelX.gameObject.SetActive(true);
                labelX.anchoredPosition = new Vector2(xPosition, -20f);
                labelX.GetComponent<Text>().text = i.ToString();


            }
            int intCount = 10;
            for (int i = 0; i < intCount; i++)
            {
                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(graphContainer, false);
                labelX.gameObject.SetActive(true);
                float normailizedValue = i * 1f / intCount;
                labelX.anchoredPosition = new Vector2(-7f, normailizedValue * graphHeight);
                labelX.GetComponent<Text>().text = Mathf.Round(normailizedValue * yMaxinum).ToString();
            }



        }

        //create the link between two nodes
        private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
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
            rectTransform.sizeDelta = new Vector2(distance, 3f);
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

        //Create the bar
        private GameObject CreatBar(Vector2 graphPosition, float barWidth)
        {
            GameObject gameObject = new GameObject("bar", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = graphPosition;
            rectTransform.sizeDelta = new Vector2(barWidth, 11);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.anchorMin = new Vector2(0, 0);
            return gameObject;
        }
    }
}
