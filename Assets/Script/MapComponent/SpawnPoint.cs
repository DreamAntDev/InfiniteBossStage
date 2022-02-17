using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapComponent
{
    public class SpawnPoint : MonoBehaviour
    {
        public enum SpawnType
        {
            Character,
            Boss,
        }
        [SerializeField] private SpawnType spawnType;
        public SpawnType eSpawnType { get { return this.spawnType; } }
    }
}