using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    private readonly Dictionary<string, ObjectPool<GameObject>> enemyPoolsDictionary = new();


    private void RegisterPool(GameObject prefab)
    {
        var pool = new ObjectPool<GameObject>
        (
            () => Instantiate(prefab),
            obj =>
            {
                obj.GetComponent<Enemy>().OnGet();
            }
            ,
            obj =>
            {
                obj.GetComponent<Enemy>().OnRelease();
            },
            defaultCapacity: 1
        );

        enemyPoolsDictionary.Add(prefab.name, pool);
    }

    public ObjectPool<GameObject> GetPool(GameObject prefab)
    {
        EnsurePool(prefab);

        return enemyPoolsDictionary[prefab.name];
    }

    public void EnsurePool(GameObject prefab)
    {
        if (enemyPoolsDictionary.ContainsKey(prefab.name)) return;
        RegisterPool(prefab);
    }
}
