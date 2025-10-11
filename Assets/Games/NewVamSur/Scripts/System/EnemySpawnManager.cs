using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// wave manager
/// </summary>
/// 


public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    [Serializable]
    public class Wave
    {
        [Serializable]
        public struct SpawnInfo
        {
            public EnemyType enemyType;
            public int spawnCount;
        }

        public SpawnInfo[] spawnInfos;
        public float duration;
    }

    [SerializeField] private EnemyMapSO enemyMap;
    [SerializeField] private Wave[] waves;
    public EnemyPool enemyPool;

    [SerializeField] private float waveInterval;
    [SerializeField] private float maxSpawnRange;
    [SerializeField] private float minSpawnRange;

    public Transform enemyParent;
    private Player player;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        enemyParent = new GameObject("EnemyParent").transform;
        player = Player.Instance;
        StartCoroutine(StartWaves());
    }

    private IEnumerator StartWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            Wave wave = waves[i];
            yield return ExecuteWave(wave.spawnInfos, wave.duration);
            yield return new WaitForSeconds(waveInterval);
        }
    }

    private IEnumerator ExecuteWave(Wave.SpawnInfo[] spawnInfoList, float duration)
    {
        int spawnInfoLength = spawnInfoList.Length;

        float[] spawnInterval = new float[spawnInfoLength];
        for (int i = 0; i < spawnInfoLength; i++)
            spawnInterval[i] = duration / spawnInfoList[i].spawnCount;
        GameObject[] prefabList = spawnInfoList.Select(info => enemyMap.MatchPrefab(info.enemyType)).ToArray();

        float[] spawnTimer = spawnInterval.ToArray();
        while (duration > 0)
        {
            for (int i = 0; i < spawnInfoLength; i++)
            {
                spawnTimer[i] -= Time.deltaTime;
                if (spawnTimer[i] <= 0)
                {
                    SpawnEnemy(prefabList[i]);
                    spawnTimer[i] = spawnInterval[i];
                }
            }
            duration -= Time.deltaTime;
            yield return null;
        }
    }

    private void SpawnEnemy(GameObject prefab)
    {
        float theta = UnityEngine.Random.Range(0, Mathf.PI * 2);
        Vector2 vec = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
        var enemy = enemyPool.GetPool(prefab).Get();

        enemy.transform.position = player.transform.position + (vec * UnityEngine.Random.Range(minSpawnRange, maxSpawnRange)).UnsqueezeZ();
    }

    public void ReleaseEnemy(Enemy enemy)
    {
        GameObject prefab = enemyMap.enemyPrefabs.Where(e => e.enemy == enemy.type).ToList()[0].enemyPrefab;
        enemyPool.GetPool(prefab).Release(enemy.gameObject);
    }
}
