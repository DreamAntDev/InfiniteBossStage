using IBS.Resoruce;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IBS.Monster
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Combat.CombatFighter))]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(DropRelic))]
    [RequireComponent(typeof(ActionScheduler))]

    public abstract class AIController : MonoBehaviour
    {
        float distance = 0;

        Animator animator;

        GameObject player;

        Combat.CombatFighter combatFighter;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            combatFighter = GetComponent<Combat.CombatFighter>();
            RelicApply();
        }
        private void RelicApply()
        {
            List<Relic> activeRelics = RelicManager.Instance.ActivePlayerRelic();
            foreach (var relic in activeRelics)
            {
                if (relic.ApplyType == Type.RelicApplyType.Boss)
                {
                    foreach (var effect in relic.Effects)
                    {
                        switch (effect.EffectType)
                        {
                            case Type.RelicEffect.Attack:
                                //������ ����
                                combatFighter.DamageReduction = effect.EffectValue;
                                break;
                            case Type.RelicEffect.HP:
                                //HP ����
                                GetComponent<Health>().HealthValue = effect.EffectValue;
                                break;
                            case Type.RelicEffect.Energy:
                                //���׹̳� ����
                                break;
                            case Type.RelicEffect.Move:
                                //�̵� �ӵ� ����
                                Debug.Log(effect.EffectValue);
                                GetComponent<Mover>().Speed = effect.EffectValue;
                                break;
                        }
                    }
                    //Value �ջ��ؼ� �Ѳ����� ���� �ʿ�.

                }
            }
        }
    }
}
