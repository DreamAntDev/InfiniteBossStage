using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Status
    {
        public int MaxHP { get; private set; }
        public int CurrentHP { get; private set; }
        
        public int MaxStamina { get; private set; }
        public float CurrentStamina { get; private set; }

        private int StaminaRecoveryValuePerSec;
        private int AvoidUsingStamina;

        public Status(Data.Character.CharacterStatus data)
        {
            MaxHP = data.MaxHP;
            MaxStamina = data.MaxStamina;

            CurrentHP = MaxHP;
            CurrentStamina = (float)MaxStamina;

            StaminaRecoveryValuePerSec = data.StaminaRecoveryValuePerSec;
            AvoidUsingStamina = data.AvoidUsingStamina;

            UI.MainInterface.MainInterface.Instance.hpSlider.SetValue(1.0f, forceApply: true);
            UI.MainInterface.MainInterface.Instance.staminaSlider.SetValue(1.0f, forceApply: true);
        }

        public void RecoveryStamina()
        {
            if (CurrentStamina < MaxStamina)
            {
                var recoveryValue = StaminaRecoveryValuePerSec * Time.deltaTime;
                var tempValue = this.CurrentStamina + recoveryValue;

                CurrentStamina = Mathf.Min(tempValue, MaxStamina);
                UI.MainInterface.MainInterface.Instance.staminaSlider.SetValue((float)this.CurrentStamina / (float)this.MaxStamina);
            }
        }

        public void OnDamage(int damage)
        {
            this.CurrentHP -= damage;
            this.CurrentHP = Mathf.Max(0, this.CurrentHP);

            UI.MainInterface.MainInterface.Instance.hpSlider.SetValue((float)this.CurrentHP/(float)this.MaxHP);
        }

        public bool OnAvoid()
        {
            if (this.CurrentStamina < this.AvoidUsingStamina)
                return false;

            this.CurrentStamina -= this.AvoidUsingStamina;
            UI.MainInterface.MainInterface.Instance.staminaSlider.SetValue((float)this.CurrentStamina / (float)this.MaxStamina);
            return true;
        }
    }
}