using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBezier : MonoBehaviour
{
    public Path path;
    int SEGMENT_COUNT = 10, SEGMENT_COUNT_Half;
    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider;
    Vector3 Offset = new Vector2(-12, -3); // la class path commence en bas a gauche, avec se offset la. 
    //Color c1 = Color.yellow;
    //Color c2 = Color.red;
    TourelleBezier tourelle;
    public GameObject tourel;
    Vector3 PositionTourelle = new Vector3(1.0f, 1.0f, 1.0f);
    public float PenteTourette = 90;
    //private SpriteRenderer m_SpriteRenderer;

    void Start()
    {
        SEGMENT_COUNT_Half = (int)Mathf.Floor(SEGMENT_COUNT * .5f); // utiliser pour calculer la position de la tourelle et de la cible
        tourelle = tourel.GetComponent<TourelleBezier>();
        path = new Path(transform.position + Offset); // retourne just une list de point pour fait la courbe de Bezier. 
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        //lineRenderer.SetColors(c1, c2);
        //lineRenderer.SetWidth(0.07f, 0.07f);
        lineRenderer.SetVertexCount(SEGMENT_COUNT * path.NumSegments);
        DrawCurve();
        MeshOfTriangle(lineRenderer);
    }
    
    void Update()
    {
        //tourelle.transform.position = PositionTourelle;
    }
    
     void DrawCurve()
    {
        List<Vector2> edges = new List<Vector2>();
        //LineRenderer lineRenderer = GetComponent<LineRenderer>();
        //EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
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
        ChangerPositionTourelle();
        
        
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
    
    void MeshOfTriangle(LineRenderer myLine){

    Vector2[] uv = new Vector2[myLine.positionCount * 2];
    Vector3[] MeshVertices = new Vector3[myLine.positionCount * 2];
    for (int j = 0; j < myLine.positionCount; ++j) {
        MeshVertices[2 * j] = new Vector3(myLine.GetPosition(j).x, -6, -1);
        MeshVertices[2 * j + 1] = myLine.GetPosition(j);
        uv[2 * j] = new Vector2(j%70*.0285f-1, -1);
        uv[2 * j + 1] = new Vector2(j%70*.0285f-1, myLine.GetPosition(j).y * 0.15f); 
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
         triangles[i++] = t + 3;
         triangles[i++] = t + 2;
         triangles[i++] = t + 1;
     }
    
     Mesh mesh = new Mesh();
     mesh.vertices = MeshVertices;
     mesh.uv = uv;
     mesh.triangles = triangles;
     GetComponent<MeshFilter>().mesh = mesh;
     var materials  = GetComponent<Renderer>().materials;
    }

    public Vector3 PositionSurMap(){
        return lineRenderer.GetPosition(Random.Range(SEGMENT_COUNT_Half + SEGMENT_COUNT ,lineRenderer.positionCount - SEGMENT_COUNT_Half));
    }

    public void ChangerPositionTourelle(){
        int temp;
        do{     
            temp = Random.Range(SEGMENT_COUNT_Half + SEGMENT_COUNT ,lineRenderer.positionCount - SEGMENT_COUNT_Half);
            CalculePente(lineRenderer.GetPosition(temp-1), lineRenderer.GetPosition(temp+1));
        } while (PenteTourette > 50|| PenteTourette < -50 );
        PositionTourelle = lineRenderer.GetPosition(temp);
        tourelle.transform.position = PositionTourelle;
    }

    void CalculePente (Vector3 Point1, Vector3 Point2){
        float DeltaY, DeltaX, Pent;
        DeltaX = Point1.x - Point2.x;
        DeltaY = Point1.y - Point2.y;
        Pent = DeltaY / DeltaX;

        PenteTourette = Mathf.Atan(Pent) * 180 / 3.1416f;
    }
}