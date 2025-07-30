using UnityEngine;

namespace MovingObjectSystem.WaypointsProvider
{
    public class MBTransformChildrenWaypointsProvider : AMBWaypointsProvider
    {
        private Vector3[] _waypoints;

        public override Vector3[] Waypoints
        {
            get
            {
                if (_waypoints == null)
                    CollectWaypoints();

                return _waypoints;
            }
        }

        public void CollectWaypoints()
        {
            Transform[] transforms = GetComponentsInChildren<Transform>();

            _waypoints = new Vector3[transforms.Length];

            for (int i = 0; i < transforms.Length; i++)
            {
                _waypoints[i] = transforms[i].position;
            }
        }
    }
}
