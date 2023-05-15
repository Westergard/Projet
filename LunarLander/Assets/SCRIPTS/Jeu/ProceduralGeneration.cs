using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{

    //Ces variables seront modifiées dans unity pour nous permettre d’avoir le terrain désiré avec la bonne hauteur, largeur et type de terrain(roche ou terre (j’ai décidé de ne pas mettre d’herbe car nous sommes sur mars)
    [SerializeField] int width,height;
    [SerializeField] int minStoneheight, maxStoneHeight;

    //Chaque point de la grille sera défini par un tile, qui est une texture 2d créant donc un effet rétro avec des terrains carrés(pas lisse)
    [SerializeField] GameObject dirt,grass,stone;

    public GameObject RandomTile;

    //Dès le début de la partie, je génère le terrain
    void Start()
    {
        Generation();
    }

    void Generation()
    {
        for (int x = 0; x < width; x++)//Prendre tous les points sur l’axe des x
        {
            // les variables suivantes servent à définir une hauteur maximale et minimale selon la hauteur prescrit et ensuite définir la hauteur finale avec un random range
            int minHeight = height - 1;
            int maxHeight = height + 2;
            height = Random.Range(minHeight, maxHeight);

            //Les stoneheights sont seulement présent pour l'esthétique du terrain
            int minStoneSpawnDistance = height - minStoneheight;
            int maxStoneSpawnDistance = height - maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);

            /*Les fonctions suivantes sont la définition du bruit de perlin. Nous utilisons une fonction (description plus loin) pour définir les vecteurs et nous utilisons les boucles for pour toutes les jumeler ensemble, créant donc le terrain avec le bruit de perlin. Le int y = 59 est le minimum qui s’assure que le terrain de va pas totalement en dessous de la caméra.Il commencera par faire apparaitre la roche et mettra la terre par dessus par la suite*/
            for (int y = 59; y < height; y++)//Prendre tous les points sur l’axe des y
            {
                if (y < totalStoneSpawnDistance)
                {
                    spawnObj(stone, x, y);
                }
                else
                {
                    spawnObj(dirt, x, y);
                }
               
            }
            if(totalStoneSpawnDistance == height)
            {
                spawnObj(stone, x, height);
            }
            else
            {
                spawnObj(grass, x, height);
            }
            
        }
    }

    /*Ceci est la fonction permettant de définir les vecteurs de chaque point. La première ligne prend la hauteur et la largeur prisent aléatoirement et utilise le quaternion pour définir que cet objet ne fera pas de rotation (parfaitement aligné avec les axes). Pour la seconde ligne, le script s’assure que peut importe ce qui est généré, l’objet restera l’enfant du gameobject de la génération de terrain*/
    void spawnObj(GameObject obj,int width,int height)//What ever we spawn will be a child of our procedural generation gameObj
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}
