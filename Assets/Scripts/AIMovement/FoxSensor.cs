using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UtilityAi;

public class FoxSensor : AISensor
{
   FoxController _foxController;
   public bool bFoundTrace { get; private set; }

   protected override void Start()
   {
      base.Start();
      _foxController = GetComponentInParent<FoxController>();
   }

   public List<GameObject> GetRabbitsInRange()
   {
      return rabbitsInRange;
   }
   
   private bool ScanForRabbitTrace()
   {
      return false;
   }

}
