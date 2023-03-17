using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsYouWish : MonoBehaviour
{
    public Path path;
    int SEGMENT_COUNT = 50;
    float OffsetX = -12f, OffsetY = -3f;
    Color c1 = Color.yellow;
    Color c2 = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        path = new Path(transform.position);
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2f, 0.2f);
        lineRenderer.SetVertexCount(SEGMENT_COUNT * path.NumSegments);
        DrawCurve();
        //SetEdgeCollider(lineRenderer);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void SetEdgeCollider(LineRenderer myLine)
    {
        List<Vector2> edges = new List<Vector2>();
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        for(int point = 0; point<myLine.positionCount; point++)
        {
            Vector3 lineRendererPoint = myLine.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }
        edgeCollider.SetPoints(edges);
    }*/
    
     void DrawCurve()
    {
        List<Vector2> edges = new List<Vector2>();
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        var PointsLineRender = new Vector3[SEGMENT_COUNT * path.NumSegments];
        for (int j = 0; j < path.NumSegments; j++)
        {
            Vector2[] PointsBerzier = path.GetPointsInSegment(j);
            for (int i = 1; i <= SEGMENT_COUNT; i++)
           {
                float t = i / (float)SEGMENT_COUNT;
                Vector2 pixel = CalculateCubicBezierPoint(t, PointsBerzier[0], PointsBerzier[1], PointsBerzier[2], PointsBerzier[3]);
                edges.Add(new Vector2(pixel.x - OffsetX, pixel.y - OffsetY));
                PointsLineRender[(j * SEGMENT_COUNT) + (i - 1)] = new Vector3(pixel.x, pixel.y, 0); // elver le i-1 si sa marche
            }
        }
        lineRenderer.SetPositions(PointsLineRender);
        edgeCollider.SetPoints(edges);
        
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