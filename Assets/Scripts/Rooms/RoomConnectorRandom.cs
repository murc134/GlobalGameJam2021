using SnowFlakeGamesAssets.TaurusDungeonGenerator.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConnectorRandom : RoomConnector
{
    public List<GameObject> closedPrefabList;
    public List<GameObject> openPrefabList;
	private void Awake()
	{
		closedPrefab = closedPrefabList[Random.Range(0, closedPrefabList.Count)];
		openPrefab = openPrefabList[Random.Range(0, openPrefabList.Count)];
	}
}
