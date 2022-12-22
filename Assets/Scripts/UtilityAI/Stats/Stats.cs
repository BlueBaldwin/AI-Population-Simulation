using System;
using UnityEngine;

[Serializable]
public class Stats
{
   
   private int _energy;
   private int _hunger;

   public Stats(int energy, int hunger)
   {
      Energy = energy;
      Hunger = hunger;
   }
   public int Energy
   {
      get { return _energy; }
      set { _energy = Mathf.Clamp(value, 0, 100); }
   }

   public int Hunger
   {
      get { return _hunger; }
      set { _hunger = Mathf.Clamp(value, 0, 100); }
   }
   
   public void IncreaseEnergy(int amount)
   {
      _energy += amount;
      if (_energy > 100)
      {
         _energy = 100;
      }
   }

   public void DecreaseEnergy(int amount)
   {
      _energy -= amount;
      if (_energy < 0)
      {
         _energy = 0;
      }
   }

   public void IncreaseHunger(int amount)
   {
      _hunger += amount;
      if (_hunger > 100)
      {
         _hunger = 100;
      }
   }

   public void DecreaseHunger(int amount)
   {
      _hunger -= amount;
      if (_hunger < 0)
      {
         _hunger = 0;
      }
   }

}

