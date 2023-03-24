using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPerlin : MonoBehaviour
{
    public AudioSource musique;
    public AudioSource SE;

    void Start()
    {
        float vP = PlayerPrefs.GetFloat("VolumePrincipale");
        float m = PlayerPrefs.GetFloat("Musique");
        float se = PlayerPrefs.GetFloat("EffetSonore");

        musique.volume = 0.5f * m * vP;
        SE.volume = 1 * se * vP;
    }
}
