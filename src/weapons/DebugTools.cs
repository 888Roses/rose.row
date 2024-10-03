using UnityEngine;

namespace rose.row.weapons
{
    public static class DebugTools
    {
        public static GameObject createSphere(Vector3 pos, float radius, Color colour, float lifetime = 10f)
        {
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject.Destroy(gameObject.GetComponent<Collider>());
            gameObject.transform.position = pos;
            gameObject.transform.localScale = Vector3.one * radius;
            gameObject.transform.GetComponent<Renderer>().material.color = colour;
            GameObject.Destroy(gameObject, lifetime);
            return gameObject;
        }

        public static GameObject connectPoints(Vector3 a, Vector3 b, float radius, Color colour, float lifetime = 10f)
        {
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            GameObject.Destroy(gameObject.GetComponent<Collider>());
            gameObject.transform.localScale = new Vector3(radius, Vector3.Distance(a, b), radius);
            gameObject.transform.GetComponent<Renderer>().material.color = colour;
            gameObject.transform.localPosition = (a - b) / 2;
            gameObject.transform.up = (a - b).normalized;

            return gameObject;
        }
    }
}
