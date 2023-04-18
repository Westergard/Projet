using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBezier : MonoBehaviour
{
    public Path path;
    int SEGMENT_COUNT = 10, SEGMENT_COUNT_Half; // le nombre de point dans chaque segment
    LineRenderer lineRenderer; 
    EdgeCollider2D edgeCollider;
    Vector3 Offset = new Vector2(-12, -3); // la class path commence en bas a gauche, avec ce offset. 
    TourelleBezier tourelle;
    public GameObject tourel;
    Vector3 PositionTourelle = new Vector3(1.0f, 1.0f, 1.0f);
    public float PenteTourette = 90;
    public float delaiTourelle = 1;


    void Start()
    {
        SEGMENT_COUNT_Half = (int)Mathf.Floor(SEGMENT_COUNT * .5f); // utiliser pour trouvere une position a la tourelle et la cible
        tourelle = tourel.GetComponent<TourelleBezier>();
        path = new Path(transform.position + Offset); // retourne just une list de point pour fait la courbe de Bezier. 
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        lineRenderer.SetVertexCount(SEGMENT_COUNT * path.NumSegments);
        DrawCurve(); // dessiner la courbe et lui mettre un EdgeCollider2D
        MeshOfTriangle(lineRenderer); //mettre un mesh sous la courbe de bezier
        tourelle.transform.position = new Vector3(0, -5, 0);
    }
    

    void Update()
    {
        // attendre une seconde avant de mettre la tourelle
        if(delaiTourelle > 0)
        {
            delaiTourelle -= Time.deltaTime;
        }
        else if (delaiTourelle < 0)
        {
            delaiTourelle = 0;
            ChangerPositionTourelle();
        }
    }
    

     void DrawCurve()
    {
        List<Vector2> CurvePoint = new List<Vector2>();// la courbe en liste de Vectro2 pour le edgeCollider
        var PointsLineRender = new Vector3[SEGMENT_COUNT * path.NumSegments]; // la courbe en liste de Vectro3 pour le lineRenderer
        for (int j = 0; j < path.NumSegments; j++) // j = le nombre de segments
        {
            Vector2[] PointsBerzier = path.GetPointsInSegment(j); // les quatres points utiliser pour faire une courbes de bézier cubiques.
            for (int i = 1; i <= SEGMENT_COUNT; i++) // i = le nombre de point dans un segments
           {
                float t = i / (float)SEGMENT_COUNT;// 0 ≤ t ≤ 1, donc t passe de 0 a 1 avec le nombre de point dans le segment.
                Vector2 pixel = CalculateCubicBezierPoint(t, PointsBerzier[0], PointsBerzier[1], PointsBerzier[2], PointsBerzier[3]);
                CurvePoint.Add(pixel);
                PointsLineRender[(j * SEGMENT_COUNT) + (i - 1)] = new Vector3(pixel.x, pixel.y, 0); // i - 1 puisque il commence a 1
            }
        }
        lineRenderer.SetPositions(PointsLineRender);
        edgeCollider.SetPoints(CurvePoint);
    }


    //simplement utiliser la formule de la courbes de bézier cubiques. où 0 ≤ t ≤ 1
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

   //t = ((i/m-b))+b
    void MeshOfTriangle(LineRenderer myLine){

    Vector2[] uv = new Vector2[myLine.positionCount * 2]; // égale au nombre de point pour faire les triangle (la ligne suivante)
    Vector3[] MeshVertices = new Vector3[myLine.positionCount * 2]; // un point aditionaile sous chaque point. 
    float InverceDuNombreDePoint = 1/(float)myLine.positionCount;
    for (int j = 0; j < myLine.positionCount; ++j) {
        MeshVertices[2 * j] = new Vector3(myLine.GetPosition(j).x, -6, -1); // un point sous la courbe
        MeshVertices[2 * j + 1] = myLine.GetPosition(j); // un point sur la courbe

        // la position en x du UV va passer de -1 a 1, 
        // et sa position en y va rester entre -1 et 0, plus precisement entre -1 et (* entre -0.32 et -0.64) 
        //* puisque myLine.GetPosition(j).y varie entre -2 et -4. 
        uv[2 * j] = new Vector2(j*InverceDuNombreDePoint*2-1, -1); 
        uv[2 * j + 1] = new Vector2(j*InverceDuNombreDePoint*2-1, myLine.GetPosition(j).y * 0.16f); 
    }   
 
     int numTriangles = (myLine.positionCount -1) * 2; // le premier point n'a pas de triangle et les autre en ont deux chaque.
     int[] triangles = new int[numTriangles * 3];  // chaque triangle on trois point
 
     int i = 0;
     for (int t = 0; t < numTriangles; t += 2) {// mettre deux triangle a la foi
         // triangle avec un angle droit en bas a gauche
         triangles[i++] = t;
         triangles[i++] = t +1;
         triangles[i++] = t +2;
         //triangle avec un angle droit en haut a droite
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


    public Vector3 PositionSurMap(){// changer la position de la cible sur la map (pas trop proche des extrémités de la map)
        return lineRenderer.GetPosition(Random.Range(SEGMENT_COUNT_Half + SEGMENT_COUNT ,lineRenderer.positionCount - SEGMENT_COUNT_Half));
    }


    public void ChangerPositionTourelle(){
        int temp;
        do{     
            temp = Random.Range(SEGMENT_COUNT_Half + SEGMENT_COUNT ,lineRenderer.positionCount - SEGMENT_COUNT_Half);// un point sur la map pas trop proche des extrémités
            CalculePente(lineRenderer.GetPosition(temp-1), lineRenderer.GetPosition(temp+1));// temp-1 et temp+1 pour avoir une pente plus précis.
        } while (PenteTourette > 40|| PenteTourette < -40 );// pour que la tourelle ne soit pas trop sur le coter.
        PositionTourelle = lineRenderer.GetPosition(temp);
        tourelle.transform.position = PositionTourelle;
    }


    void CalculePente (Vector3 Point1, Vector3 Point2){// La pente va être utilisé dans la script TourelleBezier.
        float DeltaY, DeltaX, Pent;
        DeltaX = Point1.x - Point2.x;
        DeltaY = Point1.y - Point2.y;
        Pent = DeltaY / DeltaX;

        PenteTourette = Mathf.Atan(Pent) * 180 / 3.1416f;//de RAD en DEG
    }
}