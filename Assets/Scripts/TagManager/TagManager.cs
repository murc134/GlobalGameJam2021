using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "C4rtwork/Tag Manager")]
public class TagManager : ScriptableObject
{
    private static TagManager instance = null;

    public static TagManager Instance { 
        get
        {
            if (instance == null) { instance = Resources.Load<TagManager>("TagManager"); }
            return instance;
        } 
    }

    [SerializeField]
    private List<string> tags = new List<string>();
    [SerializeField]
    private List<string> layers = new List<string>();

    public static List<string> Tags
    {
        get
        {
            return Instance.tags;
        }
    }

    public static List<string> Layers
    {
        get
        {
            return Instance.layers;
        }
    }

    public static List<TaggedObject> FindObjectsWithTag(string tag)
    {
        List<TaggedObject> list = new List<TaggedObject>();
        TaggedObject[] array = GameObject.FindObjectsOfType<TaggedObject>();
        for (int i = 0; i < array.Length; i++)
        {
            if (!list.Contains(array[i]) && (array[i].Tags.Contains(tag) || array[i].tag == tag))
            {
                list.Add(array[i]);
            }
        }
        return list;
    }

    public static TaggedObject FindObjectWithTag(string tag)
    {
        TaggedObject[] array = GameObject.FindObjectsOfType<TaggedObject>();
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Tags.Contains(tag) || array[i].tag == tag)
            {
                return array[i];
            }
        }
        return null;
    }

    public static List<T> FindObjectsWithTag<T>(string tag)
    {
        List<T> list = new List<T>();
        TaggedObject[] array = GameObject.FindObjectsOfType<TaggedObject>();
        for (int i = 0; i < array.Length; i++)
        {
            T obj = array[i].gameObject.GetComponent<T>();

            if (obj != null)
            {
                if (!list.Contains(obj) && (array[i].Tags.Contains(tag) || array[i].tag == tag))
                {
                    list.Add(obj);
                }
            }
        }
        return list;
    }

    public static T FindObjectWithTag<T>(string tag)
    {
        TaggedObject[] array = GameObject.FindObjectsOfType<TaggedObject>();
        if (array.Length <= 0)
        {
            throw new IndexOutOfRangeException("There is no TaggedObjects in the scene");
        }
        for (int i = 0; i < array.Length; i++)
        {
            T obj = array[i].gameObject.GetComponent<T>();

            if (obj != null && (array[i].Tags.Contains(tag) || array[i].tag == tag))
            {
                return obj;
            }
        }
        throw new KeyNotFoundException("There is no TaggedObjects in the scene with the tag " + tag);

    }

#if UNITY_EDITOR
    public void Refresh()
    {
        layers = new List<string>(UnityEditorInternal.InternalEditorUtility.layers);
        tags = new List<string>(UnityEditorInternal.InternalEditorUtility.tags);
        tags.Remove("Untagged");
    }
#endif
}

