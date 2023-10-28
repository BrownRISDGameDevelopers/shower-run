using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [Header("Related to the Raycast")]
    [SerializeField] float interactRange = 2.5f;
    [SerializeField] Transform raycastPosition; //Where the raycast is shot from on the player's body
    [SerializeField] LayerMask interactLayer;

    [Header("Player Input")]
    [SerializeField] KeyCode interactKey;

    //Temp location for the variable
    public bool isHiding { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRaycast();

        CheckLeaveHiding();
    }

    void CheckRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastPosition.position, transform.forward, out hit, interactRange, interactLayer))
        {
            GameObject interactableObject = hit.collider.gameObject;
            LookingAtInteractableObject(interactableObject);
        }
    }

    void LookingAtInteractableObject(GameObject obj)
    {
        if(Input.GetKeyDown(interactKey))
        {
            InteractedWithObject(obj);
        }
    }

    void InteractedWithObject(GameObject obj)
    {
        Debug.Log("Object was interacted with");
        StartCoroutine(TeleportPlayerIntoObject(obj));
    }

    IEnumerator TeleportPlayerIntoObject(GameObject obj)
    {
        transform.parent = obj.transform;
        transform.localPosition = Vector3.zero;

        //1 frame delay so no conflict with leaving hiding
        yield return null;

        isHiding = true;
    }

    void CheckLeaveHiding()
    {
        if(isHiding && Input.GetKeyDown(interactKey)) //Also check if player is in hiding
        {
            StartCoroutine(LeaveHiding());
        }
    }

    IEnumerator LeaveHiding()
    {
        //Getting the step out point
        Transform stepOutPoint = transform.parent.transform.Find("Player Step Out Point");

        //Setting player's position to the hiding step out point
        transform.position = stepOutPoint.position;

        transform.parent = null;

        yield return null;

        isHiding = false;
    }
}