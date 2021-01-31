using SnowFlakeGamesAssets.TaurusDungeonGenerator.Structure;
using SnowFlakeGamesAssets.TaurusDungeonGenerator.Utils;
using SnowFlakeGamesAssets.TaurusDungeonGenerator.Component;
using System;
using System.Collections.Generic;
using TaurusDungeonGenerator.Example.Scripts;
using UnityEngine;
using SnowFlakeGamesAssets.TaurusDungeonGenerator;
using System.Linq;

public enum DungeonLayout { Realistic, BranchingAndOptional, Nesting }
public class DungeonToyJourneyRoot : DungeonDemoRoot
{

	public event Action<DungeonStructure> OnToyDungeonRebuilt;

	[Header("Nur Hier Editiren")]
	[Range(0.0F, 5.0F)]
	public float Margin;
	[Range(0.0F, 5.0F)]
	public float BranchPercent;

	[Range(1, 3)]
	public uint OptionalEndPoints;

	public DungeonLayout DungeonLayoutFromYamel;
	
	[Header("Game Prefaps")]
	public List<GameObject> Player;
	public GameObject KeyPrefap;

	public DungeonNode EndgameRoom;

	public GameObject EndGameCollaiderObj;
	public GameObject LookedDoorPrefap;

	DungeonStructure DungeonStruct;
	/// <summary>
	/// Keys in DungeonDemoRootstructur.cs gut erklärt
	/// </summary>
	//protected Dictionary<string, AbstractDungeonStructure> DungeonStructures => _dungeonStructures;
	private void Awake()
	{
		//OnDungeonRebuilt += TryUnderstandCode_OnDungeonRebuilt;
		OnToyDungeonRebuilt += SpownPlayer;

	}
	new void Start()
    {

		//Load Settings aus yaml
		_dungeonStructures = LoadStructure();
		var selectedStructure = _dungeonStructures.Keys.ToList()[(int)DungeonLayoutFromYamel];
		// Build Dungeon
		BuildDungeon(_dungeonStructures[selectedStructure], seedText?.text?.GetHashCode() ?? 0, BranchPercent, Margin,
			new PrototypeDungeonGenerator.GenerationParameters
			{
				RequiredOptionalEndpointNumber = OptionalEndPoints
			});

		CreatEndgamePoint();
		SpownKeys();

	}
	// Überscheibe Update, sonst bei lehrtaste dungen weg
	new void Update()
	{

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

				Instantiate(Player[0], item.Room.transform.position, Quaternion.identity);
				break;
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

	private void BuildDungeon(
	AbstractDungeonStructure structure,
	int generationSeed,
	float branchPercent,
	float margin,
	PrototypeDungeonGenerator.GenerationParameters parameters)
	{
		if (structure.BranchDataWrapper != null)
			structure.BranchDataWrapper.BranchPercentage = branchPercent;

		structure.StructureMetaData.MarginUnit = margin;

		var generator = new PrototypeDungeonGenerator(structure, generationSeed, parameters);
		var prototypeDungeon = generator.BuildPrototype();
		DungeonStruct = prototypeDungeon.BuildDungeonInUnitySpace(transform);
		OnToyDungeonRebuilt?.Invoke(DungeonStruct);
	}
}
