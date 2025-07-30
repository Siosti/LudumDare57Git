using MovingObjectSystem.WaypointsProvider;
using System;
using UnityEngine;

namespace MonsterBrainSystem
{
    [Serializable]
    public class MonsterObjectives
    {
        [SerializeField] private AMBWaypointsProvider waypointsProvider;
        
        [field: Space]
        [field: SerializeField] public bool IsActive { get; private set; }

        public Vector3[] PatrolRoute { get => waypointsProvider.Waypoints; }
    }
}
