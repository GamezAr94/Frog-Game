using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;

    void Awake(){
        current = this;
    }
    
    private void Update()
    {
        swipeTouch();
    }

    public event Action onSwipeTouch;
    public event Action<float,float> onMovingFrogSideToSide;
    public event Action<Vector3> onSettingHeadsRotation;
    public event Action<Vector3> onBodyTongFollowingTong;

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
}
