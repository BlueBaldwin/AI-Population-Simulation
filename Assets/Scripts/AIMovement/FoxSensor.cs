using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class FoxSensor : AISensor
{
	private FoxController _foxController;
	
	private void Awake()
	{
		_foxController = GetComponentInParent<FoxController>();
	}
	
	protected override void Scan()
	{
		_count = Physics.OverlapSphereNonAlloc(transform.position, distance, _colliders, detectionLayer, QueryTriggerInteraction.Collide);
		objectsWithinSensor.Clear();
		for (int i = 0; i < _count; i++)
		{
			GameObject g = _colliders[i].gameObject;
            
			if (g == null)
			{
				Debug.LogError("GameObject is null!");
				break;
			}
            
			if (base.IsInSight(g))
			{
				objectsWithinSensor.Add(g);
				switch (g.tag)
				{ 
					// If the game object is a food object, move to it
					case "Food":
						// _foxController.SetFoodObject(g);
						previousFoodLocations.Add(g.transform.position);
						break;
                    
					case "Prey":
						bIsInSight = true;
						break;
				}
			}
		}
	}
}
