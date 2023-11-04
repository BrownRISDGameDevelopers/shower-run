using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [Header("Raycasting")]
    [SerializeField] private float interactRange = 2.5f;
    [SerializeField] private Transform raycastOrigin;

    private LayerMask _interactLayer; 
    private Rigidbody _playerBody;
    // Store direction that interactable faces, so that character exits in that direction
    private Quaternion _hidingSpotRotation;
    
    void Awake()
    {
        _playerBody = gameObject.GetComponent<Rigidbody>();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        _interactLayer = ~LayerMask.NameToLayer("Interactable");
    }
    
    public void OnInteract()
    {
        if (GameManager.Instance.HideSpotRotation is not null)
        {
            ExitHiding();
        }
        else if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out var hit, interactRange, _interactLayer))
        {
            EnterHiding(hit.transform); 
        }  
    }

    private void EnterHiding(Transform other)
    {
        _playerBody.isKinematic = true;
        _playerBody.velocity = Vector3.zero;

        // store hiding spot rotation
        GameManager.Instance.HideSpotRotation = other.rotation;
        GameManager.Instance.isHiding = true;

        var selfTransform = gameObject.transform;
        selfTransform.position = new Vector3(other.position.x, selfTransform.position.y, other.position.z);
        selfTransform.rotation = Quaternion.Euler(0, _hidingSpotRotation.eulerAngles.y, 0);
    }

    private void ExitHiding()
    {
        GameManager.Instance.HideSpotRotation = null;
        GameManager.Instance.isHiding = false;
        _playerBody.isKinematic = false;

        // Move in the direction the hiding spot looks 
        gameObject.transform.position += Quaternion.Euler(0, _hidingSpotRotation.eulerAngles.y, 0) * Vector3.forward;
        // Look down the hall
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        // gameObject.transform.position += gameObject.transform.forward;
    }
}