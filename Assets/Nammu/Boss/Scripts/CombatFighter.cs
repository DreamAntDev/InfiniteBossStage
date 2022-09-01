using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IBS.Combat
{
    public class CombatFighter : MonoBehaviour, IAction
    {
        GameObject target;
        Animator animator;
        Mover mover;
        Health health;

        [SerializeField]
        float attackDistance = 2f;
        [SerializeField]
        int attackCount = 3;

        string attackName = string.Empty;

        float damageReduction = 1;

        public float DamageReduction
        {
            set => damageReduction = 1-value;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            target = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            if (target == null || health.IsDead())
                return;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
            {
                if (InAttackRangeOfPlayer())
                {
                    mover.Cancel();
                    AttackBehaviour();
                }
                else
                {
                    mover.MoveTo(target.transform.position, 0.5f);
                }
            }
            else
            {
                mover.Cancel();
            }
        }


        private float PlayerToDistance()
        {
          
            return Vector3.Distance(target.transform.position, transform.position);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = PlayerToDistance();
            return distanceToPlayer < attackDistance;
        }
        private bool InAttackRangeOfPlayer(float value)
        {
            float distanceToPlayer = PlayerToDistance();
            return distanceToPlayer < attackDistance * value;
        }

        public void Hit(float damage) 
        {
            if (target != null)
            {
                if (InAttackRangeOfPlayer(1.05f))
                {
                    target.GetComponent<PlayerController>().OnDamage(Convert.ToInt32(damage * damageReduction) );
                }
            }
        }

        public void TakeDamage(float damage)
        {
            health.TakeDamage(damage);
        }


        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            TriggerAttack();
        }

        private void TriggerAttack()
        {
            GetComponent<ActionScheduler>().StartAction(this);
            attackName = "Attack" + Mathf.RoundToInt(UnityEngine.Random.Range(1, attackCount +1));
            animator.SetTrigger(attackName);
        }

        public void Cancel()
        {
            StopAttacking();
          //  target = null;
        }

        private void StopAttacking()
        {
            animator.ResetTrigger(attackName);
            animator.SetTrigger("StopAttack");
        }
    }
}
