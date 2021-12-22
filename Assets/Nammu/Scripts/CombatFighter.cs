using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IBS.Combat
{
    public class CombatFighter : MonoBehaviour
    {
        GameObject target;
        Animator animator;
        Mover mover;
        Health health;

        [SerializeField]
        float attackDistance = 2f;

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
            if (target == null || health.IsDead()) return;

            
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

        public void Hit(float damage) 
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
            animator.SetTrigger("Attack" + Mathf.RoundToInt(Random.Range(1, 3)));
        }
    }
}
