using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[CustomEditor(typeof(TagManager))]
[CanEditMultipleObjects]
public class TagManagerEditor : Editor
{
    private Vector2 scrollPos;
    private static SerializedObject tagManager;

    public static List<string> tags = new List<string>();
    public static List<string> layers = new List<string>();

    private static bool showTags = false;
    private static bool showLayers = false;

    private static int maxTags = 10000;
    private static int maxLayers = 31;

    void OnEnable()
    {
        Refresh();
    }

    public static void Refresh()
    {
        tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        TagManager.Instance.Refresh();
        tags = new List<string>(UnityEditorInternal.InternalEditorUtility.tags);
        tags.Add("");
        layers = new List<string>(UnityEditorInternal.InternalEditorUtility.layers);
        layers.Add("");


    }

    public override void OnInspectorGUI()
    {
        RenderGUI();
    }

    public static void RenderGUI()
    {
        //base.OnInspectorGUI();
        if (showTags = EditorGUILayout.Foldout(showTags, "Tags"))
        {
            for (int i = 0; i < tags.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if(i==tags.Count-1)
                {
                    tags[i] = GUILayout.TextField(tags[i]);
                    if (!string.IsNullOrEmpty(tags[i]))
                    {
                        if (GUILayout.Button("Add"))
                        {
                            AddTag(tags[i]);
                            Refresh();
                        }
                    }

                }
                else
                {
                    EditorGUI.BeginDisabledGroup(true);
                    tags[i] = GUILayout.TextField(tags[i]);
                    EditorGUI.EndDisabledGroup();
                    if (GUILayout.Button("Remove"))
                    {
                        RemoveTag(tags[i]);
                        Refresh();
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
        if (showLayers = EditorGUILayout.Foldout(showLayers, "Layers"))
        {
            for (int i = 0; i < layers.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (i == layers.Count - 1)
                {
                    layers[i] = GUILayout.TextField(layers[i]);
                    if (!string.IsNullOrEmpty(layers[i]))
                    {
                        if (GUILayout.Button("Add"))
                        {
                            AddLayer(layers[i]);
                            Refresh();
                        }
                    }

                }
                else
                {
                    EditorGUI.BeginDisabledGroup(true);
                    layers[i] = GUILayout.TextField(layers[i]);
                    EditorGUI.EndDisabledGroup();
                    if (GUILayout.Button("Remove"))
                    {
                        RemoveLayer(layers[i]);
                        Refresh();
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }



    /// <summary>
    /// Adds the tag.
    /// </summary>
    /// <returns><c>true</c>, if tag was added, <c>false</c> otherwise.</returns>
    /// <param name="tagName">Tag name.</param>
    public static bool AddTag(string tagName)
    {
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        // Tags Property
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        if (tagsProp.arraySize >= maxTags)
        {
            Debug.Log("No more tags can be added to the Tags property. You have " + tagsProp.arraySize + " tags");
            return false;
        }
        // if not found, add it
        if (!PropertyExists(tagsProp, 0, tagsProp.arraySize, tagName))
        {
            int index = tagsProp.arraySize;
            // Insert new array element
            tagsProp.InsertArrayElementAtIndex(index);
            SerializedProperty sp = tagsProp.GetArrayElementAtIndex(index);
            // Set array element to tagName
            sp.stringValue = tagName;
            Debug.Log("Tag: " + tagName + " has been added");
            // Save settings
            tagManager.ApplyModifiedProperties();
            return true;
        }
        else
        {
            //Debug.Log ("Tag: " + tagName + " already exists");
        }
        return false;
    }
    /// <summary>
    /// Removes the tag.
    /// </summary>
    /// <returns><c>true</c>, if tag was removed, <c>false</c> otherwise.</returns>
    /// <param name="tagName">Tag name.</param>
    public static bool RemoveTag(string tagName)
    {

        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        // Tags Property
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        if (PropertyExists(tagsProp, 0, tagsProp.arraySize, tagName))
        {
            SerializedProperty sp;

            for (int i = 0, j = tagsProp.arraySize; i < j; i++)
            {

                sp = tagsProp.GetArrayElementAtIndex(i);
                if (sp.stringValue == tagName)
                {
                    tagsProp.DeleteArrayElementAtIndex(i);
                    Debug.Log("Tag: " + tagName + " has been removed");
                    // Save settings
                    tagManager.ApplyModifiedProperties();
                    return true;
                }

            }
        }

        return false;

    }

    /// <summary>
    /// Checks to see if tag exists.
    /// </summary>
    /// <returns><c>true</c>, if tag exists, <c>false</c> otherwise.</returns>
    /// <param name="tagName">Tag name.</param>
    public static bool TagExists(string tagName)
    {
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        // Layers Property
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        return PropertyExists(tagsProp, 0, maxTags, tagName);
    }
    /// <summary>
    /// Adds the layer.
    /// </summary>
    /// <returns><c>true</c>, if layer was added, <c>false</c> otherwise.</returns>
    /// <param name="layerName">Layer name.</param>
    public static bool AddLayer(string layerName)
    {
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        // Layers Property
        SerializedProperty layersProp = tagManager.FindProperty("layers");
        if (!PropertyExists(layersProp, 0, maxLayers, layerName))
        {
            SerializedProperty sp;
            // Start at layer 9th index -> 8 (zero based) => first 8 reserved for unity / greyed out
            for (int i = 8, j = maxLayers; i < j; i++)
            {
                sp = layersProp.GetArrayElementAtIndex(i);
                if (sp.stringValue == "")
                {
                    // Assign string value to layer
                    sp.stringValue = layerName;
                    Debug.Log("Layer: " + layerName + " has been added");
                    // Save settings
                    tagManager.ApplyModifiedProperties();
                    return true;
                }
                if (i == j)
                    Debug.Log("All allowed layers have been filled");
            }
        }
        else
        {
            //Debug.Log ("Layer: " + layerName + " already exists");
        }
        return false;
    }

    /// <summary>
    /// Removes the layer.
    /// </summary>
    /// <returns><c>true</c>, if layer was removed, <c>false</c> otherwise.</returns>
    /// <param name="layerName">Layer name.</param>
    public static bool RemoveLayer(string layerName)
    {

        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        // Tags Property
        SerializedProperty layersProp = tagManager.FindProperty("layers");

        if (PropertyExists(layersProp, 0, layersProp.arraySize, layerName))
        {
            SerializedProperty sp;

            for (int i = 0, j = layersProp.arraySize; i < j; i++)
            {

                sp = layersProp.GetArrayElementAtIndex(i);

                if (sp.stringValue == layerName)
                {
                    sp.stringValue = "";
                    Debug.Log("Layer: " + layerName + " has been removed");
                    // Save settings
                    tagManager.ApplyModifiedProperties();
                    return true;
                }

            }
        }

        return false;

    }
    /// <summary>
    /// Checks to see if layer exists.
    /// </summary>
    /// <returns><c>true</c>, if layer exists, <c>false</c> otherwise.</returns>
    /// <param name="layerName">Layer name.</param>
    public static bool LayerExists(string layerName)
    {
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        // Layers Property
        SerializedProperty layersProp = tagManager.FindProperty("layers");
        return PropertyExists(layersProp, 0, maxLayers, layerName);
    }
    /// <summary>
    /// Checks if the value exists in the property.
    /// </summary>
    /// <returns><c>true</c>, if exists was propertyed, <c>false</c> otherwise.</returns>
    /// <param name="property">Property.</param>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    /// <param name="value">Value.</param>
    private static bool PropertyExists(SerializedProperty property, int start, int end, string value)
    {
        for (int i = start; i < end; i++)
        {
            SerializedProperty t = property.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(value))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Searches for the index of a property.
    /// </summary>
    /// <returns><c>i=index of property</c>, if property exists, otherwise <c>-1</c> .</returns>
    /// <param name="property">Property.</param>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    /// <param name="value">Value.</param>
    private static int PropertyIndex(SerializedProperty property, int start, int end, string value)
    {
        for (int i = start; i < end; i++)
        {
            SerializedProperty t = property.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(value))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Searches for the index of a layer.
    /// </summary>
    /// <returns><c>i=index of layer</c>, if layer exists, otherwise <c>-1</c> .</returns>
    /// <param name="layerName">Layer name.</param>
    public static int LayerIndex(string layerName)
    {
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        // Layers Property
        SerializedProperty layersProp = tagManager.FindProperty("layers");
        return PropertyIndex(layersProp, 0, maxLayers, layerName);
    }

    /// <summary>
    /// Searches for the index of a property.
    /// </summary>
    /// <returns><c>i=index of tag</c>, if tag exists, otherwise <c>-1</c> .</returns>
    /// <param name="tagName">Tag name.</param>
    public static int TagIndex(string tagName)
    {
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        // Layers Property
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        return PropertyIndex(tagsProp, 0, maxTags, tagName);
    }
}

