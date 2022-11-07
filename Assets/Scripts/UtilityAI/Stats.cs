using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Animal Stats", menuName = "UtilityAI/AnimalStats")]
[Serializable]
public class Stats  {
   // public float health;
   // public float runSpeed;
   // public float walkSpeed;
   // public float visionLength;
   public int energy;
   public int hunger;

   public Stats CreateCopy()
   {
	   Stats copy = (Stats)MemberwiseClone();
	   return copy;
   }
}
