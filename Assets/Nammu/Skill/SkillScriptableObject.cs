using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Relic", menuName = "Scriptable Object/Skill", order = int.MaxValue)]
public class SkillScriptableObject : ScriptableObject
{
    [SerializeField]
    private string skileName;

    [SerializeField]
    private SkileType skileType;

    [SerializeField]
    private float skileReadyTime;

    [SerializeField]
    private float skileDamage;

    [SerializeField]
    private float range;

    public SkileType Skile
    {
        get => skileType;
    }

    public float ReadyTime
    {
        get => skileReadyTime;
    }
    
    public float Range
    {
        get => range;
    }




    public enum SkileType
    {
        Single,
        AOE,
        Projectile,
        Summons
    }

}
