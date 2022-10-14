using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public AIMovement Movement;
    public AIBrain aiBrain { get; set; }
    public Action[] actions;
    
    void Start()
    {
        Movement = GetComponent<AIMovement>();
        aiBrain = GetComponent<AIBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
