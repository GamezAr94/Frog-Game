using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    TextMeshProUGUI _countDownText; 

    [SerializeField][Tooltip("This variable controlls the countdown before the start the game")]
    float timerToStartTheGame = 3f;

    IEnumerator counterToStartGame;

    [Tooltip("GameObject with text mesh pro to display the counter")]
    [SerializeField] private GameObject counterText;
    
    private void Awake()
    {
        _countDownText = counterText.GetComponent<TextMeshProUGUI>();
        EventSystem.current.onStartingGame += StartingGame;
    }

    public void StartingGame(){
        counterToStartGame = CountDownText(timerToStartTheGame);
        StartCoroutine(counterToStartGame);
    }

    private void OnDestroy()
    {
        StopCoroutine(counterToStartGame);
    }

    public IEnumerator CountDownText(float time)
    {
        yield return new WaitForSeconds(.2f);
        
        counterText.SetActive(true);
        
        while(time >= 0){
            if(time > 0){
                _countDownText.text = ((int)time).ToString();
            }else{
                _countDownText.text = "Go!";
            }
            time -= 1;
            yield return new WaitForSeconds(1f);
        }
        counterText.SetActive(false);

        //EventSystem.current.ParallaxEffect(true);
        EventSystem.current.StartMovingTheGame();
        
        StopCoroutine(counterToStartGame);
        yield return null;
    }
}
