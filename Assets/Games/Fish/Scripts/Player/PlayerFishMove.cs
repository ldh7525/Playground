using UnityEngine;

/// <summary>
/// Player의 이동을 담당 - Player에서 composition
/// </summary>
public class PlayerFishMove : MonoBehaviour
{
    [Header("Base Movement")]
    public float normalSwimForce = 12f;
    public float carryingSwimForce = 6f;
    public float maxSpeed = 7f;
    public float waterDrag = 3f;

    [Header("Game States")]
    public bool isCarryingFood = false;

    [SerializeField]  private Rigidbody2D rb;
     private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        SwimMovement();
        ApplyWaterPhysics();
    }

    void GetInput()
    {
        moveDirection = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;
    }

    void SwimMovement()
    {
        // 먹이 운반 상태에 따른 힘 조절
        float currentSwimForce = isCarryingFood ? carryingSwimForce : normalSwimForce;

        if (moveDirection.magnitude > 0.1f)
        {
            Vector2 force = moveDirection * currentSwimForce;
            rb.AddForce(force, ForceMode2D.Force);
        }
    }

    void ApplyWaterPhysics()
    {
        // 물 저항 시뮬레이션
        Vector2 resistance = -rb.linearVelocity * rb.linearVelocity.magnitude * waterDrag * 0.1f;
        rb.AddForce(resistance, ForceMode2D.Force);

        // 점진적 감속 효과
        if (moveDirection.magnitude < 0.1f && rb.linearVelocity.magnitude > 0.5f)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.fixedDeltaTime * 2f);
        }

        // 속도 제한
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    public void SetCarryingState(bool carrying)
    {
        isCarryingFood = carrying;
    }
}
