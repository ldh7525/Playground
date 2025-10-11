using System;
using UnityEngine;

public enum EnemyType { Circle, Bat, Skeleton, Slime}

[CreateAssetMenu(fileName = "EnemyMapSO", menuName = "Game/EnemyMapSO")]
public class EnemyMapSO : ScriptableObject
{
    [Serializable]
    public struct EnemyData
    {
        public EnemyType enemy;
        public GameObject enemyPrefab;
    }

    public EnemyData[] enemyPrefabs;

    public GameObject MatchPrefab(EnemyType type)
    {
        for(int i=0;i<enemyPrefabs.Length;i++)
        {
            if (enemyPrefabs[i].enemy == type)
                return enemyPrefabs[i].enemyPrefab;
        }

        Debug.LogWarning($"enemyPrefabs에 안들어가있음 {type}");
        return null;
    }
}
