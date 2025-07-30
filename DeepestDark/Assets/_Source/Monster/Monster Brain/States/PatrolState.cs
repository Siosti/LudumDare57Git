using MonsterMovementSystem;
using StateMachineSystem;
using System;
using UnityEngine;

namespace MonsterBrainSystem
{
    public class PatrolState : IState
    {
        private readonly MonsterObjectives _objectives;
        private readonly MonsterMovement _movement;

        private int _nextWaypointIndex;

        public PatrolState(MonsterMovement movement, MonsterObjectives objectives)
        {
            _movement = movement != null ? movement : throw new ArgumentNullException(nameof(movement));
            _objectives = objectives ?? throw new ArgumentNullException(nameof(objectives));
        }

        public void Enter()
        {
            Debug.Log("Patroling");

            _movement.SetQuiteSpeed();

            MoveToNextWaypoint();
        }

        public void Exit()
        {
            _movement.StopMoving();
        }

        public void Tick()
        {
            if (_movement.ReachedEndOfPath)
                MoveToNextWaypoint();
        }

        private void MoveToNextWaypoint()
        {
            if (_objectives.PatrolRoute.Length <= 0)
                return;

            if (_nextWaypointIndex + 1 < _objectives.PatrolRoute.Length)
                _nextWaypointIndex++;
            else
                _nextWaypointIndex = 0;

            Vector3 nextWaypoint = _objectives.PatrolRoute[_nextWaypointIndex];

            _movement.MoveToPosition(nextWaypoint);
        }
    }
}
