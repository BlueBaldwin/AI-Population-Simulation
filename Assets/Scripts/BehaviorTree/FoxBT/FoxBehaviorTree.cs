using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorTree
{
    public class FoxBehaviorTree : BehaviorTree 
    {
        public FoxBehaviorTree()
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
            // Code for searching for prey goes here
            // Return SUCCESS if prey is found, FAILURE if not
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
            // Get a list of all rabbits within the sensor's range
            List<GameObject> rabbitsInRange = GetRabbitsInRange();

            // Check if any rabbits are within the sensor's field of view
            foreach (GameObject rabbit in rabbitsInRange)
            {
                // Check if the rabbit is within the sensor's field of view
                if (IsInFieldOfView(rabbit))
                {
                    // If the rabbit is within the sensor's field of view, return true
                    return true;
                }
            }

            // If no rabbits are within the sensor's field of view, return false
            return false;
        }
        
        private bool IsPreyCloseEnough()
        {
            // Code for checking if prey is close enough to attack goes here
            // Return true if prey is close enough, false if not
            return false;
        }
    }
}
