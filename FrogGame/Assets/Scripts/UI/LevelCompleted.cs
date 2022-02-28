using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelCompleted : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField]
    ScoreGradeScript scoreStats;//Script to retrieve the number of stars collected at the end of the game
    
    [SerializeField]
    GoldScript goldStats;//Script to retrieve the total number of gold collected at the end of the game

    [Header("UI References")]
    [SerializeField]
    TextMeshProUGUI goldScoreText;//Text mesh to display the gold score
    [SerializeField]
    TextMeshProUGUI creaturesScoreText;
    [SerializeField]
    TextMeshProUGUI numberOfLevel;

    [FormerlySerializedAs("NextLevelButton")] [SerializeField]
    Button nextLevelButton;

    [SerializeField]
    GameObject[] stars;

    [SerializeField]
    GameObject uiLevelCompleted;
    
    [SerializeField]
    GameObject uiPause;
    
    [SerializeField]
    GameObject uiStats;
    
    [SerializeField] private GameObject content;
    
    [SerializeField] private GameObject background;
    
    [Header("Animations")]
    
    [SerializeField] LeanTweenType easeType;
    
    [SerializeField][Range(0f,1f)] float containerDuration, starsDuration, containerDelay, starsDelay;

    private void Awake() {
        uiLevelCompleted.SetActive(false);
        uiPause.SetActive(true);
        uiStats.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stars.Length; i++)//Hiding the stars at the beginning of the game
        {
            stars[i].transform.localScale = new Vector3(0, 0, 0);
        }
        content.transform.localScale = new Vector3(0,0,0);
        
        nextLevelButton.interactable = false;
        numberOfLevel.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        EventSystem.current.onCompletingLevel += ShowingEndingUIScreen;
    }

    //Function to activate the Winning UI screen and its component, refresh the stats info, and activate/desactivate the NEXT level button
    void ShowingEndingUIScreen(){
        
        uiLevelCompleted.SetActive(true);
        uiPause.SetActive(false);
        uiStats.SetActive(false);
        
        goldScoreText.text = goldStats.getTotalGoldCought().ToString();
        creaturesScoreText.text = scoreStats.GetTotalCreaturesCaught();
        
        int starsCollected = scoreStats.TotalStars;
        nextLevelButton.interactable = starsCollected > 1;
        
        Time.timeScale = 0.1f;
        
        ShowingWinningUI();
    } 
    
    //Function to animate the Winning Screen UI and its content
    void ShowingWinningUI()
    {
        LeanTween.scale(content, new Vector3(1,1,1), containerDuration).setDelay(containerDelay).setEase(easeType).setIgnoreTimeScale(true);
        for (int i = 0; i < scoreStats.TotalStars; i++)
        {
            LeanTween.scale(stars[i], new Vector3(1,1,1), starsDuration).setDelay(starsDelay+((float)i/2)).setEase(easeType).setIgnoreTimeScale(true);
        }
    }
}
