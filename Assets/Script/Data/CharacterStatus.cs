using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Character
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterStatus", order = 1)]
    public class CharacterStatus : ScriptableObject
    {
        public int MaxHP;
        public int MaxStamina;
        public int StaminaRecoveryValuePerSec;
        public int AvoidUsingStamina;
        public int MaxAttack;
        public int MaxMove;
    }
}