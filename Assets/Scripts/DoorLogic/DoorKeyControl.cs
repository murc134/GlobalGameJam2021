using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorKeyControl : MonoBehaviour
{
    public bool IsOpen;
    public Renderer Door;
    public float DoorOpeningDuration;
    // Start is called before the first frame update
    void Start()
    {
        OpenDoor();
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
        Door.transform.DORotate(new Vector3(0f, 100f, 0f), DoorOpeningDuration);
	}
}
