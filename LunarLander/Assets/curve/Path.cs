using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path {

    [SerializeField, HideInInspector]
    List<Vector2> points;

    public Path(Vector2 centre)
    {
       
        points = new List<Vector2>
        {
            centre+Vector2.left,
            centre+(Vector2.left+Vector2.up),
            centre + (Vector2.right * Random.Range(1,9) * .25f + Vector2.down * Random.Range(-8,8)*.25f),
            centre + (Vector2.right * 2) 
        };
        for (int i = 2; i < 11; i++)
        {
            AddSegment(centre + Vector2.right * i * 2 + Vector2.down * Random.Range(-8,8) * .25f );
        }
        AddSegment(centre + Vector2.right * 22);
        /*AddSegmentCustom(centre + Vector2.right * 24 + Vector2.down * 5, Vector2.right + Vector2.up);
        AddSegmentCustom(centre + Vector2.down * 5, Vector2.right + Vector2.down);
        AddSegmentCustom(centre + Vector2.left, Vector2.up);*/
    }

    public Vector2 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    public int NumPoints
    {
        get
        {
            return points.Count;
        }
    }

    public int NumSegments
    {
        get
        {
            return (points.Count - 4) / 3 + 1;
        }
    }

    public void AddSegment(Vector2 NextAnchorPoint)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        //points.Add((points[points.Count - 1] + NextAnchorPoint) * .5f);
        points.Add(NextAnchorPoint + Vector2.left * Random.Range(1,9) * .25f + Vector2.down * Random.Range(-8,8) * .25f );
        points.Add(NextAnchorPoint);
    }
    public void AddSegmentCustom(Vector2 NextAnchorPoint, Vector2 OtherPoint)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        //points.Add((points[points.Count - 1] + NextAnchorPoint) * .5f);
        points.Add(NextAnchorPoint + OtherPoint);
        points.Add(NextAnchorPoint);
    }

    public Vector2[] GetPointsInSegment(int i)
    {
        return new Vector2[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[i * 3 + 3] };
    }

    /*public void MovePoint(int i, Vector2 pos)
    {
        Vector2 deltaMove = pos - points[i];
        points[i] = pos;

        if (i % 3 == 0)
        {
            if (i + 1 < points.Count)
            {
                points[i + 1] += deltaMove;
            }
            if (i - 1 >= 0)
            {
                points[i - 1] += deltaMove;
            }
        }
        else
        {
            bool nextPointIsAnchor = (i + 1) % 3 == 0;
            int correspondingControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
            int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;

            if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count)
            {
                float dst = (points[anchorIndex] - points[correspondingControlIndex]).magnitude;
                Vector2 dir = (points[anchorIndex] - pos).normalized;
                points[correspondingControlIndex] = points[anchorIndex] + dir * dst;
            }
        }
    }*/

}