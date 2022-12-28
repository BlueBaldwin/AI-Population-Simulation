using UnityEngine;
using UtilityAi;

public class SearchForPrey : SelectorNode
{
    private FoxController _foxController;
    private FoxSensor _sensor;
    private AIMovement _aiMovement;

    public SearchForPrey(FoxController foxController, FoxSensor sensor, AIMovement aiMovement) : base(foxController, sensor)
    {
       
    }
}


