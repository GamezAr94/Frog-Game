using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;

    void Awake(){
        current = this;
    }
    private void Start() {
        StartingGame();//this code starts the countdown, the spawning process and the parallax effect
    }
    
    private void Update()
    {
        if(Time.timeScale != 0){
            swipeTouch();
        }
    }

    public event Action onSwipeTouch;
    public event Action<float,float> onMovingFrogSideToSide;
    public event Action<Vector3> onSettingHeadsRotation;
    public event Action<Vector3> onBodyTongFollowingTong;
    public event Action<int> onAddingPoints;
    public event Action<int> onSettingCombo;
    public event Action onSettingStamina;
    public event Action onStartingSpawnCreatures;
    public event Action<bool> onParallaxEffect;
    public event Action onStartingGame;

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

    public void BodyTongFOllowingTong(Vector3 tongPosition){
        if(onBodyTongFollowingTong != null){
            onBodyTongFollowingTong(tongPosition);
        }
    }

    public void AddingPoints(int points){
        if(onAddingPoints != null){
            onAddingPoints(points);
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

    public void StartingSpawnCreatures(){
        if(onStartingSpawnCreatures != null){
            onStartingSpawnCreatures();
        }
    }
    public void ParallaxEffect(bool isStarting){
        if(onParallaxEffect != null){
            onParallaxEffect(isStarting);
        }
    }
    public void StartingGame(){
        if(onStartingGame != null){
            onStartingGame();
        }
    }
    
}
