using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    int maxCombo = 0;

    [SerializeField]
    Text textTries;


    private void Awake()
    {
        EventSystem.current.onAddingPoints += AddingPoints;
        EventSystem.current.onSettingCombo += Combo; // Uncoment to accept combos
    }

    //Not sure if keep the combo feature, if I decided to take it out i have to delete the maxCombo, the event and the reference in the tongbehaviour
    public void Combo(int combo)
    {
        if(combo > 0){
            maxCombo++;
        }else{
            maxCombo = 0;
        }
    }
    void AddingPoints(int points){
        score += points; 
    } 
}
