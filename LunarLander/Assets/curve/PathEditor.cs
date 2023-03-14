using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[RequireComponent(typeof(LineRenderer))]
[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor {

    PathCreator creator;
    Path path;
    LineRenderer lineRenderer;
    int SEGMENT_COUNT = 50;
    //List<Vector2> dot;

    void OnSceneGUI()
    {
        //Input();
        DrawCurve();
    }

    /*void Input()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(creator, "Add segment");
            path.AddSegment(mousePos);
        }
    }*/

    /*void Draw()
    {

        for (int i = 0; i < path.NumSegments; i++)
        {
            Vector2[] points = path.GetPointsInSegment(i);
            Handles.color = Color.black;
            //Handles.DrawLine(points[1], points[0]);
            //Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
        }

        Handles.color = Color.red;
        for (int i = 0; i < path.NumPoints; i++)
        {
            Vector2 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, .1f, Vector2.zero, Handles.CylinderHandleCap);
            if (path[i] != newPos)
            {
                Undo.RecordObject(creator, "Move point");
                path.MovePoint(i, newPos);
            }
        }
    }*/
    /**/void DrawCurve()
    {
        Handles.color = Color.red;
        for (int j = 0; j < path.NumSegments; j++)
        {
            Vector2[] points = path.GetPointsInSegment(j);
            //Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
            Vector2 temp = points[0];  
            for (int i = 1; i <= SEGMENT_COUNT; i++)
           {
                float t = i / (float)SEGMENT_COUNT;
                Vector2 pixel = CalculateCubicBezierPoint(t, points[0], points[1], points[2], points[3]);
                //dot.Add(pixel);
                
                Handles.DrawLine(pixel, temp);
                temp = pixel;
                //lineRenderer.SetVertexCount(((j * SEGMENT_COUNT) + i));
                //lineRenderer.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);
            }
        }
        //Handles.color = Color.red;
        //for (int j = 1; j < dot.Count; j++){
        //    Handles.DrawLine(dot[j], dot[j-1]);
        //}

        
    }
    Vector2 CalculateCubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        
        Vector2 p = uuu * p0; 
        p += 3 * uu * t * p1; 
        p += 3 * u * tt * p2; 
        p += ttt * p3; 
        
        return p;
    }

    /*void OnEnable()
    {
        creator = (PathCreator)target;
        if (creator.path == null)
        {
            creator.CreatePath();
        }
        path = creator.path;
    }*/
}
