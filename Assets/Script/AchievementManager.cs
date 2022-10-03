using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AI;

using System.Linq;
namespace Static
{
    public class AchievementManager : MonoBehaviour
    {
        public static int get(string id)
        {
            return PlayerPrefs.GetInt(id, 0);
        }

        public static int set(string id, int value)
        {
            PlayerPrefs.SetInt(id, 0);
            return PlayerPrefs.GetInt(id, 0);
        }

        public static int incr(string id, int value=1)
        {
           PlayerPrefs.SetInt(id, PlayerPrefs.GetInt(id, 0) + value);
           return PlayerPrefs.GetInt(id, 0);
        }

        public static int setMax(string id, int now=1)
        {
           var before = PlayerPrefs.GetInt(id, 0);
           if (before < now) {
            PlayerPrefs.SetInt(id, now);
           }
           return PlayerPrefs.GetInt(id, 0);
        }

        public static void testDel()
        {
           PlayerPrefs.SetInt(AchievementDefine.stageClearMax, 0);
           PlayerPrefs.SetInt(AchievementDefine.relicCount, 0);
        }
    }
}