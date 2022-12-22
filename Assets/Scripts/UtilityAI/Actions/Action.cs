using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAi
{
    public abstract class Action : ScriptableObject
    { 
        // Used to return the current best action for Debugging purposes
        public Consideration[] Considerations;
        private float _score;
        
        public Transform RequiredDestination { get; protected set; }
    
        public float Score
        {
            get { return _score; }
            set { _score = Mathf.Clamp01(value); } // Normalise the score bewteen 0-1
        }
     
        public void Awake()
        {
            Score = 0;
            
        }
    
        // Dependancy injection - we don't have a referenced Controller in this class, but thats ok because whoever calls this will just need to pass it through
        public abstract void Execute(RabbitController rabbit);
        public abstract void SetRequiredDestination(RabbitController rabbit);
    }
}

