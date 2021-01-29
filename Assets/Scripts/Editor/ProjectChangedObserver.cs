using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
class ProjectChangedObserver
{
    static ProjectChangedObserver()
    {
        EditorSceneManager.sceneLoaded += OnSceneLoaded;
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private static void OnHierarchyChanged()
    {
        ApplyTags();
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyTags();
    }

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        ApplyTags();
    }

    private static void ApplyTags()
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            TaggedObject tobj = gameObjects[i].GetComponent<TaggedObject>();

            if (tobj == null)
            {
                tobj = gameObjects[i].AddComponent<TaggedObject>();
            }

            if ((!string.IsNullOrEmpty(tobj.tag) || tobj.tag != "Untagged") && !tobj.Tags.Contains(tobj.tag))
            {
                tobj.Tags.Add(tobj.tag);
            }
        }

    }
}