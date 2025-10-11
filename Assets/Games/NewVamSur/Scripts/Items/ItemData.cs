using UnityEngine;

[System.Serializable]
public class ItemLevelData
{
    public int level;
    public float damage;
    public float cooldown;
    public float range;
    public int projectileCount;
    public string description;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Game/ItemData")]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public Sprite icon;
    public ItemType itemType;
    public int maxLevel = 5;

    [Header("Item Prefab Reference")]
    public GameObject itemPrefab; // 실제 MonoBehaviour 아이템 프리팹

    [Header("Level Data")]
    public ItemLevelData[] levelData;

    public ItemLevelData GetLevelData(int level)
    {
        level = Mathf.Clamp(level, 1, levelData.Length);
        return levelData[level - 1];
    }
}
