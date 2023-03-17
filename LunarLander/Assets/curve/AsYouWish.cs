using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class AsYouWish : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    LineRenderer myLine;
    private Path path;
    int SEGMENT_COUNT = 50;

    void Awake(){
        path = GetComponent<Path>();
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        myLine = this.GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //DrawCurve();
        //myLine.SetVertexCount(2);
        //myLine.SetPosition(0, new Vector3(-1, 0, 0));
        //myLine.SetPosition(0, new Vector3(2, 0, 0));
        //SetEdgeCollider(myLine);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetEdgeCollider(LineRenderer lineRenderer)
    {
        List<Vector2> edges = new List<Vector2>();

        for(int point = 0; point<lineRenderer.positionCount; point++)
        {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }

        edgeCollider.SetPoints(edges);
    }
    
     void DrawCurve()
    {
        //Handles.color = Color.red;
        for (int j = 0; j < path.NumSegments; j++)
        {
            Vector2[] points = path.GetPointsInSegment(j);
            //Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
            //Vector2 temp = points[0];  
            for (int i = 1; i <= SEGMENT_COUNT; i++)
           {
                float t = i / (float)SEGMENT_COUNT;
                Vector2 pixel = CalculateCubicBezierPoint(t, points[0], points[1], points[2], points[3]);
                //dot.Add(pixel);
                
                //Handles.DrawLine(pixel, temp);
                //temp = pixel;
                myLine.SetVertexCount(((j * SEGMENT_COUNT) + i));
                Vector3 temp = new Vector3(pixel.x, pixel.y, 0);
                myLine.SetPosition((j * SEGMENT_COUNT) + (i - 1), temp);
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
}