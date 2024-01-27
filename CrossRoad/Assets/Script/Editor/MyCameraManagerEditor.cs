
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

namespace CrossRoadProject
{
    [CustomEditor(typeof(MyCameraManager))]
    [InitializeOnLoad]
    public class MyCameraManagerEditor : Editor
    {

        static public void DrawBounds(Bounds b)
        {
            Handles.color = Color.blue;
            // bottom
            var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
            var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
            var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
            var p4 = new Vector3(b.min.x, b.min.y, b.max.z);


            Handles.DrawLine(p1, p2);
            Handles.DrawLine(p2, p3);
            Handles.DrawLine(p3, p4);
            Handles.DrawLine(p4, p1);
       

            // top
            var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
            var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
            var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
            var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

            Handles.DrawLine(p5, p6);
            Handles.DrawLine(p6, p7);
            Handles.DrawLine(p7, p8);
            Handles.DrawLine(p8, p5);

            // sides
            Handles.DrawLine(p1, p5);
            Handles.DrawLine(p2, p6);
            Handles.DrawLine(p3, p7);
            Handles.DrawLine(p4, p8);
           
        }

        [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
        static void DrawGameObjectName(MyCameraManager cameraManager, GizmoType gizmoType)
        {
            GUIStyle style = new GUIStyle();
            Vector3 v3FrontTopLeft;

            if (cameraManager.cameraBounds.size != Vector3.zero)
            {
                style.normal.textColor = Color.yellow;
                v3FrontTopLeft = new Vector3(cameraManager.cameraBounds.center.x - cameraManager.cameraBounds.extents.x, cameraManager.cameraBounds.center.y +
                    cameraManager.cameraBounds.extents.y + 1, cameraManager.cameraBounds.center.z - cameraManager.cameraBounds.extents.z);  // Front top left corner
                Handles.Label(v3FrontTopLeft, "Camera Bounds", style);
                DrawBounds(cameraManager.cameraBounds);
                //MMDebug.DrawHandlesBounds(enemyLevelConstrain._Enemybounds, Color.yellow);
            }
        }
    }

}