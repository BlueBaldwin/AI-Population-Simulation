using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class FoxController : MonoBehaviour
    {
        private FoxBehaviorTree _behaviorTree;

        private AISensor _aiSensor;

        private void Awake()
        {
            _aiSensor = GetComponentInChildren<AISensor>();
        }

        void Start()
        {
            _behaviorTree = new FoxBehaviorTree();
        }
    
        void Update()
        {
            _behaviorTree.Update();
        }

        public GameObject GetRabbitsInRange()
        {
            return new GameObject();
        }
    }
}

