using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorKeyControl : MonoBehaviour
{
    public bool IsOpen;
    public Renderer Door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player" && !IsOpen)
            OpenDoor();
	}

	private void OpenDoor()
	{
        IsOpen = true;
        Door.transform.DOMoveY(Door.transform.position.y + Door.bounds.size.y, 1);
	}
}
