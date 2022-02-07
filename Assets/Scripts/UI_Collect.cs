using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UI_Collect : MonoBehaviour
{
    public BubbleStats stats;
    public TMP_Text statsTxt;
    
    private int score;

    private void OnEnable() {
        UpdateScore();
    }

    public void UpdateScore()
    {
        score = stats.toColect;

        score = score <= 0 ? 0 : score;

        statsTxt.text = score.ToString();
    }
}
