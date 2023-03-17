using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumePrincipale : MonoBehaviour
{
    public Slider m_Slider;

    void Start()
    {
        float value = (PlayerPrefs.GetFloat("VolumePrincipale"));

        m_Slider.value = value;
    }

    void Update()
    {
        
    }
}
