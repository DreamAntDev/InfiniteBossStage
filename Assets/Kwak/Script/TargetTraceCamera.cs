using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Component
{
    public class TargetTraceCamera : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        private void LateUpdate()
        {
            var pos = target.position + offset;
            //var correctPos = Vector3.Lerp(transform.position, pos, Time.deltaTime *2);
            var correctPos = pos;
            this.transform.position = correctPos;

            this.transform.LookAt(target);
        }
    }
}