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

        for(int i = 0; i<gameObjects.Length;i++)
        {
            TaggedObject tobj = gameObjects[i].GetComponent<TaggedObject>();

            if(tobj == null)
            {
                gameObjects[i].AddComponent<TaggedObject>();
            }
        }

    }
}