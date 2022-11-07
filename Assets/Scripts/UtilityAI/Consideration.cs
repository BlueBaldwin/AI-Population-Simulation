using System;
using System.Collections;
using System.Collections.Generic;
using AiMovement;
using UnityEngine;

public abstract class Consideration : ScriptableObject // Weightings for each action
{
   private float _score;

   public float Score
   {
      get { return _score; }
      set { _score = Mathf.Clamp01(value); } // Normalise the score bewteen 0-1
   }

   public virtual void Awake()
   {
      _score = 0;
   }

   public abstract float ScoreConsideration(AnimalController animal);
}
