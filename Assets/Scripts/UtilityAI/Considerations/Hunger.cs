using System.Collections;
using System.Collections.Generic;
using UtilityAi;
using UnityEngine;

namespace UtilityAi
{
   [CreateAssetMenu(fileName = "Hunger", menuName = "UtilityAI/Considerations/Hunger")]
   public class Hunger : Consideration
   {
      [SerializeField] private AnimationCurve responseCurve;
   
      public override float ScoreConsideration(RabbitController rabbit, AISensor sensor)
      {
         // Look up the hunger stat divide by 100 and pass it to to the animation curve to see what the value is
         // Calculating our score using animation curve
         float score = responseCurve.Evaluate(Mathf.Clamp01(rabbit.stats.Hunger / 100f));
         return score;
      }
   }
}

