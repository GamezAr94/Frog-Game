using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongPowerLine : MonoBehaviour
{
    [SerializeField] private GameObject tongTargetPositionGameObject;
    private Vector3 _startLocalTouchPosition;
    private Vector3 _finalLocalTouchPosition;
    private float distance;
    LineRenderer renderLine;

    private void Awake()
    {
        renderLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                _startLocalTouchPosition = GetThePositionOfTheTouch(touch);                
                SetDrawPosition(2, tongTargetPositionGameObject.transform.position.x, tongTargetPositionGameObject.transform.position.y);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                _finalLocalTouchPosition = GetThePositionOfTheTouch(touch);
                distance = Vector3.Distance(_startLocalTouchPosition, _finalLocalTouchPosition);

                tongTargetPositionGameObject.transform.localPosition = new Vector3(0, distance, 0);
                SetDrawPosition(1, tongTargetPositionGameObject.transform.position.x, tongTargetPositionGameObject.transform.position.y);

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                SetDrawPosition();
                tongTargetPositionGameObject.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
    
    Vector3 GetThePositionOfTheTouch(Touch touch){
        Vector3 positionTouch = Camera.main.ScreenToWorldPoint(touch.position);
        positionTouch.z = 0f;
        return positionTouch;
    }

    private void SetDrawPosition(int lineNum = 2, float positionX = 0, float positionY = 0)
    {
        if (lineNum == 2)
        {
            renderLine.SetPosition(0, new Vector3(positionX, positionY, 0f));
            renderLine.SetPosition(1, new Vector3(positionX, positionY, 0f));
        }
        else if (lineNum == 0)
        {
            renderLine.SetPosition(lineNum, new Vector3(positionX, positionY, 0f));
        }
        else if (lineNum == 1)
        {
            renderLine.SetPosition(lineNum, new Vector3(positionX, positionY, 0f));
        }
        else
        {
            Debug.LogError("Wrong LineNum Input. Input must be a number between 0 and 2 inclusive. Your Input = " + lineNum);
        }
    }
}
