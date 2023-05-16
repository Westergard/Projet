using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscore : MonoBehaviour
{
    public Text score;
    
    void Start()
    {
        score.text = PlayerPrefs.GetInt("high score").ToString();
    }
}
