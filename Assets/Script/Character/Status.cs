using IBS.Resoruce;
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

        public int MaxAttack { get; private set; }
        public int CurrentAttack { get; private set; }

        public int MaxMove { get; private set; }
        public int CurrentMove { get; private set; }        

        public Status(Data.Character.CharacterStatus data)
        {
            MaxHP = data.MaxHP;
            MaxStamina = data.MaxStamina;
            MaxAttack = data.MaxAttack;
            MaxMove = data.MaxMove;

            RelicApply();

            Debug.Log($"Player Status\n========\nHP:{MaxHP}/{CurrentHP}\nS:{MaxStamina}/{CurrentStamina}\nA:{MaxAttack}/{CurrentAttack}\nM:{MaxMove}/{CurrentMove}\n==========");

            StaminaRecoveryValuePerSec = data.StaminaRecoveryValuePerSec;
            AvoidUsingStamina = data.AvoidUsingStamina;


            UI.MainInterface.MainInterface.Instance.hpSlider.SetValue(1.0f, forceApply: true);
            UI.MainInterface.MainInterface.Instance.staminaSlider.SetValue(1.0f, forceApply: true);
        }

        private void RelicApply()
        {
            List<Relic> activeRelics = RelicManager.Instance.ActivePlayerRelic();
            int add_Attack = 0;
            int add_HP = 0;
            int add_Energy = 0;
            int add_Move = 0;
            foreach(var relic in activeRelics)
            {
                foreach(var effect in relic.Effects)
                {
                    switch (effect.EffectType)
                    {
                        case Type.RelicEffect.Attack:
                            //데미지 증가
                            add_Attack += effect.EffectValue;
                            break;
                        case Type.RelicEffect.HP:
                            //HP 증가
                            add_HP += effect.EffectValue;
                            break;
                        case Type.RelicEffect.Energy:
                            //스테미너 증가
                            add_Energy += effect.EffectValue;
                            break;
                        case Type.RelicEffect.Move:
                            //이동 속도 증가
                            add_Move += effect.EffectValue;
                            break;
                    }
                }
            }

            CurrentHP = Mathf.RoundToInt(MaxHP * (1 +(float)(add_HP * 0.01)));
            CurrentStamina = Mathf.RoundToInt(MaxStamina * (1 + (float)(add_Energy * 0.01)));
            CurrentAttack = Mathf.RoundToInt(MaxAttack * (1 + (float)(add_Attack * 0.01)));
            CurrentMove = Mathf.RoundToInt(MaxMove * (1 + (float)(add_Move * 0.01)));
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