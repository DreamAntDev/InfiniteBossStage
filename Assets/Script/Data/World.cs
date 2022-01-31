using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.World
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/World", order = 1)]
    public class World : ScriptableObject
    {
        public string PrefabPath;
    }
}
