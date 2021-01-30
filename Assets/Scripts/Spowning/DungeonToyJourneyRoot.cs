using SnowFlakeGamesAssets.TaurusDungeonGenerator.Structure;
using SnowFlakeGamesAssets.TaurusDungeonGenerator.Utils;
using SnowFlakeGamesAssets.TaurusDungeonGenerator.Component;
using System;
using System.Collections.Generic;
using TaurusDungeonGenerator.Example.Scripts;
using UnityEngine;

public class DungeonToyJourneyRoot : DungeonDemoRoot
{
    public List<GameObject> Player;
	public GameObject KeyPrefap;

	public DungeonNode EndgameRoom;

	public GameObject EndGameCollaiderObj;
	public GameObject LookedDoorPrefap;
	/// <summary>
	/// Keys in DungeonDemoRootstructur.cs gut erklärt
	/// </summary>
	protected Dictionary<string, AbstractDungeonStructure> DungeonStructures => _dungeonStructures;

	new void Start()
    {
		//OnDungeonRebuilt += TryUnderstandCode_OnDungeonRebuilt;
		OnDungeonRebuilt += SpownPlayer;
		base.Start();

		CreatEndgamePoint();
		SpownKeys();

	}

	private void CreatEndgamePoint()
	{
		Instantiate(EndGameCollaiderObj, EndgameRoom.Room.transform.position, Quaternion.identity);

		foreach (var item in EndgameRoom.Room.GetConnections())
		{
			// Verschliest die offene Türe zum ende mit einem Key
			if(item.State == RoomConnector.ConnectionState.CONNECTED)
			{
				var door = Instantiate(LookedDoorPrefap, item.transform.position, item.transform.rotation, EndgameRoom.Room.transform);
				door.AddComponent<DoorKeyCode>().KeyDoorName = "endkey";
				item.closedPrefab = door;
				PrototypeDungeon.CreateClosedReplacement(item);
			}
		}
	}

	private void SpownKeys()
	{
		if (KeyPrefap == null )
			return;

		foreach (var item in TreeExtensions.TraverseDepthFirstReverse(EndgameRoom))
		{
			if(item.Style == "DungeonGenerationTest/BigRoom")
			{
				Instantiate(KeyPrefap, item.Room.transform.position, Quaternion.identity);
			}
		}
	}

	private void SpownPlayer(DungeonStructure obj)
	{
		// Main 0.0.0. Position
		//Instantiate(Player[0], obj.StartElement.Room.transform.position, Quaternion.identity);
		EndgameRoom = obj.StartElement;
		// Bei Multiplayer mehr Spown Plätze
		foreach (var item in TreeExtensions.TraverseDepthFirstReverse(obj.StartElement))
		{
			//Styls:
			//DungeonGenerationTest/CorrX
			//DungeonGenerationTest/BigRoom
			//DungeonGenerationTest/CorridorsNormalBig
			//DungeonGenerationTest/MiddleRoom
			// print(item.Style);

			if(item.Style == "DungeonGenerationTest/EndRoom")
			{
				if (EndgameRoom == item)
				{
					continue;
				}
				if(item.Room.transform.position == Vector3.zero)
				{

				}

				Instantiate(Player[0], item.Room.transform.position, Quaternion.identity);
			}
		}
	}

	private void TryUnderstandCode_OnDungeonRebuilt(DungeonStructure obj)
	{
		// zum Testen
		var node = String.Format("{0}, {1}, {2}, pro: {3}", obj.StartElement.Style, obj.StartElement.Value, obj.StartElement.Room.name, obj.StartElement.Room.transform.position);
		print(node);
		Instantiate(Player[0], obj.StartElement.Room.transform.position, Quaternion.identity);

	}
}
