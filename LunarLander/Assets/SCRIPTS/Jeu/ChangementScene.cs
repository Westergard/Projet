using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class ChangementScene : MonoBehaviour
{
    public TMPro.TMP_Dropdown m_Dropdown;
    public Slider m_SliderVolumePincipale;
    public Slider m_SliderEffetSonore;
    public Slider m_SliderMusique;
    public AudioSource bgMusic;
    public AudioSource soundEffects;

    void Start()
    {
        bgMusic = GameObject.Find("BackrgroundMusique").GetComponent<AudioSource>();
        soundEffects = GameObject.Find("Sound effect").GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    public void ClickStart()
    {
        if(PlayerPrefs.GetInt("Scene") == 0)
        {
            SceneManager.LoadScene("Jeu Perlin");
        }
        else
        {
            SceneManager.LoadScene("Jeu Bézier");
        }
        
    }

    public void ClickSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ClickBackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ClickSaveSettings()
    {
        PlayerPrefs.SetInt("Scene", m_Dropdown.value);
        PlayerPrefs.SetFloat("VolumePrincipale", m_SliderVolumePincipale.value);
        PlayerPrefs.SetFloat("EffetSonore", m_SliderEffetSonore.value);
        PlayerPrefs.SetFloat("Musique", m_SliderMusique.value);

        bgMusic.volume = (float)((m_SliderVolumePincipale.value * m_SliderMusique.value) * 0.5);
        soundEffects.volume = (float)((m_SliderVolumePincipale.value * m_SliderEffetSonore.value) * 0.5);


        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
