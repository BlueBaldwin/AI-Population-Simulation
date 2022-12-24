using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class FoxBehaviorTree : BehaviorTree 
    {
       private bool _isFollowingScent = false;
       private Vector3 _scentLocation = Vector3.zero;
        
        public FoxBehaviorTree(FoxController _foxController)
        {
            // Create root node
            SelectorNode root = new SelectorNode();
            
            // Create leaf nodes for actions and conditions
            ActionNode searchForPrey = new ActionNode(SearchForPrey);
            ActionNode attackPrey = new ActionNode(AttackPrey);

            ConditionNode isPreyInSight = new ConditionNode(IsPreyInSight);
            ConditionNode isPreyCloseEnough = new ConditionNode(IsPreyCloseEnough);
            
            // Create sequence for chasing prey
            SequenceNode chasePrey = new SequenceNode();
            chasePrey.AddChild(isPreyInSight);
            chasePrey.AddChild(isPreyCloseEnough);
            chasePrey.AddChild(attackPrey);
            
            // Add chase sequence and search action to root selector
            root.AddChild(chasePrey);
            root.AddChild(searchForPrey);
            
            // Set root node as the tree's root node
            SetRootNode(root);
        }

        // Action methods
        private BehaviorTreeStatus SearchForPrey()
        {
            // searching for rabbit tracks
            // following the rabbit's smell or noise
            
            return BehaviorTreeStatus.TEST;
        }
        
        private BehaviorTreeStatus AttackPrey()
        {
            // Code for attacking prey goes here
            // Return SUCCESS if prey is successfully attacked, FAILURE if not
            return BehaviorTreeStatus.TEST;
        }
        
        // Condition methods that must be met in order for the node to be executed
        private bool IsPreyInSight()
        {
            // Check if any rabbits are within the sensor's field of view
             // foreach (GameObject rabbit in _foxSensor.GetRabbitsInRange())
             // {
             //     return true;
             // }
             return false;
        }
        
        private bool IsPreyCloseEnough()
        {
            // If yes attack
            
            // If no stalk until close another
            return false;
        }
    }
}
