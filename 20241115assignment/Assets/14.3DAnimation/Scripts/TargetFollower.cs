using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class TargetFollower : MonoBehaviour
    {
        public Transform target;

        public bool followPosition;
        public bool followRotation;

        private void Update()
        {
            if(target == null) return;
            if(followPosition) transform.position = target.position; 
            if(followPosition) transform.rotation = target.rotation;

        }
    }
}
