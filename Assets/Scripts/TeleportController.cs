using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField] Transform teleportToPoint;
    Transform player;

    public enum LeftOrRightDoor { left, right }
    public enum HallwayTypes { Party, Standard, Crush, Janitor, No_Lights, Bathroom }

    [SerializeField] LeftOrRightDoor thisDoor;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player.gameObject) return;

        TeleportManager.Instance.TeleportPlayer(player, thisDoor);
    }
}