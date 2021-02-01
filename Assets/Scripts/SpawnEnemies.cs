using SnowFlakeGamesAssets.TaurusDungeonGenerator.Structure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    DungeonToyJourneyRoot dtjr;
    

    void Awake()
    {
        dtjr = GetComponent<DungeonToyJourneyRoot>();
        dtjr.OnToyDungeonRebuilt += Spawn;
    }

    // Update is called once per frame
    void Spawn(DungeonStructure obj)
    {
        Debug.Log("Spawn Enemies");
    }

    private void OnDisable()
    {
        GetComponent<DungeonToyJourneyRoot>().OnToyDungeonRebuilt -= Spawn;
    }
}
