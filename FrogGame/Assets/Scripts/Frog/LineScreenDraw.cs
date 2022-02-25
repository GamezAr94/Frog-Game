using UnityEngine;
using System;
using Unity.Burst.Intrinsics;

public class LineScreenDraw : MonoBehaviour
{
    [Header("Line Rendering Settings")]

    [SerializeField][Range (0f, 5f)]
    [Tooltip("Float that indicates the minimum distance between the starting and ending user touch input required to spawn the tong")]
    float minDistanceToSpawnTong = 2.0f;
    
    [SerializeField][Range (0f, 5f)]
    [Tooltip("Float that indicates the minimum distance in the X axis starting and ending the user touch input required to move the frog")]
    float minDistanceToMoveFrogX = 1.77f;

    private float _distance;

    [SerializeField]
    TongBehaviour tongBehaviour;

    [Header("Line Renderer")]
    
    [Tooltip("Target point where the tong will be spawned")]
    [SerializeField] private GameObject tongTargetPositionGameObject;

    private Vector3 _tongTargetStartingPoint;

    private Vector3 _startLocalTouchPosition; //Position of the first point of the line renderer
    
    private Vector3 _trackLocalTouchPosition; //Position of the touch in movement 

    private Vector3 _endingLocalTouchPosition; //Position of the end point of the line renderer

    LineRenderer _renderLine;

    [Header("Debuger Tools")]

    [Tooltip("Game object that helps to visualize the position of the users touch input")]
    public GameObject startTargetPoint;
    [Tooltip("Game object that helps to visualize the position of the users touch exit")]
    public GameObject endTargetPoint;

    bool _isReadyToAcceptInput = true; //avoid the user entering an input before the tong comes back to its mouth

    private void Awake()
    {
        _renderLine = this.GetComponent<LineRenderer>();
        _renderLine.positionCount = 2;

        startTargetPoint.transform.position = _startLocalTouchPosition;//Debugger object
        endTargetPoint.transform.position = _endingLocalTouchPosition;//Debugger object
                        
        SetDrawPosition(this.transform.position);

        EventSystem.current.onSwipeTouch += PositionUserTouch;
    }

//Function to retrieve the user input, the position of the first touch, the swipe of the input and the position of the end of the touch
//this function is the logic to move the frog in the right direction and spawn the tong
//this function has a debugging system where two objects are placed where the user press and releases its input
//also this function draw a line to visualize the user input
    private void PositionUserTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                _startLocalTouchPosition = GetThePositionOfTheTouch(touch);

                DebugingContactPoints(startTargetPoint, _startLocalTouchPosition);

                SetDrawPosition(tongTargetPositionGameObject.transform.position);
                    
                if(tongBehaviour.TongInMouth){
                    _isReadyToAcceptInput = true;

                    EventSystem.current.SettingHeadsRotation(_startLocalTouchPosition);
                }

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if(_isReadyToAcceptInput){
                    
                    _trackLocalTouchPosition = GetThePositionOfTheTouch(touch);
                    
                    _distance = Vector3.Distance(_startLocalTouchPosition, _trackLocalTouchPosition);
                    
                    float tempDistance = DistanceBetween2Points2D(_startLocalTouchPosition, _trackLocalTouchPosition);
                    
                    if (HorizontalSwipe(_trackLocalTouchPosition.x) > VerticalSwipe(_trackLocalTouchPosition.y)) //If the touch is horizontal  
                    {
                        SetDrawPosition(this.transform.position);
                        tongTargetPositionGameObject.transform.localPosition = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        if (tempDistance > minDistanceToSpawnTong)//Debugging tool to check weather the touch has a minimum vertical distance to spawn the tong
                        {
                            _renderLine.startColor = Color.white;
                            _renderLine.endColor = Color.white;
                        }else
                        {
                            _renderLine.startColor = Color.gray;
                            _renderLine.endColor = Color.gray;
                        }
                        
                        if(_startLocalTouchPosition.y > _trackLocalTouchPosition.y) //if the user moves the touch above its first touch the line and mark will disappear
                        {
                            tongTargetPositionGameObject.transform.localPosition = new Vector3(0, _distance, 0);
                            SetDrawPosition(tongTargetPositionGameObject.transform.position, 1);
                            SetDrawPosition(this.transform.position, 0);
                        }
                        else
                        {
                            SetDrawPosition(this.transform.position);
                        }
                    }

                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _endingLocalTouchPosition = GetThePositionOfTheTouch(touch);

                _distance = DistanceBetween2Points2D(_startLocalTouchPosition, _endingLocalTouchPosition);
                
                DebugingContactPoints(endTargetPoint, _endingLocalTouchPosition); // code to show the contact points of the user input

                if(HorizontalSwipe(_endingLocalTouchPosition.x) > VerticalSwipe(_endingLocalTouchPosition.y)){

                    if(_distance > minDistanceToMoveFrogX){
                        EventSystem.current.MovingFrogSideToSide(_startLocalTouchPosition.x,_endingLocalTouchPosition.x);
                    }
                    if(_isReadyToAcceptInput){
                        EventSystem.current.SettingHeadsRotation(Vector3.zero); //Default Target location that the frog will look at
                    }
                    
                }else{
                
                    if(_isReadyToAcceptInput){

                        if(_distance >= minDistanceToSpawnTong && _startLocalTouchPosition.y > _endingLocalTouchPosition.y){
                            
                            tongBehaviour.SetCoroutineToSpawnTong(tongTargetPositionGameObject.transform.position);
                            //EventSystem.current.SettingStamina();// I NEED TO CHECK THIS CODE !!!! <===============
                            _isReadyToAcceptInput = false;
                        }

                    }
                }
                tongTargetPositionGameObject.transform.localPosition = new Vector3(0, 0, 0);
                SetDrawPosition(this.transform.position);
            }
        }
    }

    Vector3 GetThePositionOfTheTouch(Touch touch){
        Vector3 positionTouch = Camera.main.ScreenToWorldPoint(touch.position);
        positionTouch.z = 0f;
        return positionTouch;
    }

    // Function to draw the user input drag touch
    private void SetDrawPosition(Vector3 position, int lineNum = 2)
    {
        if (lineNum == 2)
        {
            _renderLine.SetPosition(0, position);
            _renderLine.SetPosition(1, position);
        }
        else if (lineNum == 0)
        {
            _renderLine.SetPosition(lineNum, position);
        }
        else if (lineNum == 1)
        {
            _renderLine.SetPosition(lineNum, position);
        }
        else
        {
            Debug.LogError("Wrong LineNum Input. Input must be a number between 0 and 2 inclusive. Your Input = " + lineNum);
        }
    }

//Function to return the distance between two points in float number
    private float DistanceBetween2Points2D(Vector3 positionOne, Vector3 positionTwo)
    {
        Vector3 distanceCoordinates = positionOne - positionTwo;
        distanceCoordinates.z = 0f;
        float distanceFloat = (distanceCoordinates[0] * distanceCoordinates[0]) + (distanceCoordinates[1] * distanceCoordinates[1]);
        return distanceFloat;
    }

//Function to determinate the absolute value on the X axis in two coordinates
    private float HorizontalSwipe(float end){
        return Mathf.Abs(_startLocalTouchPosition.x - end);
    }

//Function to determinate the absolute value on the Y axis in two coordinates
    private float VerticalSwipe(float end){
        return Mathf.Abs(_startLocalTouchPosition.y - end);
    }

//function to debug the contact points of the user showing where the user clicked or released the touch
    private void DebugingContactPoints(GameObject debuger, Vector3 contact){
        debuger.transform.position = contact;
    }
}