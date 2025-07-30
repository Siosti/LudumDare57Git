using System.Collections.ObjectModel;
using UnityEngine;

namespace MovingObjectSystem.WaypointsProvider
{
    public abstract class AMBWaypointsProvider : MonoBehaviour
    {
        public abstract Vector3[] Waypoints { get; }
    }
}
