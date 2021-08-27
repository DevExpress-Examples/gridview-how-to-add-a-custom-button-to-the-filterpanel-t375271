<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128624780/15.2.9%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T375271)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[CustomGrid.cs](./CS/CustomGrid.cs) (VB: [CustomGrid.vb](./VB/CustomGrid.vb))**
* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))
<!-- default file list end -->
#  GridView - How to add a custom button to the FilterPanel 


<p>This example demonstrates how to add a button to the FilterPanel.<br><br><img src="https://raw.githubusercontent.com/DevExpress-Examples/gridview-how-to-add-a-custom-button-to-the-filterpanel-t375271/15.2.9+/media/74423eb1-12ba-11e6-80bf-00155d62480c.png"><br><br><br>To use this solution in your application, execute the following steps:<br><br><strong>1.</strong> Create a custom <strong>GridControl</strong> as it is shown at <strong><a href="https://www.devexpress.com/Support/Center/p/E900">How to create a GridView descendant class and register it for design-time use</a>.</strong><br><strong>2.</strong> Implement all required functionality in the <strong>CustomGridViewHandler, CustomGridView, CustomGridFilterPanelPainter </strong>classes.<br><br><br><strong>Implementation details<br></strong><br>In fact, the buttons are not located in the <strong>FilterPanel</strong>. They are simply drawn on its surface. Therefore, it is necessary to implement the <strong>Click</strong> (or MouseDown\Up) event manually and trigger it when required in your scenario. In this example, the <strong>Click</strong> event is raised when a custom button is clicked. If you need, for instance, to raise this event when the mouse button is released, you can simply update the <strong>CustomGridView.UpdateButtonState </strong>method. For instance, pass a parameter into this method which will indicate whether or not the <strong>CustomGridViewHandler.OnMouseUp </strong>event occurs. Then use this parameter in order to determine whether or not the <strong>Click</strong> event should be raised. <br><br>It is also possible to change the distance between custom buttons, as well as their size. For this, update the <strong>CustomGridView.UpdateButtonsRects </strong>method as your needs dictate.<br><br><strong>See also:</strong><br><strong><a href="https://www.devexpress.com/Support/Center/p/E2793">How to add a custom button to a column header in a grid </a></strong><br><strong><a href="https://www.devexpress.com/Support/Center/p/T325446">GridControl - How to add a check box to a column header</a></strong></p>

<br/>


