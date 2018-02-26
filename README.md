# Unity UI Lecture Feb 18

Project for the material from the lecture demonstration.
Remember to reference if anything is used in assignments.

## Useful information
* Remember to use **UnityEngine.UI** to access UI elements in code.
* When searching for UI information, ignore the GUI information. This is outdated since Unity 4.6.
* Storing a prefab will not store the editor set links to functions in scripts (e.g. button clicks). If you want to add click functions dynamically see the code below.
* Only public functions can be added to buttons through the editor, however you can add both private and public functions through code.
* Using white icons will allow you to recolour them in the editor as you wish, using black ones will not.
* If you set a UI elements position to stretch, you will not be able to access the Width and Height variables in code. Vice versa of you won't be able to access the edge values if you set the item to normal.
* Using a single pixel white image for your backgrounds or buttons will yield better performance. Unity will batch render all the images of the same source and draw them together. Using no images will not allow this to occur.
* Unity does not support shapes in the UI by default. To use shapes you need to use images directly or look into [9 sprite slicing](https://docs.unity3d.com/Manual/9SliceSprites.html). Using them as buttons will also still make them interactable as rectangles, to fix this look at [alphaHitTestMinimumThreshold](https://docs.unity3d.com/ScriptReference/UI.Image-alphaHitTestMinimumThreshold.html).
* If you're using text a lot, take look at using TextMeshPro instead of the default Unity Text. It uses shaders that dynamically set the resolution of the text (it ends up looking way better), and can also react to realtime lighting.
* Not UI related, however when using Unity and Git/GitHub make sure to set your asset serialization to Force Text. This is found under **Edit -> Project Settings -> Asset Serialization**

### Button Click Code
```csharp
Button m_button;

private void Awake()
{
  m_button = GetComponent<Button>();
  m_button.onClick.AddListener(DoStuffFunction);
}

void DoStuffFunction()
{
  // do something
}
```
See https://docs.unity3d.com/ScriptReference/UI.Button-onClick.html for more information.

### Modifying Text in Code
```csharp
Text m_text;
string changeTheText = "goodbye";

private void Awake()
{
  m_text = GetComponent<Text>();
  m_text.text = "Hello";
  changeTheText = m_text.text;
  // changeTheText becomes Hello
  // Can call the .text from any part of this script as long as the variable is set prior
}
```
