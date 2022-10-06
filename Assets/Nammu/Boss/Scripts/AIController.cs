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
            List<Relic> activeRelics = RelicManager.Instance.ActiveBossRelic();

            int damageValue = 0;
            int healthValue = 0;
            int energyValue = 0;
            int moveValue = 0;

            foreach (var relic in activeRelics)
            {
                if (relic.ApplyType == Type.RelicApplyType.Boss)
                {
                    Debug.Log(relic.Context);
                    foreach (var effect in relic.Effects)
                    {
                        switch (effect.EffectType)
                        {
                            case Type.RelicEffect.Attack:
                                //������ ����
                                damageValue += effect.EffectValue;
                                break;
                            case Type.RelicEffect.HP:
                                //HP ����
                                healthValue += effect.EffectValue;
                                break;
                            case Type.RelicEffect.Energy:
                                //���׹̳� ����
                                energyValue += effect.EffectValue;
                                break;
                            case Type.RelicEffect.Move:
                                //�̵� �ӵ� ����
                                moveValue += effect.EffectValue;
                                break;
                        }
                    }
                }
            }

            combatFighter.DamageReduction = damageValue;
            GetComponent<Health>().HealthValue = healthValue;
            GetComponent<Mover>().Speed = moveValue;
        }
    }
}
