using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public DungeonToyJourneyRoot dtjr;
    public List<EnemyAgent> enemies;

    void Start()
    {
        MusicManager.Instance.PlayGameMusic();
        dtjr.Init(Random.Range(int.MinValue, int.MaxValue));

        foreach (EnemyAgent ea in enemies) {
            Instantiate(ea.gameObject, RandomNavSphere(Vector3.zero, 500, -1), Quaternion.identity);
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
