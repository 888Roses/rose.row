using UnityEngine;

namespace rose.row.util
{
    public static class BoundsUtil
    {
        public static Vector3[] getCorners(this Bounds bounds)
        {
            Vector3 c = bounds.center;
            Vector3 e = bounds.extents;

            Vector3[] worldCorners = new[] {
                new Vector3( c.x + e.x, c.y + e.y, c.z + e.z ),
                new Vector3( c.x + e.x, c.y + e.y, c.z - e.z ),
                new Vector3( c.x + e.x, c.y - e.y, c.z + e.z ),
                new Vector3( c.x + e.x, c.y - e.y, c.z - e.z ),
                new Vector3( c.x - e.x, c.y + e.y, c.z + e.z ),
                new Vector3( c.x - e.x, c.y + e.y, c.z - e.z ),
                new Vector3( c.x - e.x, c.y - e.y, c.z + e.z ),
                new Vector3( c.x - e.x, c.y - e.y, c.z - e.z ),
            };

            return worldCorners;
        }

        public static void gizmoDrawEdges(this Bounds bounds, Color colour, float duration = 0f)
        {
            Vector3 c = bounds.center;
            Vector3 e = bounds.extents;

            IngameDebugGizmos.DrawLine(new Vector3(c.x + e.x, c.y + e.y, c.z + e.z), new Vector3(c.x + e.x, c.y + e.y, c.z - e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x + e.x, c.y + e.y, c.z - e.z), new Vector3(c.x - e.x, c.y + e.y, c.z - e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x - e.x, c.y + e.y, c.z - e.z), new Vector3(c.x - e.x, c.y + e.y, c.z + e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x - e.x, c.y + e.y, c.z + e.z), new Vector3(c.x + e.x, c.y + e.y, c.z + e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x + e.x, c.y - e.y, c.z + e.z), new Vector3(c.x + e.x, c.y - e.y, c.z - e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x + e.x, c.y - e.y, c.z - e.z), new Vector3(c.x - e.x, c.y - e.y, c.z - e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x - e.x, c.y - e.y, c.z - e.z), new Vector3(c.x - e.x, c.y - e.y, c.z + e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x - e.x, c.y - e.y, c.z + e.z), new Vector3(c.x + e.x, c.y - e.y, c.z + e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x + e.x, c.y - e.y, c.z + e.z), new Vector3(c.x + e.x, c.y + e.y, c.z + e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x - e.x, c.y - e.y, c.z + e.z), new Vector3(c.x - e.x, c.y + e.y, c.z + e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x - e.x, c.y - e.y, c.z - e.z), new Vector3(c.x - e.x, c.y + e.y, c.z - e.z), colour, duration);
            IngameDebugGizmos.DrawLine(new Vector3(c.x + e.x, c.y - e.y, c.z - e.z), new Vector3(c.x + e.x, c.y + e.y, c.z - e.z), colour, duration);
        }

        public static Bounds getMaxBounds(this Transform transform, float maxDistance = -1f)
            => getMaxBounds(transform.gameObject);

        public static Bounds getMaxBounds(this GameObject gameObject, float maxDistance = -1f)
        {
            var renderers = gameObject.GetComponentsInChildren<MeshRenderer>(includeInactive: false);
            if (renderers.Length == 0)
                return new Bounds(gameObject.transform.position, Vector3.zero);

            var outputBounds = renderers[0].bounds;
            foreach (var renderer in renderers)
            {
                if (maxDistance > 0 && Vector3.Distance(renderer.transform.localPosition, Vector3.zero) > maxDistance)
                    continue;

                if (renderer.enabled)
                    outputBounds.Encapsulate(renderer.bounds);
            }

            return outputBounds;
        }
    }
}
