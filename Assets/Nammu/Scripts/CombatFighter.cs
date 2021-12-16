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

        [SerializeField]
        float attackDistance = 3f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
        }

        private void Start()
        {
            target = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            if (target == null) return;
            if (InAttackRangeOfPlayer())
            {
                mover.Cancel();
                animator.SetTrigger("Attack" + Mathf.RoundToInt(Random.Range(1, 3)));
            }
            else
            {
                mover.MoveTo(target.transform.position, 1f);
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


        public void CanAttack() { }
        public void Attack() { }

        public void Cancel() { }
        public void Hit() { }


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
