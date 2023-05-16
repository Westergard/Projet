using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPerlin : MonoBehaviour
{
    public AudioSource musique;
    public AudioSource Vaisseau;
    public AudioSource Tourelle;

    //Fonction qui s'exécute pour régler le son selon les paramètres choisis par le joueur
    void Start()
    {
        float vP = PlayerPrefs.GetFloat("VolumePrincipale");
        float m = PlayerPrefs.GetFloat("Musique");
        float se = PlayerPrefs.GetFloat("EffetSonore");

        musique.volume = 0.5f * m * vP;
        Vaisseau.volume = 1 * se * vP;
        Tourelle.volume = 1 * se * vP;
    }
}
