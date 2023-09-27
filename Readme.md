<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128624780/15.2.9%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T375271)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WinForms Data Grid - Display a custom button within the Filter Panel 

> **Important**
>
> This example uses internal APIs that may change in newer versions.

This example demonstrates how to display custom buttons within the Grid control's Filter Panel. The example [creates a custom grid control](https://supportcenter.devexpress.com/ticket/details/e900/winforms-data-grid-how-to-create-a-gridview-descendant-and-use-it-at-design-time), draws custom buttons within its Filter Panel, and implements the click functionality for custom buttons.

![WinForms Data Grid - Add a button to the Filter Panel](https://raw.githubusercontent.com/DevExpress-Examples/gridview-how-to-add-a-custom-button-to-the-filterpanel-t375271/15.2.9+/media/74423eb1-12ba-11e6-80bf-00155d62480c.png)

### Implementation Notes

* To raise the event when the mouse button is released, update the `CustomGridView.UpdateButtonState` method. For example, pass a parameter into this method to indicate that the `CustomGridViewHandler.OnMouseUp` event occurs and fire the `Click` event.
* The `CustomGridView.UpdateButtonsRects` method specifies the distance between custom buttons and their size.


## Files to Review

* [CustomGrid.cs](./CS/CustomGrid.cs) (VB: [CustomGrid.vb](./VB/CustomGrid.vb))
* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))


## See Also

* [WinForms Data Grid - How to create a GridView descendant and use it at design time](https://supportcenter.devexpress.com/ticket/details/e900/winforms-data-grid-how-to-create-a-gridview-descendant-and-use-it-at-design-time)
* [How to add a custom button to a column header in a grid](https://supportcenter.devexpress.com/ticket/details/e2793/winforms-data-grid-how-to-display-a-custom-button-within-a-column-header)
* [How to add a check box to a column header](https://supportcenter.devexpress.com/ticket/details/t325446/winforms-data-grid-how-to-display-a-check-box-within-a-column-header)
