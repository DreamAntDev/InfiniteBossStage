using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Stage
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Stage", order = 1)]
    public class Stage : ScriptableObject
    {
        public int Index;
        public string BossPrefab;
        public string WorldPrefab;
    }
}
