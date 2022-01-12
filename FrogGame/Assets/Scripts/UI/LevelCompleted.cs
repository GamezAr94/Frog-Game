using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField]
    ScoreGradeScript scoreStats;
    
    [SerializeField]
    GoldScript goldStats;

    [SerializeField]
    TextMeshProUGUI goldScoreText;

    [SerializeField]
    Button NextLevelButton;

    [SerializeField]
    TextMeshProUGUI creaturesScoreText;
    
    [SerializeField]
    Image[] stars;

    [SerializeField]
    TextMeshProUGUI numberOfLevel; 

    [SerializeField]
    GameObject UILevelCompleted; 

    private void Awake() {
        UILevelCompleted.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        NextLevelButton.interactable = false;
        numberOfLevel.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        for(int i = 0; i < scoreStats.getTotalStars(); i++){
            stars[i].color = new Color(255,255,255,0f);
        }
        EventSystem.current.onCompletingLevel += showingEndingUIScreen;
    }

    public void showingEndingUIScreen(){
        //Convert this to a coroutine so when the level is completed the UI wont appear right away and it will let the ui to refresh the stats
        goldScoreText.text = goldStats.getTotalGoldCought().ToString();
        creaturesScoreText.text = scoreStats.getTotalCreaturesCought();
        int starsCollected = scoreStats.TotalStars;
        if(starsCollected <= 1){
            NextLevelButton.interactable = false;
        }else{
            NextLevelButton.interactable = true;
        }
        for(int i = 0; i < starsCollected; i++){
            stars[i].color = new Color(255,255,255,1);
        }
        UILevelCompleted.SetActive(true);
        Time.timeScale = 0.1f;
    } 
}
