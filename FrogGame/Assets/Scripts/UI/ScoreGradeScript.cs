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
    TextMeshProUGUI score;
    int _temporalScore = 0;
    int _totalStars = 0;
    public int TotalStars {get => _totalStars; }

    [SerializeField]
    SpawnCreatures totalNumberOfCreatures;
    int _creaturesCaught = 0;

    // Start is called before the first frame update
    private void Awake() {
        score.text = _creaturesCaught.ToString();
    }
    private void Start() {
        EventSystem.current.onAddingPoints += AddingPoints; 
        slider.maxValue = totalNumberOfCreatures.GetTotalNumberOfCreaturesInTheLevel();
        _totalStars = 0;
        score.text = _creaturesCaught.ToString()+"/"+slider.maxValue.ToString();
    }

    //This method is called every time an insect is destroyed by the frog
    void AddingPoints(){
        _creaturesCaught += 1;
        float percentage = (float)System.Math.Round(_creaturesCaught / slider.maxValue, 1);
        if (percentage >= PERCENTAGE_FOR_STARS[2])
        {
            _totalStars = 3;
        }else if (percentage >= PERCENTAGE_FOR_STARS[1])
        {
            _totalStars = 2;
        }else if (percentage >= PERCENTAGE_FOR_STARS[0])
        {
            _totalStars = 1;
        }
        else
        {
            _totalStars = 0;
        }

    }

    //Method returns the string to show how many insects were caught and how many were in total
    public string GetTotalCreaturesCaught(){
        return _creaturesCaught.ToString()+"/"+slider.maxValue.ToString();
    }

    void Update()
    {
        if(_temporalScore != _creaturesCaught){

            slider.value = _creaturesCaught;

            _temporalScore = _creaturesCaught;

            score.text = _creaturesCaught.ToString()+"/"+slider.maxValue.ToString();
        }
    }
}
