using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float range;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        WalkForward();
        CheckIfPlayerInRange();
    }

    void WalkForward()
    {
        if (CheckDistanceToPlayer() < range) DestroyEnemy();

        transform.Translate(transform.forward * walkSpeed * Time.deltaTime);
    }

    void CheckIfPlayerInRange()
    {
        if (CheckDistanceToPlayer() < range && !PlayerInteraction.Instance.isHiding) FoundPlayer();
    }

    float CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance;
    }

    void FoundPlayer()
    {
        Debug.Log("Game Over!");
    }

    void DestroyEnemy()
    {
        Debug.Log("Enemy was destroyed");
        Destroy(gameObject);
    }
}