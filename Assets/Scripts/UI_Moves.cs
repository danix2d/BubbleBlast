using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UI_Moves : MonoBehaviour
{
    public IntVariable moves;
    public TMP_Text movesTxt;

    private void OnEnable() {
        UpdateScore();
    }
    
    public void UpdateScore()
    {
        movesTxt.text = moves.Value.ToString();
    }
}
