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
    public TMPro.TMP_Dropdown m_levelSecret;
    public Slider m_SliderVolumePincipale;
    public Slider m_SliderEffetSonore;
    public Slider m_SliderMusique;
    public Toggle t;
    public Toggle SecretToggle;
    public Text MessageSecret;

    TMPro.TMP_Dropdown m_levelSecret1;

    void Start()
    {
        SecretToggle.gameObject.SetActive(false);
        MessageSecret.gameObject.SetActive(false);
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
        
        if(t.isOn)
        {
            PlayerPrefs.SetInt("high score", 0);
            PlayerPrefs.SetFloat("high time", 0);
        }
        if (t.isOn)
        {
            PlayerPrefs.SetInt("Level", 4);
        }else
        {
            PlayerPrefs.SetInt("Level", m_level.value + 1);
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void ClickSecretButton()
    {
        SecretToggle.gameObject.SetActive(true);
        MessageSecret.gameObject.SetActive(true);
        IEnumerator CoUpdate()
        {
            MessageSecret.gameObject.SetActive(false);

            yield return null;
        }
        
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
