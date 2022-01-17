using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugCommand
{
    public static class CommandEditor
    {
        public static void Excute(string args)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (Application.isPlaying == false)
                return;

            List<string> splitArg = new List<string>(args.Split(' '));
            if (CharacterDamage(splitArg) == true)
                return;

            Debug.LogError("Not Exist Command");
#endif
        }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        static bool CharacterDamage(List<string> args)
        {
            if (args[0].Equals("Character") == false)
                return false;

            if (args[1].Equals("Damage") == false)
            {
                UnityEngine.Debug.LogError("Not Define Command");
                return false;
            }

            int damage = 0;
            if(int.TryParse(args[2],out damage) == false)
            {
                UnityEngine.Debug.LogError("Use Valid Args ex)Character Animation [string]Damage [int]0");
                return false;
            }

            PlayerController.instance.OnDamage(damage);
            return true;
        }




#endif
    }
}