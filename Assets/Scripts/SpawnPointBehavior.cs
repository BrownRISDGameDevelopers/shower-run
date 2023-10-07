using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehavior : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    public void OnActivated()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}