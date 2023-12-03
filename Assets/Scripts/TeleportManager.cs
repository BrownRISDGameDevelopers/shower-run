using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance { get; private set; }

    public int depth { get; private set; } = 0;

    [Header("Points need to be in the order of Party, Standard, Crush, Janitor, NoLights, Bathroom")]
    [SerializeField] Transform[] teleportToPoints;

    public static event Action WonGame;

    private void Start()
    {
        Instance = this; 
    }

    public TeleportController.HallwayTypes DetermineRoom(TeleportController.LeftOrRightDoor door)
    {
        switch(depth)
        {
            //depth of 0 means player is in hallway 0
            case 0:
                depth++;
                if (door == TeleportController.LeftOrRightDoor.left) return TeleportController.HallwayTypes.Party;

                else return TeleportController.HallwayTypes.Standard;

            case 1:
                depth++;
                if (door == TeleportController.LeftOrRightDoor.left) return TeleportController.HallwayTypes.Standard;

                else return TeleportController.HallwayTypes.Crush;

            case 2:
                depth++;
                if (door == TeleportController.LeftOrRightDoor.left) return TeleportController.HallwayTypes.Janitor;

                else return TeleportController.HallwayTypes.Standard;

            case 3:
                depth++;
                if (door == TeleportController.LeftOrRightDoor.left) return TeleportController.HallwayTypes.Standard;

                else return TeleportController.HallwayTypes.No_Lights;

            case 4:
                depth++;
                return TeleportController.HallwayTypes.Standard;

            case 5:
                depth++;
                WonGame?.Invoke();
                return TeleportController.HallwayTypes.Bathroom;

            default:
                Debug.LogError("depth was not valid, teleported to standard hallway");
                return TeleportController.HallwayTypes.Standard;
        }
    }

    public void TeleportPlayer(Transform player, TeleportController.LeftOrRightDoor door)
    {
        TeleportController.HallwayTypes hallway = DetermineRoom(door);

        if (depth == 6) return;

        //Teleporting
        player.position = teleportToPoints[DetermineTeleportIndex(hallway)].position;

        //Setting rotation
        // player.transform.rotation = teleportToPoints[DetermineTeleportIndex(hallway)].rotation;
        player.transform.rotation = Quaternion.Euler(0, 90, 0); // Faces forward along the positive X-axis


        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(System.Math.Abs(rb.velocity.z), 0, 0);
    }

    int DetermineTeleportIndex(TeleportController.HallwayTypes hallway)
    {
        switch(hallway)
        {
            case TeleportController.HallwayTypes.Party:
                return 0;
            case TeleportController.HallwayTypes.Standard:
                return 1;
            case TeleportController.HallwayTypes.Crush:
                return 2;
            case TeleportController.HallwayTypes.Janitor:
                return 3;
            case TeleportController.HallwayTypes.No_Lights:
                return 4;
            case TeleportController.HallwayTypes.Bathroom:
                return 5;
            default:
                return 0;
        }
    }
}
