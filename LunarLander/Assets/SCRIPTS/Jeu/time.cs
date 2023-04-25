using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour
{
    public Text htime;

    void Start()
    {
        float timeToDisplay = PlayerPrefs.GetFloat("high time");

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        htime.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);

    }
}
