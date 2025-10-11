using System;
using UnityEngine;

/// <summary>
/// level, experience
/// </summary>
public class PlayerLevel : MonoBehaviour
{
    public int level { get; private set; }
    public int exp { get; private set; }

    [SerializeField]
    private int[] expToNextLevel;
    public Action OnLevelup;


    private void Init()
    {
        level = 1;
        exp = 0;
    }

    public void AddExperience(int amount)
    {
        exp += amount;
        if (exp > expToNextLevel[level - 1])
            LevelUp();
    }

    private void LevelUp()
    {
        level += 1;
        exp = 0;
        OnLevelup?.Invoke();    
    }
}
