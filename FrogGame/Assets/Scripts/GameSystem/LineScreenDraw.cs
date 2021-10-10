using UnityEngine;

public class LineScreenDraw : MonoBehaviour
{
    [SerializeField]
    TongBehaviour tongBehaviour;

    [SerializeField][Range (0f, 5f)]
    float minDistanceToSpawnTong = 2.0f;
    
    [SerializeField]
    const float borderLimitToPointTheAttack = -3f;

    private Vector3 _nonReachablePoint = new Vector3(-100,-100,-100);
    public Vector3 NonReachablePoint { get => _nonReachablePoint; }

    private Vector3 _startLocalTouchPosition;
    public Vector3 StartLocalTouchPosition { get => _startLocalTouchPosition; private set => _startLocalTouchPosition = value; }

    private Vector3 _endingLocalTouchPosition;
    public Vector3 EndingLocalTouchPosition { get => _endingLocalTouchPosition; private set => _endingLocalTouchPosition = value; }

    private Vector3 trackLocalTouchPosition;

    private float _distance;
    public float Distance { get => _distance; private set => _distance = value; }

    LineRenderer renderLine;

    public GameObject startTargetPoint;
    public GameObject endTargetPoint;

    PlayerActions playerActions;

    private void Awake()
    {
        renderLine = this.GetComponent<LineRenderer>();
        renderLine.positionCount = 2;

        _startLocalTouchPosition = NonReachablePoint;
        _endingLocalTouchPosition = NonReachablePoint;
        trackLocalTouchPosition = NonReachablePoint;

        startTargetPoint.transform.position = _startLocalTouchPosition;
        endTargetPoint.transform.position = _endingLocalTouchPosition;

        playerActions = this.GetComponent<PlayerActions>();

        EventSystem.current.onSwipeTouch += PositionUserTouch;

    }

    private void Update()
    {
        if(tongBehaviour.tongInMouth){
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
                
                if(_startLocalTouchPosition.y >= borderLimitToPointTheAttack){
                    debugingContactPoints(startTargetPoint, _startLocalTouchPosition);

                    setDrawPosition(2, _startLocalTouchPosition.x, _startLocalTouchPosition.y);

                    playerActions.FrogReadyToSpawnTong(touch);
                }

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                trackLocalTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                trackLocalTouchPosition.z = 0f;
                
                if(_startLocalTouchPosition.y >= trackLocalTouchPosition.y){
                    setDrawPosition(0, _startLocalTouchPosition.x, _startLocalTouchPosition.y);
                    setDrawPosition(1, trackLocalTouchPosition.x, trackLocalTouchPosition.y);
                }else{
                    setDrawPosition();
                }
            }
            else if (touch.phase == TouchPhase.Ended && _startLocalTouchPosition.y >= borderLimitToPointTheAttack)
            {
                _endingLocalTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                _endingLocalTouchPosition.z = 0;

                _distance = DistanceBetween2Points2D(_startLocalTouchPosition, _endingLocalTouchPosition);

                if(_distance >= minDistanceToSpawnTong && _startLocalTouchPosition.y > _endingLocalTouchPosition.y){
                    debugingContactPoints(endTargetPoint, _endingLocalTouchPosition);


                    playerActions.FrogReadyToSpawnTong(touch);
                }

                //debugingContactPoints(startTargetPoint, _nonReachablePoint);
                //debugingContactPoints(endTargetPoint, _nonReachablePoint);

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

//function to debug the contact points of the user showing where the user clicked or released the touch
    private void debugingContactPoints(GameObject debuger, Vector3 contact){
        debuger.transform.position = contact;
    }
}