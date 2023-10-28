using UnityEngine;

public class PlayerLook: MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1f;
    [SerializeField]
    private Transform playerTransform;
    
    private PlayerInputActions _actions;
    private float _upDownRotation = 0f;
    private Vector2 _inputLook;

    void Awake()
    {
        _actions = new PlayerInputActions();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() 
    {
        _inputLook = _actions.gameplay.look.ReadValue<Vector2>() * sensitivity; //* Time.deltaTime;

        _upDownRotation -= _inputLook.y;
        _upDownRotation = Mathf.Clamp(_upDownRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_upDownRotation, 0, 0);
        playerTransform.Rotate(Vector3.up * _inputLook.x);
    }
    
    private void OnEnable() { _actions.gameplay.Enable(); }
    private void OnDisable() { _actions.gameplay.Disable(); }
}