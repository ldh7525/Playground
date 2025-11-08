using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PredatorFish : MonoBehaviour
{
    private enum PredatorState {Idle, Chase, Return};

    [SerializeField] private float moveForce = 4f;      // 추진력
    [SerializeField] private float maxSpeed = 3f;       // 최고 속도
    [SerializeField] private float waterDrag = 2f;      // 물 저항
    [SerializeField] private float chaseRange = 8f;     // 플레이어를 인식하는 거리
    [SerializeField] private float stopRange = 1.5f;    // 공격/멈춤 거리
    [SerializeField] private float wanderChangeInterval = 2f; // 랜덤 방향 전환 주기

    [SerializeField] private Rigidbody2D rb;
    private Transform player;
    private Vector2 wanderDir;
    private float wanderTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = waterDrag;

        // Player를 태그로 찾는다고 가정
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        Vector2 targetDir = Vector2.zero;

        if (player != null)
        {
            float dist = Vector2.Distance(transform.position, player.position);

            if (dist < chaseRange && dist > stopRange)
            {
                // 추적 모드
                targetDir = (player.position - transform.position).normalized;
            }
            else
            {
                // 배회 모드
                wanderTimer -= Time.deltaTime;
                if (wanderTimer <= 0f)
                {
                    wanderDir = Random.insideUnitCircle.normalized;
                    wanderTimer = wanderChangeInterval;
                }
                targetDir = wanderDir;
            }
        }

        // 힘을 가한다
        rb.AddForce(targetDir * moveForce);

        // 최고 속도 제한
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        // 보는 방향을 속도에 맞춤
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
