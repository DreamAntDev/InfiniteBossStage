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

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }
    }
}
