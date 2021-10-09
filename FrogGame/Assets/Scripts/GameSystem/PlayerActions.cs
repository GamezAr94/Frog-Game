using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerActions : MonoBehaviour
{

    [SerializeField]
    GameObject tongObject;
    
    TongBehaviour tongObjecBehaviour;
    Rigidbody2D tongRingidBody;
    Vector3 touchPosition;
    Camera mainCamera;

    float velocityAttack = 20.0f;

    LineScreenDraw lineRendererObject;

    [SerializeField]
    float borderLimitToPointTheAttack = -3f;

    [SerializeField]
    [Range(0f, 1.5f)]
    float minDistanceBetweenTouches;


    private void Awake()
    {
        lineRendererObject = this.GetComponent<LineScreenDraw>();

        tongObjecBehaviour = tongObject.GetComponent<TongBehaviour>();
        tongRingidBody = tongObject.GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;

    }

    private void Update()
    {
        //PositionTouch();
    }

    private void PositionTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                transform.right = lineRendererObject.StartLocalTouchPosition - transform.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log(lineRendererObject.StartLocalTouchPosition + " Target");
                tongRingidBody.AddForce(transform.right * lineRendererObject.Distance * velocityAttack);
                //resetTong(touch.position, tongObjecBehaviour);
            }
        }
    }

    private void releaseTong(Vector3 position, TongBehaviour tongBehaviour)
    {
        touchPosition = mainCamera.ScreenToWorldPoint(position);
        if (tongBehaviour.tongInMouth && touchPosition.y > borderLimitToPointTheAttack)
        {
            //startedTouchPosition = DistanceBetween2Points2D(touchPosition, this.transform.position);
            //transform.right = startedTouchPosition;
        }
        else if (!tongBehaviour.tongInMouth)
        {
            // store the touch starting point if the user start clicking before the tong cames bak to the frog
            //startedTouchPosition = DistanceBetween2Points2D(touchPosition, this.transform.position);
        }
    } 


    private void resetTong(Vector3 position, TongBehaviour tongBehaviour)
    {
        if (tongBehaviour.tongInMouth )
        {
            
            
            if (lineRendererObject.Distance > minDistanceBetweenTouches)
            {
                //tongRingidBody.AddForce(new Vector3(0,1,0) * lineRendererObject.Distance * velocityAttack);
                //tongRingidBody.velocity = lineRendererObject.StartLocalTouchPosition * velocityAttack;
            }
            //startedTouchPosition = new Vector3(0, 0, 0);
        }
    }

}
