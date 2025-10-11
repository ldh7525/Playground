//using UnityEngine;
//using System;

//[RequireComponent(typeof(Rigidbody))]
//public class FrictionMovement : MonoBehaviour
//{
//    public const float HITSCALE = 6f;

//    [SerializeField] private float staticAcceleration = 60f;
//    [SerializeField] private float kineticAcceleration = 10f;
//    [SerializeField] public float maxVelocity = 5f;
//    [SerializeField] private float frictionThreshold = 0.4f;

//    public Vector2 moveVector { get; private set; }
//    private Vector2 _moveVector;

//    public event Action<Vector2> onKnockback;

//    new private Rigidbody rigidbody;

//    private Vector2 localVelocity = Vector2.zero;

//    private void Awake()
//    {
//        rigidbody = GetComponent<Rigidbody>();

//        //if (TryGetComponent(out Health health))
//        //{
//        //    health.onDamage += damage => Knockback(damage.knockback);
//        //}
//    }

//    private void FixedUpdate()
//    {
//        Vector2 velocity = rigidbody.linearVelocity.SqueezeY();

//        float maxDelta = staticAcceleration * Time.deltaTime;
//        if ((localVelocity - velocity).sqrMagnitude > frictionThreshold * frictionThreshold)
//        {
//            maxDelta = kineticAcceleration * Time.deltaTime;
//        }

//        // 조작감 개선을 위해 정지 시 localVelocity 조정.
//        if (velocity.sqrMagnitude < 0.5f)
//        {
//            localVelocity *= 0.5f + velocity.sqrMagnitude;
//        }

//        Vector2 targetVelocity = maxVelocity * velocityMultiplier.Value * _moveVector.normalized;
//        AdjustVector(ref targetVelocity, ref localVelocity, maxDelta);

//        AdjustVector(ref localVelocity, ref velocity, maxDelta);

//        rigidbody.linearVelocity = new Vector3(velocity.x, rigidbody.linearVelocity.y, velocity.y);

//        moveVector = _moveVector;
//        _moveVector = Vector2.zero;
//    }

//    private void AdjustVector(ref Vector2 a, ref Vector2 b, float maxDelta)
//    {
//        Vector2 d = b - a;
//        d.Normalize();
//        a += d * maxDelta;
//        b -= d * maxDelta;

//        if (Vector2.Dot(b - a, d) < 0)
//        {
//            a = (a + b) * 0.5f;
//            b = a;
//        }
//    }

//    /// <summary>
//    /// 목표 이동 방향을 지정한 벡터로 설정. 매 FixedUpdate마다 호출 권장.
//    /// moveVector는 정규화할 필요 없음.
//    /// </summary>
//    public void Move(Vector2 moveVector)
//    {
//        _moveVector += moveVector;
//    }

//    public void AddVelocity(Vector2 v)
//    {
//        rigidbody.linearVelocity += v.UnsqueezeY();
//    }

//    public void SetVelocity(Vector2 v)
//    {
//        rigidbody.linearVelocity = new Vector3(v.x, rigidbody.linearVelocity.y, v.y);
//    }

//    /// <summary>
//    /// 같은 방향이면 Add, 반대 방향이면 Set 처럼 동작
//    /// </summary>
//    public void ImpactVelocity(Vector2 v)
//    {
//        Vector2 velocity = rigidbody.linearVelocity.SqueezeY();

//        float m = Vector2.Dot(velocity, v.normalized);
//        if (m < 0)
//            velocity += -m * v.normalized;

//        velocity += v;
//        rigidbody.linearVelocity = new Vector3(velocity.x, rigidbody.linearVelocity.y, velocity.y);
//    }

//    public void Knockback(Vector2 knockback)
//    {
//        // 정확한 이동을 위해선 마찰력이 0이어야 함.
//        // Collider의 PhysicsMaterial을 마찰력이 없는 것으로 설정할것.

//        float knockbackDistance = knockback.magnitude;

//        Vector2 v = knockback.normalized * (Mathf.Sqrt(2 * kineticAcceleration * knockbackDistance));

//        ImpactVelocity(v);

//        onKnockback?.Invoke(knockback);
//    }

//    public void ResetMovement()
//    {
//        rigidbody.linearVelocity = Vector3.zero;
//        localVelocity = Vector2.zero;
//    }
//}
