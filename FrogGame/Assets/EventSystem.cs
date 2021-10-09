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

    public event Action onSwipeTouch;

    public void swipeTouch(){
        if(onSwipeTouch != null){
            onSwipeTouch();
        }
    }
}
