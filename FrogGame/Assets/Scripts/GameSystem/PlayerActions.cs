using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    TongBehaviour tongBehaviour;

    LineScreenDraw lineRendererObject;

    IEnumerator spawnTong;

    private void Awake()
    {
        lineRendererObject = this.GetComponent<LineScreenDraw>();
    }

    public void Update() {
        if(!tongBehaviour.TongInMouth){
            transform.right = lineRendererObject.StartLocalTouchPosition - transform.position;
        }
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
            spawnTong = tongBehaviour.spawningTongCoroutine(lineRendererObject.Distance);
            StartCoroutine(spawnTong);
        }
    }

}
