using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float incomingRange;
    [SerializeField] float passedPlayerRange;
    [SerializeField] float destroy_XPos;
    float range;
    Transform player;

    bool passedPlayer;
    bool isWalking = true;

    public static event Action foundPlayer;
    public static GameObject foundBy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        range = incomingRange;
        float randomMultiplier = UnityEngine.Random.Range(0.67f, 1.15f);
        walkSpeed *= randomMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isDead == false)
        {
            WalkForward();
            CheckIfPassedPlayer();
            CheckIfPlayerInRange();
            CheckIfReachedMaxDistance();
        }
    }

    void WalkForward()
    {
        if (isWalking) transform.Translate(transform.right * walkSpeed * Time.deltaTime);
    }

    void CheckIfPlayerInRange()
    {
        if (CheckDistanceToPlayer() < range && !GameManager.Instance.isHiding) FoundPlayer();
    }

    void CheckIfPassedPlayer()
    {
        if (transform.position.x < player.position.x && !passedPlayer)
        {
            passedPlayer = true;
            range = passedPlayerRange;
        }
    }

    float CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance;
    }

    void CheckIfReachedMaxDistance()
    {
        if (player.position.x - transform.position.x > destroy_XPos) DestroyEnemy();
    }

    void FoundPlayer()
    {
        Debug.Log("Player Was Found!");
        foundPlayer?.Invoke();
        isWalking = false;
        foundBy = gameObject;
    }

    void DestroyEnemy()
    { 
        Destroy(gameObject);
    }
}