using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class ChangementScene : MonoBehaviour
{
    public TMPro.TMP_Dropdown m_Dropdown;
    public TMPro.TMP_Dropdown m_level;
    public Slider m_SliderVolumePincipale;
    public Slider m_SliderEffetSonore;
    public Slider m_SliderMusique;

    void Start()
    {
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
        PlayerPrefs.SetInt("Level", m_level.value);


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
