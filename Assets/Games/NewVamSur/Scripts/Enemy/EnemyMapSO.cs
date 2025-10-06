using UnityEngine;

public enum EnemyType { Circle, Bat, Skeleton, Slime}

[CreateAssetMenu(fileName = "EnemyMapSO", menuName = "Game/EnemyMapSO")]
public class EnemyMapSO : ScriptableObject
{
    public struct EnemyData
    {
        public EnemyType enemy;
        public GameObject enemyPrefab;
    }

    public EnemyData[] enemyPrefabs;
}
