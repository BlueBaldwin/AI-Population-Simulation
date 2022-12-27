using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UtilityAi;

[ExecuteInEditMode]
public class FoxSensor : AISensor
{
   FoxController _foxController;
   public bool bFoundTrace { get; private set; }
   [SerializeField] protected float smellRadii;
   [SerializeField] private LayerMask droppingLayerMask;
   private HashSet<GameObject> droppingsInRadius = new HashSet<GameObject>();
   public List<GameObject> droppingsList;

   protected override void Start()
   {
      base.Start();
      _foxController = GetComponentInParent<FoxController>();
   }

   protected override void Update()
   {
      base.Update();
      DroppingsInRadius();
   }

   protected override void Scan()
   {
      base.Scan();
      foreach (GameObject g in foodInRange)
      {
         previousFoodLocations.Add(g.transform.position);
      }
   }

   public List<GameObject> GetRabbitsInRange()
   {
      return rabbitsInRange;
   }

   private void DroppingsInRadius()
   {
      droppingsInRadius.Clear();
      Collider[] foundDroppings = Physics.OverlapSphere(transform.position, smellRadii, droppingLayerMask, QueryTriggerInteraction.Collide);

      foreach (Collider c in foundDroppings)
      {
         droppingsInRadius.Add(c.gameObject);
      }

      ShowDroppingsInRadius();
   }

   // Debug method to serialise the hashtable
   private void ShowDroppingsInRadius()
   {
      droppingsList.Clear();
      foreach (var dropping in droppingsInRadius)
      {
         droppingsList.Add(dropping);
      }
   }
   
   public List<GameObject> GetDroppingsList()
   {
      return droppingsList;
   }

   private bool ScanForRabbitTrace()
   {
      return false;
   }

   protected override void OnDrawGizmos()
   {
      base.OnDrawGizmos();
      Gizmos.color = Color.yellow ;
      Gizmos.DrawWireSphere(transform.position, smellRadii);
   }
}
