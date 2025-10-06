using UnityEngine;

public class Enemy : MonoBehaviour
{


    public virtual void OnGet()
    {
        gameObject.SetActive(true);
        transform.SetParent(EnemySpawnManager.instance.enemyParent);
    }

    public virtual void OnRelease()
    {
        gameObject.SetActive(false);
        transform.SetParent(null);
    }
}
