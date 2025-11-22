using UnityEngine;

namespace IRM.Systems.Utility
{
    public readonly struct BoundsCorners
    {
        public const int CORNERS_COUNT = 8;
        private readonly Vector3 c0, c1, c2, c3, c4, c5, c6, c7;
        
        public BoundsCorners(Bounds bounds)
        {
            var center = bounds.center;
            var extends = bounds.extents;

            c0 = center + new Vector3(extends.x, extends.y, extends.z);
            c1 = center + new Vector3(extends.x, extends.y, -extends.z);
            c2 = center + new Vector3(extends.x, -extends.y, extends.z);
            c3 = center + new Vector3(extends.x, -extends.y, -extends.z);
            c4 = center + new Vector3(-extends.x, extends.y, extends.z);
            c5 = center + new Vector3(-extends.x, extends.y, -extends.z);
            c6 = center + new Vector3(-extends.x, -extends.y, extends.z);
            c7 = center + new Vector3(-extends.x, -extends.y, -extends.z);
        }
        
        public Vector3 this[int index] => index switch
        {
            0 => c0,
            1 => c1,
            2 => c2,
            3 => c3,
            4 => c4,
            5 => c5,
            6 => c6,
            7 => c7,
            _ => Vector3.zero
        };
    }
}