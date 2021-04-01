using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Linq;
using UnityEditor;
using System.IO;
using System;



using UnityEngine.Windows;

public class NodesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
    }

public class NodesStructure
{
    public string name { get; set; }
    //x0:
    public double x0 { get; set; }
    //x1:
    public double x1 { get; set; }
    //y0:
    public double y0 { get; set; }
    //y1:
    public double y1 { get; set; }

    public int index { get; set; }
    public int depth { get; set; }
    public int height{ get; set; }
    public int layer { get; set; }
    public double value { get; set; }
    public int yDepth { get; set; }

    public List<LinksStructure> SourceLinks
    {
        get;set;
    }
    public List<LinksStructure> TargetLinks { get; set; }

//    public void tostring()
//    {
        
        

///*        if (TargetLinks != null)
//        {
//            for (int i = 0; i < TargetLinks.Count; i++)
//            {
//                Debug.Log("Source: " + TargetLinks[i].sourceNode.name + " Target: " + TargetLinks[i].targetNode.name + " Value" + TargetLinks[i].value);
//            }
//        }*/
//    }


    /**
     *  calculate current node's value
     *  计算当前node的value
     *  分别计算传入/传出当前node的能量，并将其中较大的一方设为value
     *  对于桑基图中间的node传入和传出是一样大的，
     *  对于在桑基图两端的node只拥有传入或传出能量
     */
    public void getvalue()
    {
        double sourceValue = 0,targetValue=0;
        if(SourceLinks != null)
        {
            for(int i = 0; i < SourceLinks.Count; i++)
            {
                sourceValue += SourceLinks[i].value;
            }
        }
        else
        {
            this.depth = 0;
        }

        if (TargetLinks != null)
        {
            
            for (int i = 0; i < TargetLinks.Count; i++)
            {
                targetValue += TargetLinks[i].value;
            }
            
        }
        this.value = Math.Max(sourceValue, targetValue);
    }

}


/**
 * 按列存储node，按列在桑基图中画出位置
 */
public class ColumnNodes
{
    public List<NodesStructure> Columnnode { get; set; }
}

public class LinksStructure
{
    public double value { get; set; }
    public double y0 { get; set; }
    public double y1 { get; set; }
    public double y0_3D { get; set; }
    public double y1_3D { get; set; }
    public double width { get; set; }
    public int index { get; set; }
    public NodesStructure SourceNode { get; set; }
    public NodesStructure TargetNode { get; set; }
}

/**
 * 过渡用的对象
 * json 数据中的link 所拥有的数据
 */
public class LinksItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int source { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int target { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double value { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public List<NodesItem> nodes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LinksItem> links { get; set; }
    }


public class JsonReaderTest : MonoBehaviour
{
    private RectTransform graphContainer;
    public NodesStructure[] NodesStructures;
    public  LinksStructure[] LinksStructures;
    double x0 = 1, y0 = 1, x1 = 959, y1 = 494; // boundary of the Sankey in Unity 
    [Header("Attributes")]
    [Range(1, 100)]
    public double nodeWidth = 15; // nodeWidth
    [Range(0,100)]
    public double nodePadding = 10;
    double py; // nodePadding
    int id ;

    [Space(20)]
    [Tooltip("The image node displays the layout mode.")]
    public aligns align = aligns.justify;
    IComparable<NodesStructure> sort;
    IComparable<LinksStructure> linkSort;
    [Space(30)]
    public UnityEngine.Object jsFile;
    int links ;
   public int iterations = 25;

    public enum aligns {justify, left, right, center };

    // Start is called before the first frame update
    void Start()
    {
        loadDate("");

    }

    public void loadHtmlData(string JsonContent)
    {



        if (JsonContent == "")
        {
            Debug.Log("Online Json file is null");
        }
        else
        {

            JsonReader js = new JsonReader(new StringReader(JsonContent));
            Root r = JsonMapper.ToObject<Root>(js);
            NodesStructures = new NodesStructure[r.nodes.Count];
            for (int i = 0; i < r.nodes.Count; i++)
            {

                NodesStructures[i] = new NodesStructure();
                NodesStructures[i].name = r.nodes[i].name;
                NodesStructures[i].layer = 999;
                NodesStructures[i].index = i;
                NodesStructures[i].SourceLinks = new List<LinksStructure>();
                NodesStructures[i].TargetLinks = new List<LinksStructure>();
            }
            LinksStructures = new LinksStructure[r.links.Count];
            for (int i = 0; i < r.links.Count; i++)
            {


                /*            NodesStructures[r.links[i].target].addTargetLink(r.links[i].source, r.links[i].target, r.links[i].value,i);
                            NodesStructures[r.links[i].source].addSourceLink(r.links[i].source, r.links[i].target, r.links[i].value,i);*/
                LinksStructures[i] = new LinksStructure();
                LinksStructures[i].value = r.links[i].value;
                LinksStructures[i].index = i;
                LinksStructures[i].SourceNode = NodesStructures[r.links[i].source];
                LinksStructures[i].TargetNode = NodesStructures[r.links[i].target];
                NodesStructures[r.links[i].source].SourceLinks.Add(LinksStructures[i]);
                NodesStructures[r.links[i].target].TargetLinks.Add(LinksStructures[i]);

            }
            if (linkSort != null)
            {
                for (int i = 0; i < NodesStructures.Length; i++)
                {
                    NodesStructures[i].SourceLinks.Sort((IComparer<LinksStructure>)linkSort);
                    NodesStructures[i].TargetLinks.Sort((IComparer<LinksStructure>)linkSort);
                }
            }
            for (int i = 0; i < NodesStructures.Length; i++)
            {
                NodesStructures[i].getvalue();

            }
            ComputeNodeHeights();
            ComputeNodeDepths();

            ComputeNodeBreadths();
            computeLinkBreadths();
            /*        Debug.Log("--------------------------------");
                    for (int i = 0; i < NodesStructures.Length; i++)
                    {
                        NodesStructures[i].tostring();
                    }*/

            gameObject.SetActive(false);
            gameObject.transform.parent.GetComponent<NodeShow>().reloadFlag = true;
        }
    }

    public void loadDate(string transportFilepath)
    {
        Boolean FileRightFlag = true;
        graphContainer = transform.parent.gameObject.GetComponent<RectTransform>();
        x0 = 10;
        y0 = 10;
        x1 = x0 + graphContainer.sizeDelta.x - 10;
        y1 = graphContainer.sizeDelta.y + y0 - 20;
        string filepath = "";
        if (transportFilepath != "")
        {
            filepath = transportFilepath;  
            if (filepath != "")
            {
                
                if (!filepath.EndsWith(".json"))
                {
                    Debug.Log(filepath + " is not a json file");
                    FileRightFlag = false;
                }

            }
        }
        else
        {
            if (jsFile != null)
            {
                if (transportFilepath == "") { 
                filepath = UnityEditor.AssetDatabase.GetAssetPath(jsFile);
                 }
                if (!filepath.EndsWith(".json"))
                {
                    Debug.Log(filepath + " is not a json file");
                    FileRightFlag = false;
                }

            }

        }
        if (filepath != "" && FileRightFlag == true)
        {
            StreamReader streamreader = new StreamReader(filepath);
            JsonReader js = new JsonReader(streamreader);
            Root r = JsonMapper.ToObject<Root>(js);
            NodesStructures = new NodesStructure[r.nodes.Count];
            for (int i = 0; i < r.nodes.Count; i++)
            {

                NodesStructures[i] = new NodesStructure();
                NodesStructures[i].name = r.nodes[i].name;
                NodesStructures[i].layer = 999;
                NodesStructures[i].index = i;
                NodesStructures[i].SourceLinks = new List<LinksStructure>();
                NodesStructures[i].TargetLinks = new List<LinksStructure>();
            }
            LinksStructures = new LinksStructure[r.links.Count];
            for (int i = 0; i < r.links.Count; i++)
            {


                /*            NodesStructures[r.links[i].target].addTargetLink(r.links[i].source, r.links[i].target, r.links[i].value,i);
                            NodesStructures[r.links[i].source].addSourceLink(r.links[i].source, r.links[i].target, r.links[i].value,i);*/
                LinksStructures[i] = new LinksStructure();
                LinksStructures[i].value = r.links[i].value;
                LinksStructures[i].index = i;
                LinksStructures[i].SourceNode = NodesStructures[r.links[i].source];
                LinksStructures[i].TargetNode = NodesStructures[r.links[i].target];
                NodesStructures[r.links[i].source].SourceLinks.Add(LinksStructures[i]);
                NodesStructures[r.links[i].target].TargetLinks.Add(LinksStructures[i]);

            }
            if (linkSort != null)
            {
                for (int i = 0; i < NodesStructures.Length; i++)
                {
                    NodesStructures[i].SourceLinks.Sort((IComparer<LinksStructure>)linkSort);
                    NodesStructures[i].TargetLinks.Sort((IComparer<LinksStructure>)linkSort);
                }
            }
            for (int i = 0; i < NodesStructures.Length; i++)
            {
                NodesStructures[i].getvalue();

            }
            ComputeNodeHeights();
            ComputeNodeDepths();

            ComputeNodeBreadths();
            computeLinkBreadths();
            //Debug.Log("--------------------------------");
            //for (int i = 0; i < NodesStructures.Length; i++)
            //{
            //    NodesStructures[i].tostring();
            //}
            gameObject.SetActive(false);
            gameObject.transform.parent.GetComponent<NodeShow>().reloadFlag = true;
        }
    }

    void Update()
    {
        ComputeNodeHeights();
        ComputeNodeDepths();

        ComputeNodeBreadths();
        computeLinkBreadths();
        gameObject.SetActive(false);
    }
    public void ComputeNodeBreadths()
    {
        ColumnNodes[] columns = computeNodeLayers();
        //Debug.Log("Input Value:" + columns[0].Columnnode.Sum(c=>c.value)+"  Output Value: "+columns[columns.Length-1].Columnnode.Sum(b => b.value));
        int max1 = columns.Max(c=>c.Columnnode.Count);
        py = Math.Min(nodePadding, (y1 - y0) / (max1 - 1));
        initializeNodeBreadths(columns);
        for (var i = 0; i < iterations; ++i)
        {
            var alpha = Math.Pow(0.99f, i);
            var beta = Math.Max(1 - alpha, (i + 1) / iterations);
            relaxRightToLeft(columns, alpha, beta);
            relaxLeftToRight(columns, alpha, beta);
            
        }
    }

    private void relaxLeftToRight(ColumnNodes[] columns, double alpha, double beta)
    {
        for (int i = 1, n = columns.Length; i < n; ++i)
        {
            var column = columns[i];
            foreach (var target in column.Columnnode)
            {
                double y = 0;
                double w = 0;
                foreach(LinksStructure a in target.TargetLinks)
                {
                    double v = a.value * (target.layer - a.SourceNode.layer);
                    y += targetTop(a.SourceNode, target) * v;
                    w += v;
                }

                if (!(w > 0)) continue;
                var dy = (y / w - target.y0) * alpha;
                target.y0 += dy;
                target.y1 += dy;
                reorderNodeLinks(target);
            }

            if(sort ==null)column.Columnnode.Sort(ascendingBreadth);
            resolveCollisions(column, beta);

        }
    }
    public void relaxRightToLeft(ColumnNodes[] columns, double alpha, double beta)
    {
        for (int n = columns.Length,i = n - 2; i >= 0; --i)
        {
            ColumnNodes column = columns[i];
            foreach (NodesStructure source in column.Columnnode)
            {
                double y = 0;
                double w = 0;

                foreach (LinksStructure a in source.SourceLinks)
                {
                    var v = a.value * (a.TargetNode.layer - source.layer);
                    y += sourceTop(source, a.TargetNode) * v;
                    w += v;
                }

                if (!(w > 0)) continue;
                var dy = (y / w - source.y0) * alpha;

                source.y0 += dy;
                source.y1 += dy;
                reorderNodeLinks(source);
            }
            if (sort == null) column.Columnnode.Sort(ascendingBreadth);
            resolveCollisions(column, beta);
        }

    }
    private double targetTop(NodesStructure source, NodesStructure target)
    {
        var y = source.y0 - (source.SourceLinks.Count - 1) * py / 2;
        foreach (LinksStructure a in source.SourceLinks)
        {
            if (a.TargetNode.Equals( target))
            {
                break;
            }
            y += a.width + py;
        }
        foreach (LinksStructure a in target.TargetLinks)
        {
            if (a.SourceNode.Equals( source))
            {
                break;
            }
            y -= a.width;
        }
        return y;
    }



    private void resolveCollisions(ColumnNodes nodes, double alpha)
    {
        var i = nodes.Columnnode.Count >>  1;
        var subject = nodes.Columnnode[i];
        resolveCollisionsBottomToTop(nodes, subject.y0 - py, i - 1, alpha);
        resolveCollisionsTopToBottom(nodes, subject.y1 + py, i + 1, alpha);
        resolveCollisionsBottomToTop(nodes, y1, nodes.Columnnode.Count - 1, alpha);
        resolveCollisionsTopToBottom(nodes, y0, 0, alpha);
    }

    private void resolveCollisionsTopToBottom(ColumnNodes nodes, double y, int i, double alpha)
    {
        for (; i < nodes.Columnnode.Count; ++i)
        {
            var node = nodes.Columnnode[i];
            var dy = (y - node.y0) * alpha;
            if (dy > 1e-6)
            {
                node.y0 += dy;
                node.y1 += dy;
            }
            y = node.y1 + py;
        }
    }

    private void resolveCollisionsBottomToTop(ColumnNodes nodes, double y, int i, double alpha)
    {
        for (; i >= 0; --i)
        {
            var node = nodes.Columnnode[i];
            var dy = (node.y1 - y) * alpha;
            if (dy > 1e-6)
            {
              node.y0 -= dy;
              node.y1 -= dy;
            }
            y = node.y0 - py;
        }
    }

    private void reorderNodeLinks(NodesStructure source)
    {
        /*        if (linkSort === undefined)
                {*/
        
        foreach (LinksStructure a in source.TargetLinks)
        {

            a.SourceNode.SourceLinks.Sort(ascendingTargetBreadth);
        }
        foreach (LinksStructure a in source.SourceLinks)
        {
            a.TargetNode.TargetLinks.Sort(ascendingSourceBreadth);
        }

    

     /*   }*/
    }

    public double sourceTop(NodesStructure Sourcenode, NodesStructure Targetnode)
    {
        
        var y = Targetnode.y0 - (Targetnode.TargetLinks.Count - 1) * py / 2;
        foreach(LinksStructure a  in Targetnode.TargetLinks)
        {
            if (a.SourceNode.Equals(Sourcenode))
            {
                break;
            }
            y += a.width + py;
        }
        foreach (LinksStructure a in Sourcenode.SourceLinks)
        {
            if (a.TargetNode.Equals(Targetnode))
            {
                break;
            }
            y -= a.width;
        }
        return y;
    }

    



    public void initializeNodeBreadths(ColumnNodes[] columns)
    {

        double ky = columns.Min(c=>(y1-y0-((c.Columnnode.Count-1)*py))/c.Columnnode.Sum(b=>b.value));
        foreach(var a in columns)
        {
            var y = y0;
            foreach(var b in a.Columnnode)
            {
                b.y0 = y;
                b.y1 = (y + b.value * ky);
                y = b.y1 + py;
                foreach(var link in b.SourceLinks)
                {
                    link.width = (link.value * ky);
                }
            }
            y = (y1 - y + py) / (a.Columnnode.Count + 1);
            for(int i = 0; i < a.Columnnode.Count; i++)
            {
                var node = a.Columnnode[i];
                node.y0 += y * (i + 1);
                node.y1 += y * (i + 1);
            }
            reorderLinks(a.Columnnode);

        }
    }

    private void reorderLinks(List<NodesStructure> columnnode)
    {
        /*        if (linkSort ==0)
                {*/

        if (!(linkSort != null))
        { 
            foreach (var node in columnnode)
            {
                node.SourceLinks.Sort(ascendingTargetBreadth);
                node.TargetLinks.Sort(ascendingSourceBreadth);

            }
    }
       /* }*/
    }

    // Update is called once per frame




    public void ComputeNodeDepths()
    {
        List<NodesStructure> current = new List<NodesStructure>(NodesStructures);
        List<NodesStructure> next = new List<NodesStructure>();
        int x = 0;
        while (current.Count != 0)
        {
            foreach (NodesStructure a in current)
            {
                a.depth = x;
                if (a.SourceLinks != null)
                {
                    foreach (LinksStructure b in a.SourceLinks)
                    {
                        next.Add(b.TargetNode);
                    }
                }
            }
            if (++x > NodesStructures.Length)
            {
                //Debug.Log("ERROR  x>n");
            }
            current = next;
            next = new List<NodesStructure>();
        }
    }
    public void computeLinkBreadths()
    {
        foreach(var node in NodesStructures)
        {
            var y0 = node.y0;
            var y1 = y0;
            //3d y start at 0
            var y0_3D = 0.00;
            var y1_3D = y0_3D;
            foreach (var link in node.SourceLinks)
            {
                link.y0 = y0 + link.width / 2;
                y0 += link.width;

                link.y0_3D = y0_3D + link.width / 2;
                y0_3D += link.width;
            }
            foreach(var link in node.TargetLinks)
            {
                link.y1 = y1 + link.width / 2;
                y1 += link.width;

                link.y1_3D = y1_3D + link.width / 2;
                y1_3D += link.width;
            }
        }
    }

    public void ComputeNodeHeights()
    {
        List<NodesStructure> current = new List<NodesStructure>(NodesStructures);
        List<NodesStructure> next=new List<NodesStructure>();

        int x = 0;
        while (current.Count!=0)
        {
            foreach(NodesStructure a in current)
            {
                a.height = x;
                if (a.TargetLinks != null) {
                    foreach (LinksStructure b in a.TargetLinks)
                    {

                        next.Add(b.SourceNode);
                    }
                }
            }
            if (++x > NodesStructures.Length) {
                //Debug.Log("ERROR  x>n"); 
            }
            current = next;
            next = new List<NodesStructure>();
        }
    }
    public ColumnNodes[] computeNodeLayers() {
        int max = 0;
        for(int i = 0; i < NodesStructures.Length; i++)
        {
            if (NodesStructures[i].depth > max) max = NodesStructures[i].depth;
        }
        int x = NodesStructures.Max(c=>c.depth)+1;
        double kx = (x1 - x0 - nodeWidth) / (x - 1);
        ColumnNodes[] columns = new ColumnNodes[x];

        foreach(NodesStructure a in NodesStructures)
        {
            
            double temp=0;
            switch(align)
            {
                case aligns.justify:
                    temp = justify(a, x);
                    break;
                case aligns.center:
                    temp = center(a);
                    break;
                case aligns.right:
                    temp = right(a, x);
                    break;
                case aligns.left:
                    temp = left(a);
                    break;
                default:
                    //Debug.Log("ERROR Align");
                    break;
                    
            }
            int i = Math.Max(0, Math.Min(x-1,(int)Math.Floor(temp)));
            a.layer = i;
            a.x0 = (x0 + i * kx);
            a.x1 = a.x0 + nodeWidth;
            push(a,i,columns);
            /*            if (sort) for (const column of columns) {
                            column.sort(sort);
                        }*/



        }  
        if (sort!=null) foreach (var column in columns) {
                column.Columnnode.Sort((IComparer<NodesStructure>)sort);
            }
        return columns;

  }

    private double left(NodesStructure a)
    {
        return a.depth;
    }

    private double right(NodesStructure a, int x)
    {
        return x - 1 - a.height;
    }

    private double center(NodesStructure a)
    {
        return a.TargetLinks.Count!=0 ? a.depth
    : a.SourceLinks.Count!=0 ? a.SourceLinks.Min(c=>c.TargetNode.depth)-1
    : 0;
    }

    public void push(NodesStructure a,int i, ColumnNodes[] b)
    {
        if (b[i] == null)
        {a.yDepth = 0;
            b[i] = new ColumnNodes();
            b[i].Columnnode = new List<NodesStructure>();
            b[i].Columnnode.Add(a);
            

        }
        else
        {
            a.yDepth = b[i].Columnnode.Count;
            b[i].Columnnode.Add(a);
        }

    }



    public int justify(NodesStructure a ,int n)
    {   
        return a.SourceLinks.Count>0 ? a.depth : n - 1;

    }


    int  ascendingSourceBreadth(LinksStructure a, LinksStructure b)
    {
        if (ascendingBreadth(a.SourceNode, b.SourceNode) == 0) { return a.index - b.index; }
        return ascendingBreadth(a.SourceNode, b.SourceNode);
            /*|| a.index - b.index;*/
    }

    int ascendingTargetBreadth(LinksStructure a, LinksStructure b)
    {
        if (ascendingBreadth(a.TargetNode, b.TargetNode) == 0) { return a.index - b.index; }
        return ascendingBreadth(a.TargetNode, b.TargetNode);
           /* || a.index - b.index;*/
    }

    int ascendingBreadth(NodesStructure a, NodesStructure b)
    { 

        if((a.y0 - b.y0)==0) return 0;
        if ((a.y0 - b.y0) < 0) return -1;
        if ((a.y0 - b.y0) > 0) return 1;
        return (int)(a.y0 - b.y0);
    }
}
