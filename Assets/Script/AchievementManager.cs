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
        // todo
        // id 0 : stage, 1 : relics
        public static List<string> get() {
            // id:now:max
            var sample = new List<string>() {"0:1:3","1:2:5"};
            return sample;
        }
        public static List<string> set(string id="999", string now="1", string max="100") {
            var sample = new List<string>() {"0:1:3","1:2:5"};
            return sample;
        }
    }
}