using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

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
        Debug.Log("type : " + type);
        if (type == SkillScriptableObject.SkileType.AOE || type == SkillScriptableObject.SkileType.Summons)
        {
            projector.material = aoeMaterial;
        }
        else
        {
            projector.material = arrowMaterial;
        }

        projector.orthographicSize = skiles[i - 1].Range;

        

        //��� ����
        switch(type)
        {
            case SkillScriptableObject.SkileType.Summons:
                Summons(skiles[i - 1]);
                break;
            case SkillScriptableObject.SkileType.AOE:
                var coliders = Physics.OverlapSphere(transform.position, skiles[i - 1].Range);
                AOE(coliders, skiles[i - 1].Damage);
                break;
        }

        StartCoroutine(Timer(skiles[i - 1].ReadyTime));
    }

    IEnumerator Timer(float readyTime)
    {
        yield return new WaitForSeconds(readyTime);
        Stop();
    }

    private void AOE(Collider[] coliders, float damage)
    {
        foreach (var colider in coliders)
        {
            if (colider.gameObject.tag == "Player")
            {
                colider.GetComponent<PlayerController>().OnDamage(Convert.ToInt32(damage));
            }
        }
    }

    private void Summons(SkillScriptableObject skillData)
    {
        for (int i = 0; i < skillData.SummousCount; i++)
        {
            float range = skillData.Range;
            float newX = Random.Range(-range, range), newY = transform.position.y, newZ = Random.Range(-range,range);

            // ������ ������Ʈ�� �ҷ��´�.
            GameObject monster = Instantiate(skillData.SummonObject);

            // �ҷ��� ������Ʈ�� �����ϰ� ������ ��ǥ������ ��ġ�� �ű��.
            monster.transform.position = new Vector3(newX + transform.position.x, newY, newZ + transform.position.z);
        }

        Stop();
    }

    internal void Stop()
    {
        projector.enabled = false;

        isActiveSkill = false;
    }
}
