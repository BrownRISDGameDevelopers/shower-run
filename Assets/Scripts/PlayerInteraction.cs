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
        if (GameManager.Instance.HideSpotRotation is not null || GameManager.Instance.isHiding)
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
        // Access eulerAngles with null-conditional operator
        Vector3 eulerAngles = GameManager.Instance.HideSpotRotation.Value.eulerAngles;
        if (eulerAngles != null) {
            selfTransform.rotation = Quaternion.Euler(0, eulerAngles.y, 0);
            // Do something with the non-null eulerAngles
        }
    }

    private void ExitHiding()
    {
        GameManager.Instance.isHiding = false;
        _playerBody.isKinematic = false;

        // Move in the direction the hiding spot looks 
        Vector3 eulerAngles = GameManager.Instance.HideSpotRotation.Value.eulerAngles;
        if (eulerAngles != null) {
            // Look down the hall  
            // Move in the direction the hiding spot looks 
            gameObject.transform.position += Quaternion.Euler(0, eulerAngles.y, 0) * Vector3.forward;
        }
        GameManager.Instance.HideSpotRotation = null;
        
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        // gameObject.transform.position += gameObject.transform.forward;
    }
}