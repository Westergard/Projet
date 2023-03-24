using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeMenu : MonoBehaviour
{
    public AudioSource SE;

    void Start()
    {
        float vP = PlayerPrefs.GetFloat("VolumePrincipale");
        float m = PlayerPrefs.GetFloat("Musique");
        float se = PlayerPrefs.GetFloat("EffetSonore");
        SE.volume =  se * vP;
    }
    void Update()
    {
        float vP = PlayerPrefs.GetFloat("VolumePrincipale");
        float m = PlayerPrefs.GetFloat("Musique");
        PlayerPrefs.SetFloat("BGM", 0.5f * m * vP);
    }
}
