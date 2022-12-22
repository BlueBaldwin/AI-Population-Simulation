using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UtilityAi
{
   public class AIBrain : MonoBehaviour
   {
      public Action bestAction { get; set; }
      public bool doneDeciding { get; set; }
      public bool isBestActionFinished { get; set; }
      private HUD hud;
   
      [SerializeField] Action[] allActions;
   
      private RabbitController _rabbit;
      private AISensor _sensor; // Private field to store the AISensor component
   
      private void Start()
      {
         _rabbit = GetComponent<RabbitController>();
         _sensor = GetComponentInChildren<AISensor>();
         hud = GetComponentInChildren<HUD>();
         doneDeciding = false;
         isBestActionFinished = false;
      }
   
      public void FindBestAction()
      {
         isBestActionFinished = false;
         // Use LINQ's Aggregate method to find the action with the highest score
         bestAction = allActions.Aggregate((bestActionSoFar, action) =>
            ScoreAction(action, _sensor) > ScoreAction(bestActionSoFar, _sensor) ? action : bestActionSoFar);
         hud.UpdateBestActionText(bestAction);
      }
   
      // Calculates and returns the score of an action based on its considerations and the game objects within the sensor's cone
      public float ScoreAction(Action action, AISensor sensor)
      {
         float score = 1f;
         // Calculate the average score of all considerations for this action
         foreach (var consideration in action.Considerations)
         {
            float considerationScore = consideration.ScoreConsideration(_rabbit, sensor);
            // Multiply the overall score by the consideration score
            score *= considerationScore;
            // If the score becomes zero, return zero immediately
            if (score == 0)
            {
               action.Score = 0;
               return action.Score;
            }
         }
         if (action.Considerations.Length > 0)
         {
            // Average the overall score
            float originalScore = score;
            float modFactor = 1 - (1 / action.Considerations.Length);
            float makeupValue = (1 - originalScore) * modFactor;
            action.Score = originalScore + (makeupValue * originalScore);
         }
         else
         {
            // If the action has no considerations, set the score to 1
            action.Score = 1;
         }
         return action.Score;
      }
   }
}


