using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycasting")]
    [SerializeField] private float interactRange = 2.5f;
    [FormerlySerializedAs("raycastPosition")] [SerializeField] private Transform raycastOrigin; //Where the raycast is shot from on the player's body
    
    private LayerMask _interactLayer; 

    void Start()
    { 
        _interactLayer = LayerMask.NameToLayer("Interactable"); 
    }
    
    public void OnInteract()
    {
        Debug.Log("Interact");
        RaycastHit hit;
        Debug.DrawRay(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward) * interactRange);
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward), out hit, interactRange, _interactLayer))
        {
            Debug.Log("Object was interacted with");
        }
    }
}