using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UtilityAi;

namespace BehaviorTree
{
    public class FoxBehaviorTree : BehaviorTree 
    {
       private bool _isFollowingScent = false;
       private Vector3 _scentLocation = Vector3.zero;
       private float _attackRange;
       private FoxController _foxController;
       private GameObject closestRabbit;
       private float _closestDistance = float.MaxValue;         // Contains the distance to closest rabbit
        
        public FoxBehaviorTree(FoxController foxController)
        {
            _foxController = foxController;
            
            // Create root node
            SelectorNode root = new SelectorNode();
            
            // Create leaf nodes for actions and conditions
            ActionNode searchForPrey = new ActionNode(SearchForPrey);
            ActionNode stalkPrey = new ActionNode(StalkPrey);
            ActionNode attackPrey = new ActionNode(AttackPrey);

            // Passing over the closest rabbit var from the bool method
            ConditionNode isPreyInSight = new ConditionNode(() => IsPreyInSight(out closestRabbit));
            ConditionNode isPreyCloseEnough = new ConditionNode(IsPreyCloseEnough);
            
            // Create sequence for chasing prey
            SequenceNode chasePrey = new SequenceNode();
            chasePrey.AddChild(isPreyInSight);
            chasePrey.AddChild(isPreyCloseEnough);
            chasePrey.AddChild(attackPrey);
            
            // Add chase sequence and search action to root selector
            root.AddChild(searchForPrey);
            root.AddChild(chasePrey);
            root.AddChild(stalkPrey);

            _attackRange = _foxController.AttackRange;
            
            // Set root node as the tree's root node
            SetRootNode(root);
        }

        // Action methods
        private BehaviorTreeStatus SearchForPrey()
        {
            // searching for rabbit tracks
            // following the rabbit's smell or noise
            _foxController.Wander();
            
            
            return BehaviorTreeStatus.TEST;
        }

        private BehaviorTreeStatus StalkPrey()
        {
            return BehaviorTreeStatus.TEST;
        }
        
        private BehaviorTreeStatus AttackPrey()
        {
            // Code for attacking prey goes here
            // Return SUCCESS if prey is successfully attacked, FAILURE if not
            return BehaviorTreeStatus.TEST;
        }
        
        // Condition methods that must be met in order for the node to be executed
        private bool IsPreyInSight(out GameObject closestRabbit)
        {
            closestRabbit = null;
    
            // Loop through all rabbits within the sensor's range
            foreach (GameObject rabbit in _foxController._foxSensor.GetRabbitsInRange())
            {
                // Calculate the distance between the fox and the rabbit
                float distance = Vector3.Distance(_foxController.transform.position, rabbit.transform.position);

                // Check if this rabbit is closer than the current closest rabbit
                if (distance < _closestDistance)
                {
                    // Update the closest rabbit and its distance
                    closestRabbit = rabbit;
                    _closestDistance = distance;
                }
            }
    
            // Return true if a rabbit was found, false if not
            return closestRabbit != null;
        }

        private bool IsPreyCloseEnough()
        {
            return _closestDistance < _attackRange;
        }
    }
}
