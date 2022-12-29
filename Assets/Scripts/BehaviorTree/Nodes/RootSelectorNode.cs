using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAi;

public class RootSelectorNode : SelectorNode
{
    private AIMovement _aiMovement;

    public RootSelectorNode(FoxController foxController, FoxSensor sensor, AIMovement aiMovement) : base(foxController, sensor)
    {
        _aiMovement = aiMovement;

        // Create the sequence for going to sleep
        SequenceNode goToSleepSequence = new SequenceNode();
        goToSleepSequence.AddChild(new IsTired(foxController));
        goToSleepSequence.AddChild(new GoToSleep(foxController, sensor));

        // Create the sequence for finding, stalking and attacking prey
        SequenceNode findStalkAndAttackSequence = new SequenceNode();
        findStalkAndAttackSequence.AddChild(new FollowScent(foxController, sensor));
        findStalkAndAttackSequence.AddChild(new StalkRabbit(foxController, sensor, _aiMovement));
        findStalkAndAttackSequence.AddChild(new AttackRabbit(foxController, sensor));

        // Add the go to sleep and find, stalk and attack sequences as children of the root node
        AddChild(goToSleepSequence);
        AddChild(findStalkAndAttackSequence);

        // Set the default action to wander
        AddChild(new Wander(foxController, sensor, _aiMovement));
    }
}


