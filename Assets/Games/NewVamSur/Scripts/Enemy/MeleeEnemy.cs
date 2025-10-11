using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocity = (playerRef.position - transform.position).normalized * moveSpeed;
    }

    public override void OnGet()
    {
        base.OnGet();
    }

    public override void OnRelease()
    {
        base.OnRelease();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Player.Instance.GetComponent<Health>().TakeDamage(damage);
    }

}
