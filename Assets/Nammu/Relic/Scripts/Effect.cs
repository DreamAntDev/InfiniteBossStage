using IBS.Resoruce;
using UnityEngine;

[System.Serializable]
public class Effect
{
    [SerializeField]
    private Type.RelicEffect effectType;
    [SerializeField]
    private int effect;

    public Type.RelicEffect EffectType
    {
        get => effectType;
    }
    public int EffectValue
    {
        get => effect;
    }


}
