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
            if (CharacterAnimation(splitArg) == true)
                return;

            Debug.LogError("Not Exist Command");
#endif
        }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        static bool CharacterAnimation(List<string> args)
        {
            if (args[0].Equals("Character") == false)
                return false;
            if (args[1].Equals("Animation") == false)
                return false;

            if (args[2].Equals("Damage") == false)
            {
                UnityEngine.Debug.LogError("Not Define Command");
                return false;
            }

            int idx = 0;
            if(int.TryParse(args[3],out idx) == false)
            {
                UnityEngine.Debug.LogError("Use Valid Args ex)Character Animation [string]Damage [int]0");
                return false;
            }

            PlayerController.instance.OnDamage();
            return true;
        }




#endif
    }
}