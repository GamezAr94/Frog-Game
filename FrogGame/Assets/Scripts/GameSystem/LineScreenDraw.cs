using UnityEngine;
using System;

public class LineScreenDraw : MonoBehaviour
{
    
    [Header("Line Rendering Settings")]

    [SerializeField][Range (0f, 5f)]
    [Tooltip("Float that indicates the minimum distance between the starting and ending user touch input required to spawn the tong")]
    float minDistanceToSpawnTong = 2.0f;
    
    [SerializeField][Range (0f, 5f)]
    [Tooltip("Float that indicates the minimum distance between the starting and ending user touch input reuired to move the frog")]
    float minDistanceToMoveFrog = 3.0f;
    
    [SerializeField][Range (1f, -5f)]
    [Tooltip("Float that indicates the limit where the user can enter the starting input in the Y axis")]
    const float BORDER_LIMIT_TO_POINT_ATTACK_Y = -3f;

    private Vector3 _startLocalTouchPosition;
    public Vector3 StartLocalTouchPosition { get => _startLocalTouchPosition; private set => _startLocalTouchPosition = value; }

    private Vector3 _endingLocalTouchPosition;
    public Vector3 EndingLocalTouchPosition { get => _endingLocalTouchPosition; private set => _endingLocalTouchPosition = value; }

    private Vector3 trackLocalTouchPosition;

    private float _distance;
    public float Distance { get => _distance; private set => _distance = value; }

    [SerializeField]
    TongBehaviour tongBehaviour;

    LineRenderer renderLine;
    PlayerActions playerActions;

    [SerializeField]
    BodyMovement bodyMovementScript;

    [Header("Debuger Tools")]

    [Tooltip("Game object that helps to visualize the position of the users touch input")]
    public GameObject startTargetPoint;
    [Tooltip("Game object that helps to visualize the position of the users touch exit")]
    public GameObject endTargetPoint;


    private void Awake()
    {
        renderLine = this.GetComponent<LineRenderer>();
        renderLine.positionCount = 2;

        startTargetPoint.transform.position = _startLocalTouchPosition;//Debuger object
        endTargetPoint.transform.position = _endingLocalTouchPosition;//Debuger object

        playerActions = this.GetComponent<PlayerActions>();

        EventSystem.current.onSwipeTouch += PositionUserTouch;
    }

    private void Update()
    {
        if(tongBehaviour.TongInMouth){
            EventSystem.current.swipeTouch();
        }
    }

    private void PositionUserTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                _startLocalTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                _startLocalTouchPosition.z = 0f;
                
                if(_startLocalTouchPosition.y >= BORDER_LIMIT_TO_POINT_ATTACK_Y){
                    debugingContactPoints(startTargetPoint, _startLocalTouchPosition);

                    setDrawPosition(2, _startLocalTouchPosition.x, _startLocalTouchPosition.y);

                    playerActions.FrogReadyToSpawnTong(touch);
                }

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if(_startLocalTouchPosition.y >= BORDER_LIMIT_TO_POINT_ATTACK_Y){

                    trackLocalTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    trackLocalTouchPosition.z = 0f;
                    
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
                _endingLocalTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                _endingLocalTouchPosition.z = 0;

                _distance = DistanceBetween2Points2D(_startLocalTouchPosition, _endingLocalTouchPosition);

                if(HorizontalSwipe() > VerticalSwipe()){

                    if(_distance > minDistanceToMoveFrog){
                        bodyMovementScript.FrogMovement(_startLocalTouchPosition.x, _endingLocalTouchPosition.x);
                    }

                    playerActions.SettingFrogsHeadRotation(new Vector3(0,5,0)); //Default Target location that the frog will look at

                }else{
                
                    if(_startLocalTouchPosition.y >= BORDER_LIMIT_TO_POINT_ATTACK_Y){


                        if(_distance >= minDistanceToSpawnTong && _startLocalTouchPosition.y > _endingLocalTouchPosition.y){
                            
                            debugingContactPoints(endTargetPoint, _endingLocalTouchPosition);

                            playerActions.FrogReadyToSpawnTong(touch);
                        }

                        //debugingContactPoints(startTargetPoint, _nonReachablePoint);
                        //debugingContactPoints(endTargetPoint, _nonReachablePoint);

                    }
                }
                setDrawPosition();
            }
        }
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