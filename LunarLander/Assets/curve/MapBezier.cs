using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBezier : MonoBehaviour
{
    public Path path;
    int SEGMENT_COUNT = 50;
    Vector3 Offset = new Vector2(-12, -3);
    Color c1 = Color.yellow;
    Color c2 = Color.red;
    Tourelle tourelle;
    public GameObject tourel;
    Vector3 PositionTourelle = new Vector3(1.0f, 1.0f, 1.0f);
    public float PenteTourette = 180;
    private SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        tourelle = tourel.GetComponent<Tourelle>();
        path = new Path(transform.position + Offset);
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.07f, 0.07f);
        lineRenderer.SetVertexCount(SEGMENT_COUNT * path.NumSegments);
        DrawCurve();
        MeshOfTriangle(lineRenderer);
    }
    
    void Update()
    {
        tourelle.transform.position = PositionTourelle;
    }
    
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
                edges.Add(new Vector2(pixel.x, pixel.y));
                PointsLineRender[(j * SEGMENT_COUNT) + (i - 1)] = new Vector3(pixel.x, pixel.y, 0); // elver le i-1 si sa marche
            }
        }
        lineRenderer.SetPositions(PointsLineRender);
        edgeCollider.SetPoints(edges);
        int temp;
        do{
            temp = Random.Range(75,525);
            CalculePente(lineRenderer.GetPosition(temp-1), lineRenderer.GetPosition(temp+1));
        } while (PenteTourette > 50|| PenteTourette < -50 );
        PositionTourelle = lineRenderer.GetPosition(temp);
        
        
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
    void CalculePente (Vector3 Point1, Vector3 Point2){
        float DeltaY, DeltaX, Pent;
        DeltaX = Point1.x - Point2.x;
        DeltaY = Point1.y - Point2.y;
        Pent = DeltaY / DeltaX;

        PenteTourette = Mathf.Atan(Pent) * 180 / 3.1416f;
    }
    void MeshOfTriangle(LineRenderer myLine){

    Vector3[] MeshVertices = new Vector3[myLine.positionCount * 2]; // one point below each line point
    for (int j = 0; j < myLine.positionCount; ++j) {
        MeshVertices[2 * j] = new Vector3(myLine.GetPosition(j).x, -6, 0);
        MeshVertices[2 * j + 1] = myLine.GetPosition(j);
    }
 
     int numTriangles = (myLine.positionCount -1) * 2; // le premier point n'a pas de triangle et les autre n'on deux chaque.
     int[] triangles = new int[numTriangles * 3]; 
 
     int i = 0;
     for (int t = 0; t < numTriangles; t += 2) {
         // triangle en bas a gauche
         triangles[i++] = t;
         triangles[i++] = t +1;
         triangles[i++] = t +2;
         //triangle en haut a droite
         triangles[i++] = t + 1;
         triangles[i++] = t + 2;
         triangles[i++] = t + 3;
     }

     Mesh mesh = new Mesh();
     mesh.vertices = MeshVertices;
     mesh.triangles = triangles;
     GetComponent<MeshFilter>().mesh = mesh;

    }
}