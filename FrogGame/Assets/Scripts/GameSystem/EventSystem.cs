using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;
    private bool isAllowedToMove;

    void Awake(){
        current = this;
    }
    private void Start() {
        StartingGame();//this code starts the countdown, the spawning process and the parallax effect
    }
    
    private void Update()
    {
        if(isAllowedToMove){
            swipeTouch(); // this event controls all the behaviour of the frog, the user input, the spawning thong and the frog's movement.
        }
    }

    public event Action onSwipeTouch;
    public event Action<float,float> onMovingFrogSideToSide;
    public event Action<Vector3> onSettingHeadsRotation;
    public event Action onAddingPoints;
    public event Action<int> onAddingGold;
    public event Action<int> onSettingCombo;
    public event Action onSettingStamina;
    public event Action onStartMovingTheGame; //event that controls the behaviour when the countdown reaches zero and the games begins (car animation, parallax, spawn creatures)
    public event Action onStartingGame;
    public event Action<bool> onEndingGame;
    public event Action onCompletingLevel;

//Event where the PlayerAction and LineScreenDraw script are subscribed to show the line of the users input, to rotate the frogs head and to spawn the tong
    public void swipeTouch(){
        if(onSwipeTouch != null){
            onSwipeTouch();
        }
    }

    public void MovingFrogSideToSide(float startingPoint, float endingPoint){
        if(onMovingFrogSideToSide != null){
            onMovingFrogSideToSide(startingPoint, endingPoint);
        }
    }

    public void SettingHeadsRotation(Vector3 lookingAt){
        if(onSettingHeadsRotation != null){
            onSettingHeadsRotation(lookingAt);
        }
    }

    public void AddingPoints(){
        if(onAddingPoints != null){
            onAddingPoints();
        }
    }

    public void AddingGold(int gold){
        if(onAddingGold != null){
            onAddingGold(gold);
        }
    }

    public void SettingCombo(int combo){
        if(onSettingCombo != null){
            onSettingCombo(combo);
        }
    }

    public void SettingStamina(){
        if(onSettingStamina != null){
            onSettingStamina();
        }
    }

    public void StartMovingTheGame(){
        if(onStartMovingTheGame != null){
            onStartMovingTheGame();
            isAllowedToMove = true;
        }
    }
    public void StartingGame(){
        if(onStartingGame != null){
            onStartingGame();
        }
    }
    public void EndingGame(bool isEnding){
        if(onEndingGame != null){
            onEndingGame(isEnding);
        }
    }
    public void CompletingGame(){
        if(onCompletingLevel != null){
            onCompletingLevel();
        }
    }
}
