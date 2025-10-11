using UnityEngine;
using UnityEngine.Pool;

public enum GemType { Blue, Green, Red }
public class GemManager : Singleton<GemManager>
{
    [SerializeField] private Sprite[] gemSprites;
    [SerializeField] private GameObject gemPrefab;

    public ObjectPool<GameObject> gemPool;

    protected override void Awake()
    {
        base.Awake();
        gemPool = new ObjectPool<GameObject>
        (
            () => Instantiate(gemPrefab),
            null,
            null,
            defaultCapacity: 5,
            maxSize: 20
        );
    }

    public GameObject Get(GemType type)
    {
        Gem gem = gemPool.Get().GetComponent<Gem>();
        var sprite = type switch
        {
            GemType.Blue => gemSprites[0],
            GemType.Green => gemSprites[1],
            GemType.Red => gemSprites[2],
            _ => throw new System.ArgumentOutOfRangeException(nameof(type), $"지원하지 않는 GemType: {type}")
        };
        gem.OnGet(sprite);

        return gem.gameObject;
    }

    public void Release()
    {

    }
}
