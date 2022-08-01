To submit, please, read the following and create your branch. Push as always, once your solution is ready - create a pull request. We will check it and let you know the results.

# Test Project:

  The idea is to create a very simple app which should display a minimalistic window and can <ins>work as a standalone Windows app and Revit add-in<ins>.
  
### Requirements:
  The window should have <b>Text</b>, <b>Input</b> and <b>Button</b> controls. The window should look the same no matter how we open it, either from WPF.exe or from Revit command. The window should be scalable, window's Title should display the version of the add-in, parsed from this url: https://apps.autodesk.com/RVT/en/Detail/Index?id=3837838607913367957

  <b>Running standalone</b>: Text field should display what user types in Input, initial content doesn’t matter. Button should launch Revit if it’s installed on the PC and close the window.
  
  <b>Running from Revit</b>: window should not be visible in the OS taskbar and should block Revit main window. Text field should display the number of triangles for the geometry in the current view. Input should contain the name of the project. Button should close the window and “save as” current project under the name from Input.

## Doesn’t matter:
-	WPF styles
-	The way of opening the dialog from Revit

## Estimation Criteria:
-	Multiple Revit versions support
-	Shared code base
-	Exceptions handling
-	Use of WPF patterns
- Reliability of getting and parsing live add-in version number
-	Understanding of Revit API and basic 3D principles
