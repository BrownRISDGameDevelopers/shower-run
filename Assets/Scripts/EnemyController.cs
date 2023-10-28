using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        range = incomingRange;
    }

    // Update is called once per frame
    void Update()
    {
        WalkForward();
        CheckIfPassedPlayer();
        CheckIfPlayerInRange();
        CheckIfReachedMaxDistance();
    }

    void WalkForward()
    {
        if(isWalking) transform.Translate(transform.right * walkSpeed * Time.deltaTime);
    }

    void CheckIfPlayerInRange()
    {
        if (CheckDistanceToPlayer() < range && !PlayerInteraction.Instance._isHiding) FoundPlayer();
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
        if (transform.position.x < destroy_XPos) DestroyEnemy();
    }

    void FoundPlayer()
    {
        Debug.Log("Player Was Found!");
        isWalking = false;
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}