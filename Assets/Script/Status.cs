using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Status
    {
        public int MaxHP { get; private set; }
        public int CurrentHP { get; private set; }
        
        public void OnDamage(int damage)
        {
            this.CurrentHP -= damage;
            this.CurrentHP = Mathf.Max(0, this.CurrentHP);
        }
    }
}