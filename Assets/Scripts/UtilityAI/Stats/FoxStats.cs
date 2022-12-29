using UnityEngine;

public class FoxStats : Stats
{

	//private float _attackRange;

		public FoxStats(int energy, int hunger, float attackRange) : base(energy, hunger)
		{
			AttackRange = attackRange;
		}
		
		public float AttackRange { get; }
		
		
	}