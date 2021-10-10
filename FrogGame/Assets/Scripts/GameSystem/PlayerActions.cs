using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    GameObject tongObject;

    TongBehaviour tongObjecBehaviour;

    Rigidbody2D tongRingidBody;

    Vector3 touchPosition;

    Camera mainCamera;


    LineScreenDraw lineRendererObject;

    [SerializeField]
    float borderLimitToPointTheAttack = -3f;

    private void Awake()
    {
        lineRendererObject = this.GetComponent<LineScreenDraw>();

        tongObjecBehaviour = tongObject.GetComponent<TongBehaviour>();
        tongRingidBody = tongObject.GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;
    }

    public void FrogReadyToSpawnTong(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            Debug.Log("This is an event system test " + touch.phase);
            transform.right = lineRendererObject.StartLocalTouchPosition - transform.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Debug.Log("This is an event system test " + touch.phase);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            Debug.Log("This is an event system test " + touch.phase);
            StartCoroutine(tongObjecBehaviour.spawningTongCoroutine(lineRendererObject.Distance));
        }
    }

    private void releaseTong(Vector3 position, TongBehaviour tongBehaviour)
    {
        touchPosition = mainCamera.ScreenToWorldPoint(position);
        if (
            tongBehaviour.tongInMouth &&
            touchPosition.y > borderLimitToPointTheAttack
        )
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

}
