using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ScoreGradeScript : MonoBehaviour
{
    [SerializeField]
    Slider slider; 

    [SerializeField]
    TextMeshProUGUI[] stars;
    

    [SerializeField]
    TextMeshProUGUI score;
    int temporalScore = 0;

    int starts = 0;

    [SerializeField]
    SpawnCreatures totalNumberOfCreatures;
    // Start is called before the first frame update
    private void Awake() {
        score.text = GameManager.creaturesCought.ToString();
        slider.maxValue = totalNumberOfCreatures.getTotalNumberOfCreaturesInTheLevel();

    }

    private void Start() {
        starts = 0;
        score.text = GameManager.creaturesCought.ToString()+"/"+slider.maxValue.ToString();
        stars[0].color = new Color(255,255,255,0.2f);
        stars[1].color = new Color(255,255,255,0.2f);
        stars[2].color = new Color(255,255,255,0.2f);
    }
    public int getTotalStars(){
        return stars.Length;
    }
    public int getStarts(){
        int totalStars = 0;
        for(int i = 0; i < stars.Length; i++ ){
            if(stars[i].color == new Color(255,255,255,1)){
                totalStars++;
            }
        }
        return totalStars;
    }

    public string getTotalCreaturesCought(){
        return GameManager.creaturesCought.ToString()+"/"+slider.maxValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(temporalScore != GameManager.creaturesCought){

            slider.value = GameManager.creaturesCought;

            temporalScore = GameManager.creaturesCought;

            score.text = GameManager.creaturesCought.ToString()+"/"+slider.maxValue.ToString();

            if(slider.value >= slider.maxValue*0.58f){
                stars[0].color = new Color(255,255,255,1);
            }
            else{
                stars[0].color = new Color(255,255,255,0.2f);
            }

            if(slider.value >= slider.maxValue*0.8f){
                stars[1].color = new Color(255,255,255,1);
            }else{
                stars[1].color = new Color(255,255,255,0.2f);
            }

            if(slider.value >= slider.maxValue*1f){
                stars[2].color = new Color(255,255,255,1);
            }else{
                stars[2].color = new Color(255,255,255,0.2f);
            }

        }
    }
}
