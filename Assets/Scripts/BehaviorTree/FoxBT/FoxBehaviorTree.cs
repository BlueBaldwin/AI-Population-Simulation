using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UtilityAi;

namespace BehaviorTree
{
    public class FoxBehaviorTree : BehaviorTree
    {
        private FoxStats _stats;
        private bool _isFollowingScent = false;
        private Vector3 _scentLocation;
        private float _attackRange;
        private FoxController _foxController;
        private FoxSensor _sensor;
        private float _closestDistance = float.MaxValue; // Contains the distance to closest rabbit

        public FoxBehaviorTree(FoxController foxController, FoxSensor sensor)
        {
            _foxController = foxController;
            _sensor = sensor;
            _stats = new FoxStats(0f, 0f, 0f);

            // Create root node
            SelectorNode root = new SelectorNode();

            // Create leaf nodes for actions
            ActionNode goToSleep = new ActionNode(GoToSleep);
            ActionNode searchForPrey = new ActionNode(SearchForPrey);

            // Create condition nodes 
            ConditionNode isTired = new ConditionNode(IsTired);

            // Create sequence for sleep
            SequenceNode sleepSequence = new SequenceNode();
            sleepSequence.AddChild(new ConditionNode(IsTired));
            sleepSequence.AddChild(new ActionNode(GoToSleep));

            // Create sequence for stalking prey
            SequenceNode stalkSequence = new SequenceNode();
            stalkSequence.AddChild(new ConditionNode(IsPreyInSight));
            stalkSequence.AddChild(new ActionNode(StalkPrey));

            // Create sequence for attacking prey
            SequenceNode attackSequence = new SequenceNode();
            attackSequence.AddChild(new ConditionNode(IsPreyCloseEnough));
            attackSequence.AddChild(new ActionNode(AttackPrey));

            // Add sleep, search, and stalking/attacking sequences to root selector
            root.AddChild(sleepSequence);
            root.AddChild(searchForPrey);
            root.AddChild(stalkSequence);
            root.AddChild(attackSequence);

            // Set root node as the tree's root node
            SetRootNode(root);
        }

        public override void Update()
        {
            _rootNode.Update();
            base.Update();
        }
        
        // Action methods

        private BehaviorTreeStatus GoToSleep()
        {
            if (_foxController.GoToSleep())
            {
                return BehaviorTreeStatus.SUCCESS;
            }

            return BehaviorTreeStatus.RUNNING;
        }

        private BehaviorTreeStatus SearchForPrey()
        {
            // Check if a scent is detected
            if (IsScentDetected())
            {
                // If a scent is detected, follow it by executing the FollowScent method
                if (FollowScent() == BehaviorTreeStatus.SUCCESS)
                {
                    // If the FollowScent method returns SUCCESS, return SUCCESS to indicate that the prey has been found
                    return BehaviorTreeStatus.SUCCESS;
                }
                else
                {
                    // If the FollowScent method returns RUNNING, return RUNNING to indicate that the SearchForPrey sequence is still in progress
                    return BehaviorTreeStatus.RUNNING;
                }
            }
            else
            {
                // If no scent is detected, wander randomly
                _foxController.Wander();
                // Return FAILURE to indicate that the prey has not been found
                return BehaviorTreeStatus.FAILURE;
            }
        }

        private BehaviorTreeStatus FollowScent()
        {
            
            // Follow the scent by moving towards the location of the rabbit droppings
            _foxController.MoveTowardsScent(_scentLocation);

            // Check if the fox has reached the location of the rabbit droppings
            if (Vector3.Distance(_foxController.transform.position, _scentLocation) < 0.1)
            {
                // Return SUCCESS if the fox has reached the location of the rabbit droppings
                return BehaviorTreeStatus.SUCCESS;
            }
            else
            {
                // Return RUNNING if the fox has not reached the location of the rabbit droppings yet
                return BehaviorTreeStatus.RUNNING;
            }
        }
        
        private BehaviorTreeStatus StalkPrey()
        {
            // Code for stalking prey goes here
            // Return SUCCESS if prey is successfully stalked, FAILURE if not
            return BehaviorTreeStatus.RUNNING;
        }

        private BehaviorTreeStatus AttackPrey()
        {
            // Code for attacking prey goes here
            // Return SUCCESS if prey is successfully attacked, FAILURE if not
            return BehaviorTreeStatus.RUNNING;
        }
        
        // Conditions

        private bool IsPreyInSight()
        {
            // Return true if prey is in sight, false if not
            return SensorUtility.GetObjectsInRange(_foxController._foxSensor, "Rabbit").Count > 0;
        }

        private bool IsPreyCloseEnough()
        {
            return _closestDistance < _attackRange;
        }
        
        private bool IsScentDetected()
        {
            GameObject scent = SensorUtility.FindClosestTaggedGoInList(_foxController._foxSensor, _sensor.GetDroppingsList(),"Dropping");

            if (scent == null) return false;
            _scentLocation = scent.transform.position;
            return true;

        }
        
        private bool IsTired()
        {
            return _stats.TirednessLevel > 80f;
        }
    }
}
