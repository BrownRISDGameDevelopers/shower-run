using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycasting")]
    [SerializeField] private float interactRange = 2.5f;
    [SerializeField] private Transform raycastOrigin; 
    
    private LayerMask _interactLayer; 

    void Start()
    { 
        _interactLayer = ~LayerMask.NameToLayer("Interactable"); 
    }
    
    public void OnInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, interactRange, _interactLayer))
        {
            Debug.Log("Interacted with object");
        }
    }
}