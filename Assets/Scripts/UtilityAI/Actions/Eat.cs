using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AiMovement;
using TreeEditor;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
public class Eat : Action
{
    public override void Execute(AnimalController animal)
    {
        animal.Forage(1);
    }

    public override void SetRequiredDestination(AnimalController animal)
    {
        // Manually looping through all food transforms - Later change to using scanner for finding food
        float distance = Mathf.Infinity;
        Transform nearestMeal = null;

        List<Transform> foodPositions = animal.mapContext.Destinations[DestinationType.EAT];
        foreach (var foodTransform in foodPositions)
        {
            float distanceFromFood = Vector3.Distance(foodTransform.position, animal.transform.position);
            if (distanceFromFood < distance)
            {
                nearestMeal = foodTransform;
                distance = distanceFromFood;
            }
        }
        RequiredDestination = nearestMeal;
        if (RequiredDestination != null)
        {
            animal.movement.SetDestination(RequiredDestination.position);
        }
    }
}
