﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class SteeringBehaviours : MonoBehaviour
    {
        public float weight = 8f;
        public Vector3 force;
        public AIAgent owner;

       protected virtual void Awake()
        {
            owner = GetComponent<AIAgent>();
        }

        public virtual Vector3 GetForce()
        {
            return Vector3.zero;  
        }



    }
}
