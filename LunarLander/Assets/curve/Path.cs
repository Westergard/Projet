using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path {

    [SerializeField, HideInInspector]
    List<Vector2> points;

    public Path(Vector2 centre)
    {
       
        points = new List<Vector2> // les premier 4 point forme un segement et les trois prochain en forme un autre en utilisent le dernier point du dernier segement comme premier point d'ancrage, donc il on un point d'ancrage en commun. 
        {
            //premier point d'ancrage
            centre+Vector2.left,
            //premier point de contrôle
            centre+(Vector2.left+Vector2.up),
            //deuxième point de contrôle
            centre + (Vector2.right * Random.Range(1,9) * .25f + Vector2.down * Random.Range(-8,8)*.25f), // mettre une pente aléatoire
            //deuxième point d'ancrage
            centre + (Vector2.right * 2) // pas de hauteur aléatoire pour que le premier et dernier point est la même hauteur, pour pas que le vaissaut touche la courbe lorsqu'il se teleporte d'un côté a lautre de la map.
        };
        for (int i = 2; i <= 6; i++)
        {
            AddSegment(centre + Vector2.right * i * 3 + Vector2.down * Random.Range(-12,8) * .055f );
        }
        AddSegment(centre + Vector2.right * 22); // un dernier point a la même hauteur que le premier point. 
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
            return (points.Count - 4) / 3 + 1; //  le premier segement a 4 point, les autre en on troi de plus. 
        }
    }

    public void AddSegment(Vector2 NextAnchorPoint)
    {
        //le premier point d'ancrage est le deuxième point d'ancrage du segement précédant

        //premier point de contrôle
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]); // le premier point de contrôle devrai avoir la même pende et distance que le dernier point de contrôle pour avoir une courbe lisse. 
        //deuxième point de contrôle
        points.Add(NextAnchorPoint + Vector2.left * Random.Range(1,9) * .1f + Vector2.down * Random.Range(-12,12) * .1f ); // mettre un point de contrôle qui va donner la courbe une pent aléatoire mais pas trop intense.  
        //deuxième point d'ancrage
        points.Add(NextAnchorPoint);
    }
    public void AddSegmentCustom(Vector2 NextAnchorPoint, Vector2 ControlePoint) // même choes que AddSegment() mais avec un point de contrôle personaliser
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add(NextAnchorPoint + ControlePoint); // point de contrôle personaliser
        points.Add(NextAnchorPoint);
    }

    public Vector2[] GetPointsInSegment(int i)
    {
        return new Vector2[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[i * 3 + 3] };
    }

}