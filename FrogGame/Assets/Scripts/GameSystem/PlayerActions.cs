using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    TongBehaviour tongObjecBehaviour;

    LineScreenDraw lineRendererObject;

    public bool test = false;

    IEnumerator spawnTong;

    private void Awake()
    {
        lineRendererObject = this.GetComponent<LineScreenDraw>();
    }

    public void Update() {
        if(spawnTong != null){
            StartCoroutine(testCoroutine());
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
            spawnTong = tongObjecBehaviour.spawningTongCoroutine(lineRendererObject.Distance);
            StartCoroutine(spawnTong);
        }
    }

    public IEnumerator testCoroutine()
    {
        yield return new WaitUntil( () => test == true );
        StopCoroutine(spawnTong);
        yield return new WaitUntil( () => test == false );
        StartCoroutine(spawnTong);
        yield return null;
    }

}
