using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
   public Action bestAction { get; set; }
   private Controller controller;

   private void Start()
   {
      controller = GetComponent<Controller>();
   }

   //Loop through all available actions and return the highest scoring action
   public void BestAction(Action[] allActions)
   {
      
   }
   
  // Loop through and score all considerations and return an average consideration score that will become the overall action score
   public void ScoreAction(Action action)
   {
      float score = 1f;
      // Returning the score from the abstract score consideration method
      // How much that factor influences the importances of the action it's associated with
      for (int i = 0; i < action.Considerations.Length; i++)
      {
         float considerationScore = action.Considerations[i].ScoreConsideration();
      }
   }
}
