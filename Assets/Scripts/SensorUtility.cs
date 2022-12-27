using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SensorUtility 
{
    public static List<GameObject> GetObjectsInRange(AISensor sensor, string tag)
    {
        // Create a list to store the objects of the specified type
        List<GameObject> objectsInRange = new List<GameObject>();
        // Iterate through all objects within the sensor's range
        foreach (GameObject obj in sensor.objectsWithinSensor)
        {
            // Check if the object has the specified tag
            if (obj.CompareTag(tag))
            {
                // Add the object to the list if it has the specified tag
                objectsInRange.Add(obj);
            }
        }
        // Return the list of objects with the specified tag within the sensor's range
        return objectsInRange;

    }

    public static GameObject FindClosestTaggedGoInList(AISensor sensor, List<GameObject> list, string tag)
    {
        // List<GameObject> objects = GetObjectsInRange(sensor, tag);
        // Create a variable to store the closest game object
        GameObject closestGameObject = null;
        // Create a variable to store the closest distance

        float closestDistance = float.MaxValue;
        // Iterate through all game objects in the list
        foreach (GameObject gameObject in list)
        {
            // Check if the game object has the specified tag
            if (gameObject.CompareTag(tag))
            {
                // Calculate the distance between the caller and the game object
                float distance = Vector3.Distance(sensor.transform.position, gameObject.transform.position);

                // Check if this game object is closer than the current closest game object
                if (distance < closestDistance)
                {
                    // Update the closest game object and its distance
                    closestGameObject = gameObject;
                    closestDistance = distance;
                }
            }
        }

        // Return the closest game object with the specified tag
        return closestGameObject;
    }



    public static T GetComponent<T>(AISensor sensor) where T : Component
    {
        return sensor.gameObject.GetComponentInParent<T>();
    }

}
