using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Related to the Raycast")]
    [SerializeField] float interactRange = 2.5f;
    [SerializeField] Transform raycastPosition; //Where the raycast is shot from on the player's body
    [SerializeField] LayerMask interactLayer;

    [Header("Player Input")]
    [SerializeField] KeyCode interactKey;

    // Update is called once per frame
    void Update()
    {
        CheckRaycast();
    }

    void CheckRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastPosition.position, transform.forward, out hit, interactRange, interactLayer))
        {
            LookingAtInteractableObject(hit);
        }
    }

    void LookingAtInteractableObject(RaycastHit hit)
    {
        if(Input.GetKeyDown(interactKey))
        {
            InteractedWithObject();
        }
    }

    void InteractedWithObject()
    {
        Debug.Log("Object was interacted with");
    }
}