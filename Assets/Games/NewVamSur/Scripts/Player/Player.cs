using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private PlayerMove playerMove;
    public PlayerLevel playerLevel;
    public PlayerItem playeritem;
    [SerializeField] private Health health;


    protected override void Awake()
    {
        base.Awake();

    }

    private void FixedUpdate()
    {
        playerMove.Move();
    }

    // test 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckAndAttack();
        }
    }

    public float attackRange;
    public float attackDamage;
    public float x;
    void CheckAndAttack()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D enemy in enemiesInRange)
            enemy.GetComponent<Health>().TakeDamage(attackDamage);

        if (enemiesInRange.Length > 0)
        {
            // 공격 이펙트나 사운드 재생
            Debug.Log($"{enemiesInRange.Length}개의 대상을 공격했습니다!");
        }
    }
}
