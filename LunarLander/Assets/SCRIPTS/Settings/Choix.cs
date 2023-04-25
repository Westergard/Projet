using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Choix : MonoBehaviour
{
    public TMPro.TMP_Dropdown m_Dropdown;
    public TMPro.TMP_Dropdown m_Level;
    public Toggle t;

    void Start()
    {
        int choixScene = (PlayerPrefs.GetInt("Scene")); 
        int level = (PlayerPrefs.GetInt("Level"));
        m_Dropdown.value = choixScene;
        m_Level.value = level-1;


    }
}
