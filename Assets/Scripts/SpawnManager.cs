using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Transform player;

    //List of all hallway segments. Each segment is a trigger
    [SerializeField] public Transform[] hallwaySegments;

    //Number of Enemy Spawns in the Hallway
    [SerializeField] int numberOfEnemySpawns;

    //The hallway indexes where the enemy will spawn
    List<int> indexesToSpawnAt = new List<int>();

    public List<float> x_SpawnPoints { get; private set; } = new List<float>();

    [SerializeField] Transform mobSpawnSpot;
    [SerializeField] GameObject mob;

    bool canSpawnEnemy = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        ChooseRandomSegments();
        ChooseSpawnPoints();
    }

    private void FixedUpdate()
    {
        CheckPlayerXBounds();
    }

    void CheckPlayerXBounds()
    {
        for(int i = 0; i < x_SpawnPoints.Count; i++)
        {
            int playerXPosRounded = Mathf.RoundToInt(player.position.x);
            int spawnPointRounded = Mathf.RoundToInt(x_SpawnPoints[i]);

            if (playerXPosRounded == spawnPointRounded && canSpawnEnemy)
            {
                canSpawnEnemy = false;
                StartCoroutine(SpawnCooldown());

                x_SpawnPoints.Remove(x_SpawnPoints[i]);
                SpawnMob();
                break;
            }
        }
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(.25f);
        canSpawnEnemy = true;
    }

    void SpawnMob()
    {
        Instantiate(mob, mobSpawnSpot.position + new Vector3(0, 1, 0), mobSpawnSpot.rotation);
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

            Debug.Log("Value is : " + spawnXPos);
            x_SpawnPoints.Add(spawnXPos);
        }
    }
}