using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType type;

    [SerializeField] private Health health;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected GemType gemType;


    protected Transform playerRef;
    new protected Rigidbody2D rigidbody2D;
    protected EnemyPool pool;

    protected virtual void Awake()
    {
        playerRef = Player.Instance.transform;
        rigidbody2D = GetComponent<Rigidbody2D>();

        health.OnDie += GenerateGem;
        health.OnDie += Release;
    }

    public virtual void OnGet()
    {
        gameObject.SetActive(true);
        transform.SetParent(EnemySpawnManager.Instance.enemyParent);
    }

    public virtual void OnRelease()
    {
        gameObject.SetActive(false);
    }

    private void GenerateGem()
    {
        var gem = GemManager.Instance.Get(gemType);
        gem.transform.position = transform.position;
    }

    private void Release()
    {
        EnemySpawnManager.Instance.ReleaseEnemy(this);
    }
}
