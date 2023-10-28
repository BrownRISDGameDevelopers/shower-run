using UnityEngine;

public class PlayerLook: MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1f;
    
    private PlayerInputActions _actions;
    private float _verticalLook = 0f;
    private float _horizontalLook = 0f;

    void Awake()
    {
        _actions = new PlayerInputActions();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var inputLook = _actions.gameplay.look.ReadValue<Vector2>() * sensitivity;
        var player = GameManager.Instance.Player;
        
        // Camera movement (player does not move when rotating in y)
        _verticalLook = Mathf.Clamp(_verticalLook - inputLook.y, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_verticalLook, 0, 0);
        
        // Player movement (player actually rotates when rotating in x)
        var hideSpotRotation = GameManager.Instance.HideSpotRotation;
        if (hideSpotRotation is not null)
        {
            Vector3 objectEulerAngles = hideSpotRotation.Value.eulerAngles;
            _horizontalLook = Mathf.Clamp(_horizontalLook + inputLook.x, objectEulerAngles.y - 90f, objectEulerAngles.y + 90f);
            transform.localRotation = Quaternion.Euler(_verticalLook, _horizontalLook, 0);
        }
        else
        {
            player.transform.Rotate(new Vector3(0f, inputLook.x, 0f));
        }
    }
    
    private void OnEnable() { _actions.gameplay.Enable(); }
    private void OnDisable() { _actions.gameplay.Disable(); }
}