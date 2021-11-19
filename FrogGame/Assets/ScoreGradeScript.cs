using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGradeScript : MonoBehaviour
{
    [SerializeField]
    Slider slider; 

    [SerializeField]
    TextMeshProUGUI[] stars;

    [SerializeField]
    TextMeshProUGUI score;
    int temporalScore = 0;

    // Start is called before the first frame update
    private void Awake() {

        score.text = GameManager.score.ToString();


    }

    private void Start() {
                stars[0].color = new Color(255,255,255,1);
                stars[1].color = new Color(255,255,255,1);
                stars[2].color = new Color(255,255,255,1);
    }

    // Update is called once per frame
    void Update()
    {
        if(temporalScore != GameManager.score){

            slider.value = GameManager.score;

            temporalScore = GameManager.score;

            score.text = GameManager.score.ToString();

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
