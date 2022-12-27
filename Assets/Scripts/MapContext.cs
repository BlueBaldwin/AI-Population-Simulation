using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapContext : MonoBehaviour
{
	public Bounds bounds;
	[SerializeField] int foodQty;
	[SerializeField] GameObject rabbitFood;
	[SerializeField] GameObject ground;
	[SerializeField] float spawnRadius;
	[SerializeField] float spawnBoarder;
	
	// public GameObject home;
 //    public GameObject water;
 //    public float minDistance;
    // public Dictionary<DestinationType, List<Transform>> Destinations { get; private set; }
    
     private void Start()
     {
	     Renderer r = ground.GetComponent<Renderer>();
	     bounds = r.bounds;
      //
	     // //SpawnFood();
	     //
      //    // List<Transform> sleepDestinations = new List<Transform>() { home.transform };
      //    // List<Transform> drinkDestinations = new List<Transform>() { water.transform };
      List<Transform> eatDestinations = SpawnRabbitFood();
      //
      //    Destinations = new Dictionary<DestinationType, List<Transform>>()
      //    {
      //        // { DestinationType.EAT, sleepDestinations },
      //        // { DestinationType.DRINK, drinkDestinations },
      //        { DestinationType.EAT, eatDestinations }
      //    };
    }
 
     private List<Transform> SpawnRabbitFood()
     {
	     float attempts = 0;
	     List<Transform> rabbitFoodTransforms = new List<Transform>();
	     for (int i = 0; i < foodQty; i++)
	     {
		     float x = Random.Range(bounds.min.x + spawnBoarder, bounds.max.x - spawnBoarder);
		     float z = Random.Range(bounds.min.z + spawnBoarder, bounds.max.z - spawnBoarder);
		     Vector3 randomPos = new Vector3(x, 0.5f, z);

		     // Check if there is already a rabbit food object within the radius
		     bool validSpawnPos = true;
		     Collider[] colliders = Physics.OverlapSphere(randomPos, spawnRadius);
		     foreach (Collider collider in colliders)
		     {
			     if (collider.gameObject.CompareTag("Food"))
			     {
				     validSpawnPos = false;
				     attempts++;
				     break;
			     }

			     if (attempts >= 100)
			     {
				     Debug.LogError("Can't spawn that radius");
			     }
		     }

		     // Spawn if valid or reduce the count
		     if (validSpawnPos)
		     {
			     GameObject g = Instantiate(rabbitFood, randomPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
			     g.name = "RabbitFood " + i;
			     rabbitFoodTransforms.Add(g.transform);
		     }
		     else
		     {
			     i--;
		     }
	     }
	     return rabbitFoodTransforms;
     }
}


