using System.Collections;
using System.Collections.Generic;
#if !APP_MAIN && UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace OverSDK
{
#if !APP_MAIN && UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class OvrSpawnPointBase : MonoBehaviour
    {
        private float radius = 0.25f;
        // Set the number of segments for circle smoothness
        private int segments = 50;
        private float triangleOffset = 0.05f;

#if !APP_MAIN && UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            this.hideFlags = HideFlags.HideInInspector; // Hide this script in the inspector
            DrawCircleWithCutouts(transform.position, radius, segments);
            DrawDirectionalTriangle(transform.position, radius);
        }

        void Update()
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

            if(transform.position.y <= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }

        void DrawCircleWithCutouts(Vector3 center, float radius, int segments)
        {
            Gizmos.color = Color.white;
            // Draw the circle
            float angle = 0f;
            Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            for (int i = 1; i <= segments; i++)
            {
                angle = 2 * Mathf.PI * i / segments;
                Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
                Handles.DrawLine(lastPoint, nextPoint, 3);
                lastPoint = nextPoint;
            }
            // Draw the cutout lines
            float cutoutAngle = Mathf.PI / 4; // 45 degrees in radians
            for (int i = 0; i < 4; i++)
            {
                Vector3 cutoutStart = center + new Vector3(Mathf.Cos(cutoutAngle) * radius, 0f, Mathf.Sin(cutoutAngle) * radius);
                Vector3 cutoutEnd = center + new Vector3(Mathf.Cos(cutoutAngle + Mathf.PI / 2) * radius, 0f, Mathf.Sin(cutoutAngle + Mathf.PI / 2) * radius);
                Handles.DrawLine(center, cutoutStart, 3);
                Handles.DrawLine(center, cutoutEnd, 3);
                cutoutAngle += Mathf.PI / 2; // Rotate 90 degrees for the next cutout
            }
        }
        void DrawDirectionalTriangle(Vector3 center, float radius)
        {
            Gizmos.color = Color.white;
            // Define the triangle points
            float triangleSize = radius / 5f;
            Vector3 point1 = new Vector3(0f, 0f, radius + triangleOffset + triangleSize);
            Vector3 point2 = new Vector3(-triangleSize, 0f, radius + triangleOffset);
            Vector3 point3 = new Vector3(triangleSize, 0f, radius + triangleOffset);
            // Apply the rotation of the GameObject to the points
            point1 = transform.rotation * point1;
            point2 = transform.rotation * point2;
            point3 = transform.rotation * point3;
            // Translate the points to the center position
            point1 += center;
            point2 += center;
            point3 += center;
            // Draw the triangle
            Handles.DrawLine(point1, point2, 3);
            Handles.DrawLine(point2, point3, 3);
            Handles.DrawLine(point3, point1, 3);
        }
#endif
    }
}
