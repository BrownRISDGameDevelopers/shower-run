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
    
    void Awake()
    {
        _playerBody = gameObject.GetComponent<Rigidbody>();
        _actions = new PlayerInputActions();
        
        Debug.Assert(_playerBody is not null, "Player has no RigidBody attatched");
        Debug.Assert(_actions is not null, "Actions is null");
    }

    void FixedUpdate()
    {
        var inputDirection = _actions.gameplay.move.ReadValue<Vector2>();
        var moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);

        float relativeSpeedMultiplier = limit - Vector3.Dot(_playerBody.velocity, moveDirection.normalized);
        
        _playerBody.AddForce(moveDirection * (multiplier * relativeSpeedMultiplier));
    }

    private void OnEnable() { _actions.gameplay.Enable(); }
    private void OnDisable() { _actions.gameplay.Disable(); }
}