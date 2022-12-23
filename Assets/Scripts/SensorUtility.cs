using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SensorUtility 
{
    public static List<GameObject> GetObjectsInRange(AISensor sensor, List<GameObject> objectsWithinSensor, string tag)
    {
        // Create a list to store the objects of the specified type
        List<GameObject> objectsInRange = new List<GameObject>();

        // Create a HashSet to store the objects that have already been added to the list
        HashSet<GameObject> addedObjects = new HashSet<GameObject>();

        // Iterate through all objects within the sensor's range
        foreach (GameObject obj in objectsWithinSensor)
        {
            // Check if the object has the specified tag
            if (obj.CompareTag(tag))
            {
                // If the object has the specified tag and it has not already been added to the list, add it to the list
                if (!addedObjects.Contains(obj))
                {
                    objectsInRange.Add(obj);
                    addedObjects.Add(obj);
                }
            }
        }

        // Return the list of objects of the specified type within the sensor's range
        return objectsInRange;
    }
    
    public static T GetComponent<T>(AISensor sensor) where T : Component
    {
        return sensor.gameObject.GetComponentInParent<T>();
    }

}
