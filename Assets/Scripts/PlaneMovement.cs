using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    /// <summary>
    /// The DeathPlane's movement behavior.
    /// There's two rules to its behavior, in descending priority
    /// 1. The DeathPlane will at most be <c>maxTrailingDistance</c> behind the player
    /// 2. The DeathPlane move at speed <c>minSpeed</c>
    /// </summary>
    [SerializeField] private float maxTrailingDistance;
    [SerializeField] private float minSpeed;

    void Update()
    {
        if (GameManager.Instance.isDead == false)
        {
            Vector3 planePos = gameObject.transform.position;

            if (IsFarFromPlayer())
            {
                gameObject.transform.position =
                    new Vector3(GameManager.Instance.Player.transform.position.x - maxTrailingDistance, planePos.y, planePos.z);
            }
            else
            {
                gameObject.transform.position += new Vector3(minSpeed * Time.deltaTime, 0f, 0f);
            }
        }
    }

    private bool IsFarFromPlayer()
    {
        return (GameManager.Instance.Player.transform.position - gameObject.transform.position).x > maxTrailingDistance;
    }
}
