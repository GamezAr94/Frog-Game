using System;
using UnityEngine;
using UnityEngine.U2D;
using System.Collections;

public class TongBehaviour : MonoBehaviour
{
    const string ATTAKING_TAG_NAME = "TongTip";
    const string DISABLED_TONG_TAG_NAME = "DisabledTongTip";

    [Header("Tong Settings")]

    [SerializeField]
    [Tooltip("Bool to pause the tong at the middle of its spawn")]
    bool tongsIsPaused = false;

    public Vector3 ThisPosition { get => this.transform.position; }

    [SerializeField]
    [Tooltip("Bool that indicates when the tong must go back to its mouth")]
    bool tongMustGoBack = false;
    
    [SerializeField]
    [Tooltip("Bool to indicate whenever the tong is in its mouth")]
    bool _tongInMouth = true;
    public bool TongInMouth { get => _tongInMouth; private set => _tongInMouth = value; }

    [SerializeField]
    [Tooltip("The pivot where the tong has to go back")]
    Transform tongPivotObject;

    [SerializeField]
    [Tooltip("The body of the tong")]
    BodyTongBehaviour bodyTongBehaviour;

    [Tooltip("The time that tong will take to complete the attack")]
    [SerializeField][Range (0.1f, 2f)]
    float desiredTongAttackDuration = 0.1f;

    [Tooltip("The greater the shorter")]
    [SerializeField][Range (5f, 20f)]
    float rangeOfTong = 12f;

    IEnumerator spawnTong;

    private void Awake() {
        transform.position = tongPivotObject.position;
        ChangeTagName(DISABLED_TONG_TAG_NAME);
    }

    void ChangeTagName(string tag){
        this.transform.gameObject.tag = tag;
    }

    //Function to start the coroutine that will spawn the tong
    public void SetCoroutineToSpawnTong(float distance)
    {
        spawnTong = spawningTongCoroutine(distance);
        StartCoroutine(spawnTong);
    }

//Coroutine to spawn the tong, it can hadle pauses, and it can returns the tong before it has completed its path.
//it sets the original position of the tong, the right position of the nodes of the body tong and the bools in charge of returning and pausing the tong
    IEnumerator spawningTongCoroutine(float distance)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + (transform.up * (distance/rangeOfTong));

        float percentageAttackCompleted;
        float elapsedTime = 0;

        ChangeTagName(ATTAKING_TAG_NAME);

        while (elapsedTime < desiredTongAttackDuration && !tongMustGoBack)
        {
            if(!tongsIsPaused){
                elapsedTime += Time.deltaTime;
                percentageAttackCompleted = elapsedTime / desiredTongAttackDuration;
                
                transform.position = Vector3.Lerp(startingPos, finalPos, percentageAttackCompleted);
                bodyTongBehaviour.NodesFollowing();
                _tongInMouth = false;
            }
            yield return null;
        }

        startingPos = transform.position;

        desiredTongAttackDuration = elapsedTime; // setting the duration of the return equals to the time elapsed going forward

        elapsedTime = 0;

        ChangeTagName(DISABLED_TONG_TAG_NAME);
        
        while (elapsedTime < desiredTongAttackDuration)
        {
            tongMustGoBack = true;
            if(!tongsIsPaused){

                elapsedTime += Time.deltaTime;
                percentageAttackCompleted = elapsedTime / desiredTongAttackDuration;

                transform.position = Vector3.Lerp(startingPos, tongPivotObject.position, percentageAttackCompleted);
                bodyTongBehaviour.NodesFollowing();
            }
            yield return null;
        }

        transform.position = tongPivotObject.position;

        bodyTongBehaviour.NodesFollowing();

        tongMustGoBack = false;
        _tongInMouth = true;

        StopCoroutine(spawnTong);
    }
}
