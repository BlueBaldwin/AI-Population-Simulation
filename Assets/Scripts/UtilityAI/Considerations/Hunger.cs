using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hunger", menuName = "UtilityAI/Considerations/Hunger")]
public class Hunger : Consideration
{
   public override float ScoreConsideration()
   {
      return 0.1f;
   }
}
