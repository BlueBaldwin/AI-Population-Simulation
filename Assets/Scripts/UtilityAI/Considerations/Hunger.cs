using System.Collections;
using System.Collections.Generic;
using AiMovement;
using UnityEngine;

[CreateAssetMenu(fileName = "Hunger", menuName = "UtilityAI/Considerations/Hunger")]
public class Hunger : Consideration
{
   [SerializeField] private AnimationCurve responseCurve;
   //private Stats stats;
   public override float ScoreConsideration(AnimalController animal)
   {
      // Look up the hunger stat divide by 100 and pass it to to the animation curve to see what the value is
      // Calculating our score using animation curve
      float score = responseCurve.Evaluate(Mathf.Clamp01(animal.stats.hunger / 100f));
      return score;
   }
}
