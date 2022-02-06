using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ScoreGradeScript : MonoBehaviour
{
    [SerializeField][Tooltip("The percentage necessary to get the first second and third star")][Range(0, 1)]
    float[] PERCENTAGE_FOR_STARS;

    [SerializeField]
    Slider slider; 

    [SerializeField]
    Image[] stars;
    

    [SerializeField]
    TextMeshProUGUI score;
    int temporalScore = 0;
    int totalStars = 0;
    public int TotalStars {get => totalStars; }

    [SerializeField]
    SpawnCreatures totalNumberOfCreatures;
    int creaturesCought = 0;

    // Start is called before the first frame update
    private void Awake() {
        score.text = creaturesCought.ToString();
        slider.maxValue = totalNumberOfCreatures.GetTotalNumberOfCreaturesInTheLevel();
        EventSystem.current.onAddingPoints += AddingPoints; 
    }
    private void Start() {
        totalStars = 0;
        score.text = creaturesCought.ToString()+"/"+slider.maxValue.ToString();
        stars[0].color = new Color(255,255,255,0.2f);
        stars[1].color = new Color(255,255,255,0.2f);
        stars[2].color = new Color(255,255,255,0.2f);
    }

    //This method is called every time an insect is destroyed by the frog
    void AddingPoints(){
        creaturesCought += 1; 
    } 

    //method to return the total number of stars to collect (it must always be 3)
    public int getTotalStars(){
        return stars.Length;
    }

    //Method returns the string to show how many insects were cought and how many were in total
    public string getTotalCreaturesCought(){
        return creaturesCought.ToString()+"/"+slider.maxValue.ToString();
    }

    void Update()
    {
        if(temporalScore != creaturesCought){

            slider.value = creaturesCought;

            temporalScore = creaturesCought;

            score.text = creaturesCought.ToString()+"/"+slider.maxValue.ToString();

            if(slider.value >= slider.maxValue * PERCENTAGE_FOR_STARS[0] && totalStars < 1){
                stars[0].color = new Color(255,255,255,1);
                totalStars++;
            }
            if(slider.value >= slider.maxValue * PERCENTAGE_FOR_STARS[1] && totalStars < 2){
                stars[1].color = new Color(255,255,255,1);
                totalStars++;
            }
            if(slider.value >= slider.maxValue * PERCENTAGE_FOR_STARS[2] && totalStars < 3){
                stars[2].color = new Color(255,255,255,1);
                totalStars++;
            }
        }
    }
}
