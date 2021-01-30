using UnityEngine;
using UnityEditor;
using System.Collections;

class TagManagerWindow : EditorWindow
{
    public Vector2 scrollPos = Vector2.zero;
    [MenuItem("C4rtwork/TagManager")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TagManagerWindow));
        TagManagerEditor.Refresh();
    }

    public TagManager TagManager { get { return TagManager.Instance; } }

    void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        TagManagerEditor.RenderGUI();
        GUILayout.EndScrollView();
    }
}