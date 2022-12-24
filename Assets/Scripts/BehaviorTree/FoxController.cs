using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class FoxController : MonoBehaviour
    {
        private FoxBehaviorTree _behaviorTree;

        private FoxSensor _foxSensor;
        

        private void Awake()
        {
            _foxSensor = GetComponentInChildren<FoxSensor>();
        }

        void Start()
        {
            // _behaviorTree = new FoxBehaviorTree();
          //  _behaviorTree._foxSensor = GetComponent<FoxSensor>();
        }
    
        void Update()
        {
            _behaviorTree.Update();
        }

        // Getting the component through this method instead of public fields
        public T GetFoxComponent<T>(string componentName) where T : Component
        {
            T component = GetComponent<T>();

            if (component == null)
            {
                Debug.LogError($"Unable to find component of type {typeof(T)} with name {componentName}");
                return null;
            }
            return component;
        }
    }
}

