using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AiMovement;

public class AIBrain : MonoBehaviour
{
   public Action bestAction { get; set; }
   public bool doneDeciding { get; set; }
   
   [SerializeField] Action[] allActions;
   
   private AnimalController _animalController;

   
   private void Start()
   {
      _animalController = GetComponent<AnimalController>();
   }

   private void Update()
   {
      // if (bestAction == null)
      // {
      //    FindBestAction(_animalController.actions);
      // }
   }

   //Loop through all available actions and return the highest scoring action
   public void FindBestAction()
   {
      float score = 0f;
      int index = 0;
      // Compare scores of all actions againts the current best action
      for (int i = 0; i < allActions.Length; i++)
      {
         // If the pos in all actions array is greater than score then store it's index and assign to our temp var
         if (ScoreAction(allActions[i]) > score)
         {
            index = i;
            score = allActions[i].Score;
         }
      }
      // assigning the best action 
      bestAction = allActions[index];
      doneDeciding = true;
   }
   
  // Loop through and score all considerations and return an average consideration score that will become the overall action score
   public float ScoreAction(Action action)
   {
      float score = 1f;
      // Returning the score from the abstract score consideration method
      // How much that factor influences the importances of the action it's associated with
      for (int i = 0; i < action.Considerations.Length; i++)
      {
         float considerationScore = action.Considerations[i].ScoreConsideration(_animalController);
         // Add to overall score
         score *= considerationScore;
         if (score == 0) // Protect againts multiply by 0
         {
            action.Score = 0;
            return action.Score;
         }
      }
      
      // Averaging of overall score
      float originalScore = score;
      float modFactor = 1 - (1 / action.Considerations.Length);
      float makeupValue = (1 - originalScore) * modFactor;
      action.Score = originalScore + (makeupValue * originalScore);

      return action.Score;
   }

   public void DecideBestAction()
   {
      
   }
}
