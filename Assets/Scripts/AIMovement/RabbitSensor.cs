using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAi;

public class RabbitSensor : AISensor
{
	RabbitController _rabbitController;
	
	private void Awake()
	{
		_rabbitController = GetComponentInParent<RabbitController>();
	}

	protected override void Update()
	{
		base.Update();
		Debug.Log("Base has been called");
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
						_rabbitController.SetFoodObject(g);
						previousFoodLocations.Add(g.transform.position);
						 Debug.Log("Food Found");
						break;
                    
					case "Fox": 
                        Debug.Log("Fox!!!!!");
						break;
				}
			}
		}
	}
}
