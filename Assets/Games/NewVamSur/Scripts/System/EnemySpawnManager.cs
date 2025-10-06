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
    public class Wave
    {
        public struct SpawnInfo
        {
            public EnemyType enemy;
            public int spawnCount;
        }

        public SpawnInfo[] spawnInfos;
        public float duration;
    }

    [SerializeField] private EnemyMapSO enemyMap;
    [SerializeField] private Wave[] waves;
    [SerializeField] private EnemyPool enemyPool;

    [SerializeField] private float waveInterval;
    [SerializeField] private float maxSpawnRange;
    [SerializeField] private float minSpawnRange;

    public Transform enemyParent;
    private Player player;

    private void Start()
    {
        player = Player.instance;
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

    private IEnumerator ExecuteWave(Wave.SpawnInfo[] spawnInfo, float duration)
    {
        int spawnInfoLength = spawnInfo.Length;

        float[] spawnInterval = new float[spawnInfoLength];
        for (int i = 0; i < spawnInfoLength; i++)
            spawnInterval[i] = duration / spawnInfo[i].spawnCount;
        float[] spawnTimer = spawnInterval;

        while (duration > 0)
        {
            for (int i = 0; i < spawnInfoLength; i++)
            {
                spawnTimer[i] -= Time.deltaTime;
                if (spawnTimer[i] <= 0)
                {
                    SpawnEnemy(spawnInfo[i].enemy);
                    spawnTimer[i] = spawnInterval[i];
                }
            }

            duration -= Time.deltaTime;
            yield return null;
        }
    }

    private void SpawnEnemy(EnemyType enemyType)
    {
        GameObject prefab = enemyMap.enemyPrefabs.Where(e => e.enemy == enemyType).ToArray()[0].enemyPrefab;
        float theta = Random.Range(0, Mathf.PI * 2);
        Vector2 vec = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
        var enemy = enemyPool.GetPool(prefab).Get();

        enemy.transform.position = player.transform.position + (vec * Random.Range(minSpawnRange, maxSpawnRange)).UnsqueezeZ();
    }
}
