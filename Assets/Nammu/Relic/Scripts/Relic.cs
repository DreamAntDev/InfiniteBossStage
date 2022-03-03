using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBS.Resoruce;

[CreateAssetMenu(fileName = "Relic", menuName = "Scriptable Object/Relic", order = int.MaxValue)]
public class Relic : ScriptableObject
{
    [SerializeField]
    private int id;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private int level;

    [SerializeField]
    private string context;

    [SerializeField]
    private string option;

    [SerializeField]
    private Type.RelicRating rating;

    [SerializeField]
    private int dropProbability;

    [SerializeField]
    private Sprite relicSprite;

    public int ID
    {
        get { return id;}
    }

    public string Name
    {
        get { return itemName; }
    }

    public int Level
    {
        get { return level; }
    }
    public string Context
    {
        get { return context; }
    }
    public string Option
    {
        get { return option; }
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
