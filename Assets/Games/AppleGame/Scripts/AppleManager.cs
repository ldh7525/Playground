using UnityEngine;
using UnityEngine.Tilemaps;

public class AppleManager : MonoBehaviour
{
    [SerializeField] private GameObject apple;
    [SerializeField] private Grid grid;
    [SerializeField] int x;
    [SerializeField] int y;
    public static AppleManager instance;
    public float gravity;
    public float bouncePower;

    private void Awake()
    {
        grid = GetComponent<Grid>();

        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            {
                var worldPos = grid.GetCellCenterWorld(new Vector3Int(-8 + i, -4 + j));
                var a = Instantiate(apple, worldPos, Quaternion.identity);
                a.transform.SetParent(gameObject.transform);
            }
    }




}
