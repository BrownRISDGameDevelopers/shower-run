using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //List of all hallway segments. Each segment is a trigger
    [SerializeField] Transform[] hallwaySegments;

    //Number of Enemy Spawns in the Hallway
    [SerializeField] int numberOfEnemySpawns;

    //The hallway indexes where the enemy will spawn
    List<int> indexesToSpawnAt = new List<int>();

    public List<float> x_SpawnPoints { get; private set; } = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        ChooseRandomSegments();

        ChooseSpawnPoints();
    }

    void ChooseRandomSegments()
    {
        int pointsToCreate = numberOfEnemySpawns;

        while(pointsToCreate > 0)
        {
            int value = Random.Range(0, hallwaySegments.Length-1);

            if (!indexesToSpawnAt.Contains(value))
            {
                indexesToSpawnAt.Add(value);
                pointsToCreate--;
            }
        }
    }

    void ChooseSpawnPoints()
    {
        for(int i = 0; i < indexesToSpawnAt.Count; i++)
        {
            float xStartingPos = hallwaySegments[i].position.x;
            float xEndingPos = hallwaySegments[i+1].position.x;

            float spawnXPos = Random.Range(xStartingPos, xEndingPos);
            x_SpawnPoints.Add(spawnXPos);
        }
    }
}