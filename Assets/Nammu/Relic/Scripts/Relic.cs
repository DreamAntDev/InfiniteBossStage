using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBS.Resoruce;

[CreateAssetMenu(fileName = "Relic", menuName = "Scriptable Object/Relic", order = int.MaxValue)]
public class Relic : ScriptableObject
{
    [SerializeField]
    private string id;

    [SerializeField]
    private string context;

    [SerializeField]
    private Type.RelicRating rating;

    [SerializeField]
    private int dropProbability;

    [SerializeField]
    private Sprite relicSprite;

    public string ID
    {
        get { return id;}
    }
    public string Context
    {
        get { return context; }
    }
    public Type.RelicRating Rating
    {
        get { return rating; }
    }
    public int Probability
    {
        get { return dropProbability; }
    }

    public Sprite Sprite
    {
        get { return relicSprite; }
    }
}
