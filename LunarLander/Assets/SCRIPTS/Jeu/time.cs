using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour
{
    public Text htime;

    void Start()
    {
        htime.text = PlayerPrefs.GetInt("high time").ToString();
    }
}
