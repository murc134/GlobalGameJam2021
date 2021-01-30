using SnowFlakeGamesAssets.TaurusDungeonGenerator.Structure;
using SnowFlakeGamesAssets.TaurusDungeonGenerator.Utils;
using System;
using System.Collections.Generic;
using TaurusDungeonGenerator.Example.Scripts;
using UnityEngine;

public class DungeonToyJourneyRoot : DungeonDemoRoot
{
    public List<GameObject> Player;
	public List<GameObject> Keys;

	public GameObject EndGameCollaiderObj;
	/// <summary>
	/// Keys in DungeonDemoRootstructur.cs gut erklärt
	/// </summary>
	protected Dictionary<string, AbstractDungeonStructure> DungeonStructures => _dungeonStructures;

	new void Start()
    {
		OnDungeonRebuilt += TryUnderstandCode_OnDungeonRebuilt;
		OnDungeonRebuilt += SpownPlayer;
		OnDungeonRebuilt += SpownKeys;

		base.Start();

	}

	private void SpownKeys(DungeonStructure obj)
	{
		if (Keys == null || Keys.Count == 0)
			return;

		foreach (var item in TreeExtensions.TraverseDepthFirst(obj.StartElement))
		{
			Instantiate(Keys[0], item.Room.transform.position, Quaternion.identity);
		}
	}

	private void SpownPlayer(DungeonStructure obj)
	{
		// Main Pos
		Instantiate(Player[0], obj.StartElement.Room.transform.position, Quaternion.identity);

		// Bei Multiplayer mehr Spown Plätze
		foreach (var item in TreeExtensions.TraverseDepthFirst(obj.StartElement))
		{
			//Styls:
			//DungeonGenerationTest/CorrX
			//DungeonGenerationTest/BigRoom
			//DungeonGenerationTest/CorridorsNormalBig
			//DungeonGenerationTest/MiddleRoom
			print(item.Style);

			if(item.Style == "DungeonGenerationTest/EndRoom")
			Instantiate(Player[0], item.Room.transform.position, Quaternion.identity);
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
