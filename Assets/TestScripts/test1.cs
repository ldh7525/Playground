using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class test1 : MonoBehaviour
{
    public ObjectPool<GameObject> pool;
    public List<GameObject> list;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>
        (
            () => new GameObject("PooledObject"),
            obj => obj.SetActive(true),
            obj => obj.SetActive(false),
            null,
            false,
            10,
            20
        );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            list.Add(pool.Get());

            Debug.Log("Get Object");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var target = list[0];
            pool.Release(target);
            list.Remove(target);
            Debug.Log("Rel Object");
        }
    }
}
