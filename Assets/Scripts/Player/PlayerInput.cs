using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAction;
    private InputActionMap playerMap;
    private InputAction moveAction;

    public Vector2 inputDirection {get; private set;} = Vector2.zero;

    private void Awake()
    {
        playerMap = inputAction.FindActionMap("Player", throwIfNotFound: true);
        moveAction = playerMap.FindAction("Move", throwIfNotFound: true);

        playerMap.Enable();
    }

    private void Update()
    {
        inputDirection = moveAction.ReadValue<Vector2>();
    }
}
