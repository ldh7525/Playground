using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] new private Rigidbody2D rigidbody2D;
    [SerializeField] private PlayerInput playerInput;

    private Vector2 moveVector;

    private void Awake()
    {
        playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == "Move")
            moveVector = context.ReadValue<Vector2>();
    }

    public void Move()
    {
        rigidbody2D.linearVelocity = moveVector * moveSpeed;
    }

}
