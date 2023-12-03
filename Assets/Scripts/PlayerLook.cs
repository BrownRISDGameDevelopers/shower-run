using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1f;

    [SerializeField] private bool godMode;

    [SerializeField] private float enemyHeight;

    private PlayerInputActions _actions;
    private float _verticalLook = 0f;
    private float _horizontalLook = 0f;

    bool canLook = true;

    void Awake()
    {
        _actions = new PlayerInputActions();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!canLook) return;

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
            if (objectEulerAngles.y <= 0)
            {
                _horizontalLook = Mathf.Clamp(_horizontalLook + inputLook.x, objectEulerAngles.y - 90f, objectEulerAngles.y + 90f);
                transform.localRotation = Quaternion.Euler(_verticalLook, _horizontalLook, 0);
            }
            else
            {
                _horizontalLook = Mathf.Clamp(_horizontalLook + inputLook.x, objectEulerAngles.y + 90f, objectEulerAngles.y + 270f);
                transform.localRotation = Quaternion.Euler(_verticalLook, _horizontalLook, 0);
            }

        }
        else
        {
            player.transform.Rotate(new Vector3(0f, inputLook.x, 0f));
        }
    }

    void OnFound()
    {
        if (!godMode) {
            canLook = false;
            Camera.main.transform.LookAt(EnemyController.foundBy.transform.position + new Vector3(0, enemyHeight, 0));
        }
    }

    public void OnEnable()
    {
        _actions.gameplay.Enable();
        EnemyController.foundPlayer += OnFound;
    }
    public void OnDisable()
    {
        _actions.gameplay.Disable();
        EnemyController.foundPlayer -= OnFound;
    }
}