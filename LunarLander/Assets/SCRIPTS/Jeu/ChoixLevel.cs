using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoixLevel : MonoBehaviour
{
    public TMPro.TMP_Dropdown m_Level;

    void Start()
    {
        int choixLevel = (PlayerPrefs.GetInt("Level"));

        m_Level.value = choixLevel;

    }
}
