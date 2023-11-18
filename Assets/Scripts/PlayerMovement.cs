using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float multiplier;
    [SerializeField] private float limit;
    
    private Rigidbody _playerBody;
    private PlayerInputActions _actions;
    private AudioSource running;
    private bool moveAudioStarted = false;
    
    void Awake()
    {
        _playerBody = gameObject.GetComponent<Rigidbody>();
        _actions = new PlayerInputActions();
        
        Debug.Assert(_playerBody is not null, "Player has no RigidBody attatched");
        Debug.Assert(_actions is not null, "Actions is null");
        running = _playerBody.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        var inputDirection = _actions.gameplay.move.ReadValue<Vector2>();
        var moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);

        float relativeSpeedMultiplier = limit - Mathf.Clamp(Mathf.Abs(Vector3.Dot(_playerBody.velocity, moveDirection.normalized)), 0f, 1f);
        bool receivedMoveInput = ReceiveMoveInput(moveDirection);

        if (receivedMoveInput)
        {
            if (!moveAudioStarted)
            {
                running.Play();
                moveAudioStarted = true;
            }
            _playerBody.AddRelativeForce(moveDirection.normalized * (multiplier * relativeSpeedMultiplier));
        }
        else
        {
            running.Pause();
            moveAudioStarted = false;
        }
    }

    private bool ReceiveMoveInput(Vector3 moveDirection)
    {
        return (Mathf.Abs(moveDirection.x) > 0f || Mathf.Abs(moveDirection.z) > 0f) && !GameManager.Instance.isHiding;
    }

    private void OnEnable() { _actions.gameplay.Enable(); }
    private void OnDisable() { _actions.gameplay.Disable(); }
}