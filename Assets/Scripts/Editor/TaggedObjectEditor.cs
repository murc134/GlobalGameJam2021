using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[CustomEditor(typeof(TaggedObject))]
[CanEditMultipleObjects]
public class TaggedObjectEditor : Editor
{
    private static Vector2 scrollPos;
    private TaggedObject obj;

    private List<bool> hasTag;

    void OnEnable()
    {
        obj = (TaggedObject)target;

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        hasTag = new List<bool>();

        foreach (string tag in TagManager.Tags)
        {
            hasTag.Add(obj.Tags.Contains(tag));
        }

        GUILayout.BeginScrollView(scrollPos);
        //base.OnInspectorGUI();

        for (int i = 0; i < TagManager.Tags.Count; i++)
        {
            if (obj.gameObject.tag == TagManager.Tags[i])
            {
                var boldtext = new GUIStyle(GUI.skin.toggle);
                boldtext.fontStyle = FontStyle.BoldAndItalic;

                hasTag[i] = GUILayout.Toggle(hasTag[i], TagManager.Tags[i], boldtext);
            }
            else
            {
                hasTag[i] = GUILayout.Toggle(hasTag[i], TagManager.Tags[i]);
            }

            if (hasTag[i]) // I have the tag in UI
            {
                if (!obj.Tags.Contains(TagManager.Tags[i])) // I dont have tag on object
                {
                    obj.Tags.Add(TagManager.Tags[i]); // I add tag to object

                    if(string.IsNullOrEmpty(obj.tag) || obj.tag == "Untagged")
                    {
                        obj.tag = TagManager.Tags[i];
                    }
                }
            }
            else
            {
                if (obj.Tags.Contains(TagManager.Tags[i]))
                {
                    obj.Tags.Remove(TagManager.Tags[i]); // I remove tag from object

                    if (obj.tag == TagManager.Tags[i])
                    {
                        obj.tag = "Untagged";
                    }
                }
            }
        }
        if (GUILayout.Button("Edit"))
        {
            TagManagerWindow.ShowWindow();
        }
        GUILayout.EndScrollView();
    }


}

