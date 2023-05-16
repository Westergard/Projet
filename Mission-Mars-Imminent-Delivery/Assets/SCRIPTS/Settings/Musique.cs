using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Musique : MonoBehaviour
{
    public Slider m_Slider;

    void Start()
    {
        float value = (PlayerPrefs.GetFloat("Musique"));

        m_Slider.value = value;
    }

    void Update()
    {

    }
}
