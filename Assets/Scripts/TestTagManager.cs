using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTagManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(TagManager.FindObjectWithTag<TestTagManager>("GameController").name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
