using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAudio : MonoBehaviour
{
    public static DontDestroyAudio instance;
    public AudioSource audio;

    //Fonction qui vérifie la scène actuelle pour arrêter la musique lorsqu'on est rendu dans le jeu
    void Update()
    {
        audio.volume = PlayerPrefs.GetFloat("BGM");

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Jeu Perlin" || currentScene == "Jeu Bézier")
        {
            Destroy(this.gameObject);
            return;
        }
    }
    void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (instance == null && currentScene != "Jeu Perlin")
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        
            DontDestroyOnLoad(this.gameObject);
    }
}
