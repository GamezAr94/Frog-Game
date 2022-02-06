using System.Collections;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI countDownText; 

    [SerializeField][Tooltip("This variable controlls the countdown before the start the game")]
    float timerToStartTheGame = 3f;

    IEnumerator counterToStartGame;
    
    private void Awake() {
        counterToStartGame = CountDownText(timerToStartTheGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.onStartingGame += StartingGame;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartingGame(){
        StartCoroutine(counterToStartGame);
    }

    public IEnumerator CountDownText(float time){
        countDownText.enabled = true;
        while(time >= 0){
            if(time > 0){
                countDownText.text = ((int)time).ToString();
            }else{
                countDownText.text = "Go!";
            }
            time -= 1;
            yield return new WaitForSeconds(1f);
        }
        countDownText.enabled = false;

        EventSystem.current.ParallaxEffect(true);
        EventSystem.current.StartingSpawnCreatures();
        
        StopCoroutine(counterToStartGame);
        yield return null;
    }
}
