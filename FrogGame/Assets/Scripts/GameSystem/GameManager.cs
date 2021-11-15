using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    int score = 0;
    int maxCombo = 0;
    int numOfTries = 0;
    int totalTries = 0;
    [SerializeField]
    Text textScore;

    [SerializeField]
    Text textTries;


    private void Awake()
    {
        textScore.text = score.ToString();
        textTries.text = totalTries.ToString();
    }

    public void UpdateScore(int combo, int points, int tries)
    {
        numOfTries = tries;
        if (maxCombo < combo)
        {
            maxCombo = combo;
        }
        score += points * combo;
        totalTries = tries;
        textScore.text = score.ToString();
        textTries.text = tries.ToString();
    }
    void AddingPoints(){

    }
}
