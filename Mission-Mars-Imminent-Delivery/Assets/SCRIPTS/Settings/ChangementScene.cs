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

    //Fonction qui s'exécute lorsque l'on pèse sur le bouton start
    public void ClickStart()
    {
        //Charge la bonne scène
        if (PlayerPrefs.GetInt("Scene") == 0)
        {
            SceneManager.LoadScene("Jeu Perlin");
        }
        else
        {
            SceneManager.LoadScene("Jeu Bézier");
        }
        
    }

    //Fonction qui s'exécute lorsque l'on pèse sur le bouton settings
    public void ClickSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    //Fonction qui s'exécute lorsque l'on pèse sur le bouton pour revenir au menu principale
    public void ClickBackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Fonction qui s'exécute lorsque l'on pèse sur le bouton save des settings
    public void ClickSaveSettings()
    {
        //Sauvegarde les données dans des variables globales
        PlayerPrefs.SetInt("Scene", m_Dropdown.value);
        PlayerPrefs.SetFloat("VolumePrincipale", m_SliderVolumePincipale.value);
        PlayerPrefs.SetFloat("EffetSonore", m_SliderEffetSonore.value);
        PlayerPrefs.SetFloat("Musique", m_SliderMusique.value);
        
        if(t.isOn)
        {
            PlayerPrefs.SetInt("high score", 0);
            PlayerPrefs.SetFloat("high time", 0);
        }
        if (SecretToggle.isOn)
        {
            PlayerPrefs.SetInt("Level", 4);
        }
        if(!SecretToggle.isOn)
        {
            PlayerPrefs.SetInt("Level", m_level.value + 1);
        }

        SceneManager.LoadScene("MainMenu");
    }

    //Fonction qui s'exécute lorsque l'on pèse sur le bouton caché sur la barre du haut du E de SETTINGS
    public void ClickSecretButton()
    {
        SecretToggle.gameObject.SetActive(true);
        MessageSecret.gameObject.SetActive(true);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        MessageSecret.gameObject.SetActive(true);
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
