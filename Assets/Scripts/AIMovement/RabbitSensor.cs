using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAi;

public class RabbitSensor : AISensor
{
   RabbitController _rabbitController;

   protected override void Start()
   {
      base.Start();
      _rabbitController = GetComponentInParent<RabbitController>();
   }

   protected override void Scan()
   {
      base.Scan();
      foreach (GameObject g in foodInRange)
      {
         _rabbitController.SetFoodObject(g);
         previousFoodLocations.Add(g.transform.position);
      }
   }
}
