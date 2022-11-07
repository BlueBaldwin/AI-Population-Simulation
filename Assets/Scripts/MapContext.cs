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
	
	public GameObject home;
    public GameObject water;
    public float minDistance;
    public Dictionary<DestinationType, List<Transform>> Destinations { get; private set; }
    
     private void Start()
     {
	     Renderer r = ground.GetComponent<Renderer>();
	     bounds = r.bounds;

	     //SpawnFood();
	     
         // List<Transform> sleepDestinations = new List<Transform>() { home.transform };
         // List<Transform> drinkDestinations = new List<Transform>() { water.transform };
         List<Transform> eatDestinations = SpawnRabbitFood();
    
         Destinations = new Dictionary<DestinationType, List<Transform>>()
         {
             // { DestinationType.EAT, sleepDestinations },
             // { DestinationType.DRINK, drinkDestinations },
             { DestinationType.EAT, eatDestinations }
         };
    }

     private List<Transform> SpawnRabbitFood()
     {
	     List<Transform> rabbitFoodTransforms = new List<Transform>();
	     for (int i = 0; i < foodQty; i++)
	     {
		     float x = Random.Range(bounds.min.x, bounds.max.x);
		     float z = Random.Range(bounds.min.z, bounds.max.z); 
		     GameObject g = Instantiate(rabbitFood, new Vector3(x, 0, z), Quaternion.Euler(0, Random.Range(0, 360), 0));
		     g.name = "Tree " + i;
		     rabbitFoodTransforms.Add(g.transform);
     
		     // hits = Physics.SphereCastAll(newTreePos, sphereRadius, Vector3.up);
		     // foreach (var raycastHit in hits)
		     // {
			    //  Destroy(GameObject.Find("Tree " + i));
			    //  treePlantingHitObjects.Add(raycastHit.transform.gameObject);
       //
		     // }
	     }
	     return rabbitFoodTransforms;
     }
}


