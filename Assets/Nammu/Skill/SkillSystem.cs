using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Projector projector;

    [SerializeField]
    private Material arrowMaterial;

    [SerializeField]
    private Material aoeMaterial;

    [SerializeField]
    private List<SkillScriptableObject> skiles;

    private bool isActiveSkill = true;
    private void Awake()
    {
        isActiveSkill = false;
        projector.enabled = false;
    }
    public void SkillAttack(int i)
    {
        if (isActiveSkill)
            return;
        isActiveSkill = true;
        projector.enabled = true;
        var type = skiles[i - 1].Skile;

        if(type == SkillScriptableObject.SkileType.AOE || type == SkillScriptableObject.SkileType.Summons)
        {
            projector.material = aoeMaterial;
        }
        else
        {
            projector.material = arrowMaterial;
        }

        projector.orthographicSize = skiles[i - 1].Range;
    }

    internal void Stop()
    {
        projector.enabled = false;

        isActiveSkill = false;
    }
}
