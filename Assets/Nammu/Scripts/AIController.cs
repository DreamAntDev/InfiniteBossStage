using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IBS.Monster
{
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
