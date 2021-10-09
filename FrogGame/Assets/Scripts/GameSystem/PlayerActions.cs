using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    Vector3 startedTouchPosition, endingTouchPosition;

    [SerializeField]
    GameObject tongObject;
    TongBehaviour tongObjecBehaviour;
    Rigidbody2D tongRingidBody;
    Vector3 touchPosition;
    Camera mainCamera;

    [SerializeField]
    [Range(0, 1)]
    float velocityAttack = .7f;

    [SerializeField]
    LineScreenDraw lineRendererObject;

    [SerializeField]
    float borderLimitToPointTheAttack = -3f;

    [SerializeField]
    [Range(0f, 1.5f)]
    float minDistanceBetweenTouches;

    private void Awake()
    {
        tongObjecBehaviour = tongObject.GetComponent<TongBehaviour>();
        tongRingidBody = tongObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
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
                Vector3 startLocalTouchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                lineRendererObject.setDrawPosition(2, startLocalTouchPosition.x, startLocalTouchPosition.y);
                releaseTong(touch.position, tongObjecBehaviour);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 trackLocalTouchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                lineRendererObject.setDrawPosition(1, trackLocalTouchPosition.x, trackLocalTouchPosition.y);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                resetTong(touch.position, tongObjecBehaviour);
                lineRendererObject.setDrawPosition();
            }
        }
    }

    private void releaseTong(Vector3 position, TongBehaviour tongBehaviour)
    {
        touchPosition = mainCamera.ScreenToWorldPoint(position);
        if (tongBehaviour.tongInMouth && touchPosition.y > borderLimitToPointTheAttack)
        {
            startedTouchPosition = DistanceBetween2Points2D(touchPosition, this.transform.position);
            transform.right = startedTouchPosition;
        }
        else if (!tongBehaviour.tongInMouth)
        {
            // store the touch starting point if the user start clicking before the tong cames bak to the frog
            startedTouchPosition = DistanceBetween2Points2D(touchPosition, this.transform.position);
        }
    }
    private void resetTong(Vector3 position, TongBehaviour tongBehaviour)
    {
        if (tongBehaviour.tongInMouth && startedTouchPosition != new Vector3(0, 0, 0))
        {
            transform.right = startedTouchPosition;

            touchPosition = mainCamera.ScreenToWorldPoint(position);
            endingTouchPosition = DistanceBetween2Points2D(touchPosition, this.transform.position);
            float distanceBetweenPoints = (startedTouchPosition - endingTouchPosition).magnitude;
            if (distanceBetweenPoints > minDistanceBetweenTouches)
            {
                tongRingidBody.velocity = startedTouchPosition * distanceBetweenPoints * velocityAttack;
            }
            startedTouchPosition = new Vector3(0, 0, 0);
        }
    }
    private Vector3 DistanceBetween2Points2D(Vector3 positionOne, Vector3 positionTwo)
    {
        Vector3 distance = positionOne - positionTwo;
        distance.z = 0f;
        return distance;
    }
}
