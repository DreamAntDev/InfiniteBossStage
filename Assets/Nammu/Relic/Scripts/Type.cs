using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IBS.Resoruce
{
    public class Type
    {
        public enum RelicRating
        {
            Normal,
            Epic,
            Unique
        }

        public enum RelicApplyType
        {
            Boss,
            Player
        }

        public enum RelicEffect
        {
            HP,
            Energy,
            Move,
            Attack
        }
    }
}
