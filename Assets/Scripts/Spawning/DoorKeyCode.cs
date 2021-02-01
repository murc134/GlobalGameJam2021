using System;
using System.Collections.Generic;
using TaurusDungeonGenerator.Example.Scripts;
using UnityEngine;


internal class DoorKeyCode : MonoBehaviour
{
	public string KeyDoorName;


	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			print("you got key!");
			DoorKeyControl.PlayerGotEndkey = true;
			Destroy(gameObject);
		}

	}
}