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
                                //데미지 감소
                                combatFighter.DamageReduction = effect.EffectValue;
                                break;
                            case Type.RelicEffect.HP:
                                //HP 감소
                                GetComponent<Health>().HealthValue = effect.EffectValue;
                                break;
                            case Type.RelicEffect.Energy:
                                //스테미너 감소
                                break;
                            case Type.RelicEffect.Move:
                                //이동 속도 감소
                                Debug.Log(effect.EffectValue);
                                GetComponent<Mover>().Speed = effect.EffectValue;
                                break;
                        }
                    }
                    //Value 합산해서 한꺼번에 적용 필요.

                }
            }
        }
    }
}
