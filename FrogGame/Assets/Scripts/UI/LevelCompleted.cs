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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showingEndingUIScreen(){
        goldScoreText.text = goldStats.getTotalGoldCought().ToString();
        creaturesScoreText.text = scoreStats.getTotalCreaturesCought();
        int starsCollected = scoreStats.getStarts();
        for(int i = 0; i < starsCollected; i++){
            stars[i].color = new Color(255,255,255,1);
        }
        if(starsCollected <= 1){
            NextLevelButton.interactable = false;
        }else{
            NextLevelButton.interactable = true;
        }
        UILevelCompleted.SetActive(true);
        Time.timeScale = 0;
    } 
}
