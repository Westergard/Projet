using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Choix : MonoBehaviour
{
    public TMPro.TMP_Dropdown m_Dropdown;

    void Start()
    {
        int choixScene = (PlayerPrefs.GetInt("Scene")); 

        m_Dropdown.value = choixScene;

    }
}
