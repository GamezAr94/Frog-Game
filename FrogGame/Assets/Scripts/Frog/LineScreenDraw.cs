using UnityEngine;
using System;

public class LineScreenDraw : MonoBehaviour
{
    [Tooltip("Float that indicates the limit where the user can enter the starting input in the Y axis")]
    const float BORDER_LIMIT_TO_POINT_ATTACK_Y = -3f;
    
    [Header("Line Rendering Settings")]

    [SerializeField][Range (0f, 5f)]
    [Tooltip("Float that indicates the minimum distance between the starting and ending user touch input required to spawn the tong")]
    float minDistanceToSpawnTong = 2.0f;
    
    [SerializeField][Range (0f, 5f)]
    [Tooltip("Float that indicates the minimum distance between the starting and ending user touch input reuired to move the frog")]
    float minDistanceToMoveFrog = 3.0f;
    

    private Vector3 _startLocalTouchPosition;
    public Vector3 StartLocalTouchPosition { get => _startLocalTouchPosition; private set => _startLocalTouchPosition = value; }

    private Vector3 _endingLocalTouchPosition;
    public Vector3 EndingLocalTouchPosition { get => _endingLocalTouchPosition; private set => _endingLocalTouchPosition = value; }

    private Vector3 trackLocalTouchPosition;

    private float _distance;
    public float Distance { get => _distance; private set => _distance = value; }


    [Header("Game Objects/Components")]

    [SerializeField]
    TongBehaviour tongBehaviour;

    LineRenderer renderLine;

    [Header("Debuger Tools")]

    [Tooltip("Game object that helps to visualize the position of the users touch input")]
    public GameObject startTargetPoint;
    [Tooltip("Game object that helps to visualize the position of the users touch exit")]
    public GameObject endTargetPoint;

    bool isReadyToAcceptInput = true; //avoid the user entering an input before the tong cames back to its mouth

    private void Awake()
    {
        renderLine = this.GetComponent<LineRenderer>();
        renderLine.positionCount = 2;

        startTargetPoint.transform.position = _startLocalTouchPosition;//Debuger object
        endTargetPoint.transform.position = _endingLocalTouchPosition;//Debuger object
                        
        setDrawPosition();

        EventSystem.current.onSwipeTouch += PositionUserTouch;
    }

//Funtion to retrieve the user input, the position of the first touch, the swipe of the input and the position of the end of the touch
//this function is the logic to move the frog in the right direction and spawn the tong
//this function has a debbuging system where two objects are posisionated where the user press and releases its input
//also this function draw a line to visualize the user input
    private void PositionUserTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                _startLocalTouchPosition = GetThePositionOfTheTouch(touch);
                
                if(_startLocalTouchPosition.y >= BORDER_LIMIT_TO_POINT_ATTACK_Y && tongBehaviour.TongInMouth){
                    isReadyToAcceptInput = true;

                    //debugingContactPoints(startTargetPoint, _startLocalTouchPosition);

                    setDrawPosition(2, _startLocalTouchPosition.x, _startLocalTouchPosition.y);

                    EventSystem.current.SettingHeadsRotation(_startLocalTouchPosition);
                }

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if(_startLocalTouchPosition.y >= BORDER_LIMIT_TO_POINT_ATTACK_Y  && tongBehaviour.TongInMouth && isReadyToAcceptInput){
                    

                    trackLocalTouchPosition = GetThePositionOfTheTouch(touch);
                    
                    if(_startLocalTouchPosition.y >= trackLocalTouchPosition.y){ //Si el touch es movido por debajo del touch inicial
                        setDrawPosition(0, _startLocalTouchPosition.x, _startLocalTouchPosition.y);
                        setDrawPosition(1, trackLocalTouchPosition.x, trackLocalTouchPosition.y);
                    }else{
                        setDrawPosition();
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _endingLocalTouchPosition = GetThePositionOfTheTouch(touch);

                _distance = DistanceBetween2Points2D(_startLocalTouchPosition, _endingLocalTouchPosition);
                
                //debugingContactPoints(endTargetPoint, _endingLocalTouchPosition); // code to show the contact points of the user input

                if(HorizontalSwipe() > VerticalSwipe()){

                    if(_distance > minDistanceToMoveFrog){
                        EventSystem.current.MovingFrogSideToSide(_startLocalTouchPosition.x,_endingLocalTouchPosition.x);
                    }
                    if(isReadyToAcceptInput){
                        EventSystem.current.SettingHeadsRotation(Vector3.zero); //Default Target location that the frog will look at
                    }
                    
                }else{
                
                    if(_startLocalTouchPosition.y >= BORDER_LIMIT_TO_POINT_ATTACK_Y && tongBehaviour.TongInMouth && isReadyToAcceptInput){

                        if(_distance >= minDistanceToSpawnTong && _startLocalTouchPosition.y > _endingLocalTouchPosition.y){
                            
                            tongBehaviour.SetCoroutineToSpawnTong(_distance);

                            isReadyToAcceptInput = false;
                        }

                        //debugingContactPoints(startTargetPoint, _nonReachablePoint);
                        //debugingContactPoints(endTargetPoint, _nonReachablePoint);

                    }
                }
                setDrawPosition();
            }
        }
    }

Vector3 GetThePositionOfTheTouch(Touch touch){
    Vector3 positionTouch = Camera.main.ScreenToWorldPoint(touch.position);
    positionTouch.z = 0f;
    return positionTouch;
}

// Function to draw the unser input drag touch
    public void setDrawPosition(int lineNum = 2, float positionX = 0, float positionY = 0)
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
            Debug.LogError("Wrong LineNum Input. Input must be a number between 0 and 2 inclusive. Your Inut = " + lineNum);
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
    private float HorizontalSwipe(){
        return Mathf.Abs(_startLocalTouchPosition.x - _endingLocalTouchPosition.x);
    }

//Function to determinate the absolute value on the Y axis in two coordinates
    private float VerticalSwipe(){
        return Mathf.Abs(_startLocalTouchPosition.y - _endingLocalTouchPosition.y);
    }

//function to debug the contact points of the user showing where the user clicked or released the touch
    private void debugingContactPoints(GameObject debuger, Vector3 contact){
        debuger.transform.position = contact;
    }
}