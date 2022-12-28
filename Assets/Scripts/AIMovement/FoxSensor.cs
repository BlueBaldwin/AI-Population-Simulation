using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAi;

[ExecuteInEditMode]
public class FoxSensor : AISensor
{
   FoxController _foxController;
   public bool bFoundTrace { get; private set; }
   private HashSet<GameObject> droppingsInRadius = new HashSet<GameObject>();
   public List<GameObject> droppingsList;
   [SerializeField] private float smellRadius;
   [SerializeField] private LayerMask droppingLayerMask;
   public Vector3 ScentLocation { get; set; }

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
      Collider[] foundDroppings = Physics.OverlapSphere(transform.position, smellRadius, droppingLayerMask, QueryTriggerInteraction.Collide);

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
   
   public bool IsScentDetected()
   {
      if (droppingsInRadius.Count > 0)
      {
         GameObject closestDropping = GetClosestDropping();
         if (closestDropping != null)
         {
            ScentLocation = closestDropping.transform.position;
            return true;
         }
      }
      return false;
   }
   
   public GameObject GetClosestDropping()
   {
      float closestDistance = float.MaxValue;
      GameObject closestDropping = null;
      foreach (GameObject dropping in droppingsInRadius)
      {
         float distance = Vector3.Distance(transform.position, dropping.transform.position);
         if (distance < closestDistance)
         {
            closestDistance = distance;
            closestDropping = dropping;
         }
      }
      return closestDropping;
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
      Gizmos.DrawWireSphere(transform.position, smellRadius);
   }
}
