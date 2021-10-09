using UnityEngine;

public class LineScreenDraw : MonoBehaviour
{
    private Vector3 _startLocalTouchPosition;
    private Vector3 trackLocalTouchPosition;
    public Vector3 StartLocalTouchPosition { get => _startLocalTouchPosition; private set => _startLocalTouchPosition = value; }

    private float _distance;
    public float Distance { get => _distance; private set => _distance = value; }

    LineRenderer renderLine;
    private void Awake()
    {
        renderLine = this.GetComponent<LineRenderer>();
        renderLine.positionCount = 2;
    }

    private void Update()
    {
        PositionTouch();
    }

    private void PositionTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _startLocalTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                _startLocalTouchPosition.z = 0f;
                setDrawPosition1(2, _startLocalTouchPosition.x, _startLocalTouchPosition.y);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                trackLocalTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                trackLocalTouchPosition.z = 0f;
                setDrawPosition1(1, trackLocalTouchPosition.x, trackLocalTouchPosition.y);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                setDrawPosition1();
                _distance = DistanceBetween2Points2D1(_startLocalTouchPosition, trackLocalTouchPosition);
            }
        }
    }


// Function to draw the unser input drag touch
    public void setDrawPosition1(int lineNum = 2, float positionX = 0, float positionY = 0)
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
    private float DistanceBetween2Points2D1(Vector3 positionOne, Vector3 positionTwo)
    {
        Vector3 distanceCoordinates = positionOne - positionTwo;
        distanceCoordinates.z = 0f;
        float distanceFloat = (distanceCoordinates[0] * distanceCoordinates[0]) + (distanceCoordinates[1] * distanceCoordinates[1]);
        return distanceFloat;
    }
}