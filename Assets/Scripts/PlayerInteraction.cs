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
    private bool _isHiding = false;
    // Store direction that interactable faces, so that character exits in that direction
    private Quaternion hidingSpotRotation;

    


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
        
        if(_isHiding) {

            ExitHiding();
            
        } else
        {
            if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out var hit, interactRange, _interactLayer))
            {
                EnterHiding(hit.transform); 
            }
        }  
        
        
    }

    private void EnterHiding(Transform other)
    {
            _playerBody.isKinematic = true;
            _playerBody.velocity = Vector3.zero;

            // store hiding spot rotation
            hidingSpotRotation = other.rotation;

            var o = gameObject.transform;
            o.position = new Vector3(other.position.x, o.position.y, other.position.z);
            o.rotation = Quaternion.Euler(0, other.rotation.eulerAngles.y, 0);
            _isHiding = true;
            // TODO: limit rotation when hiding
    }

    private void ExitHiding()
    {
        _isHiding = false;
        _playerBody.isKinematic = false;

        // Move in the direction the hiding spot looks 
        gameObject.transform.position += Quaternion.Euler(0, hidingSpotRotation.eulerAngles.y, 0) * Vector3.forward;
        // Look down the hall
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        // gameObject.transform.position += gameObject.transform.forward;
    }
}