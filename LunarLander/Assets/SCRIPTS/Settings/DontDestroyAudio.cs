using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAudio : MonoBehaviour
{
    public static DontDestroyAudio instance;
    public AudioSource audio;

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Jeu Perlin")
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
