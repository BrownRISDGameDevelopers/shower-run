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
    private Rigidbody _playerBody;

    void Start()
    { 
        _interactLayer = ~LayerMask.NameToLayer("Interactable"); 
    }

    void Awake()
    {
        _playerBody = gameObject.GetComponent<Rigidbody>();
    }
    
    public void OnInteract()
    {
        if (GameManager.Instance.isHiding)
        {
            ExitHiding();
            GameManager.Instance.isHiding = false;
        }
        else if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out var hit, interactRange, _interactLayer))
        {
           EnterHiding(hit.transform);
           GameManager.Instance.isHiding = true;
        }
    }

    private void EnterHiding(Transform other)
    {
            _playerBody.isKinematic = true;
            _playerBody.velocity = Vector3.zero;
            var o = gameObject.transform;
            o.position = other.position;
            o.rotation = other.rotation;
            // TODO: limit rotation when hiding
    }

    private void ExitHiding()
    {
        _playerBody.isKinematic = false;
        // TODO: Need to change this to follow the behavior talked about with producers
        gameObject.transform.position += gameObject.transform.forward;
    }
}