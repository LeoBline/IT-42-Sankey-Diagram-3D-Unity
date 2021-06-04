# IT-42-Sankey-Diagram-3D-Unity
This is the UNISA IT Project; This is a Unity project.  The purpose is to convert the imported data into a 3D Sankey diagram in Unity.  And add a series of functions to better display the visual data graph.
This is the last semester project's Upgraded version.
The last semester project will show the 2D Sankey Diagram in Unity:
![image](https://github.com/LeoBline/IT-42-Sankey-Diagram-3D-Unity/blob/master/Sankey%20Diagram2D.png)


The last semester project Github Link: https://github.com/LeoBline/ICT-42-Sankey-Diagram-Unity
 -----------------------------------------------------------------------------------------------------
 So This Upgraded version will change it to 3D     !!X)!!
 
 After Hundreds of hours of serious work in the group,The Finall result:
 ![image](https://github.com/LeoBline/IT-42-Sankey-Diagram-3D-Unity/blob/master/Assets/StreamingAssets/CameraScreenshot.png)
 ![image](https://github.com/LeoBline/IT-42-Sankey-Diagram-3D-Unity/blob/master/Assets/StreamingAssets/Final%20Result.png)
 The functions of the Button buttons on the left are as follows:  
 >
 <br>1.HtmlJson:
   >>A Html address input box is displayed.  After entering Html for confirmation, the visual graph of the imported data can be displayed.</br>
  <br>2.LocalJson:
   A file selection window pops up.  Select the imported json data here.  After confirmation, the corresponding visual image will be displayed. </br>
 3.Justify:
   Change the display mode of the visual Sankey diagram.  In this mode, the beginning and end of each energy transmission are on one column.
 4.Center:
   Change the display mode of the visual Sankey diagram.  In this mode, the middle point of each energy transmission will be on one column.
 5.Right:
   Change the display mode of the visual Sankey diagram.  In this mode, the end points are all on one column.
 6.Left:
   Change the display mode of the visual Sankey diagram.  In this mode, the start points are all on one column.
 7.Save:
   Generate screenshots based on the current perspective.
 8.About:
   Display a specific help information window.
 9.Vartical View:
   View the entire Sankey visualization from the perspective of God. At the same time. limited the player movement.
 10.Hover:
   When this mode is turned on, when the crosshair points to the Node, this node and its subsequent nodes and the link between them will be highlighted. These highlighted nodes will also rise to allow users to better observe.  And other irrelevant nodes and links will be dim.
 
**Facing Problem**
1.The LineReaderer component used for energy transfer in the project last semester.  But this component cannot customize its three-dimensional width.  Only the width of the line can be set.  This is not applicable to energy transfer in real 3D.
2.Decide whether to adaptively adjust the size of the bottom area of the nodes.
