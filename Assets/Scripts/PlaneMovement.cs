using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] private float trailingDistance;
    [SerializeField] private float minSpeed;
    [SerializeField] private Rigidbody player;
    
    void Update()
    {
        gameObject.transform.position += new Vector3(math.max(player.velocity.x * Time.deltaTime, minSpeed * Time.deltaTime), 0f, 0f);
    }
}
