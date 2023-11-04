using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField] Transform teleportToPoint;
    Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player.gameObject) return;

        //Teleporting
        player.transform.position = teleportToPoint.position;

        //Setting rotation
        player.transform.rotation = teleportToPoint.rotation;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
    }
}