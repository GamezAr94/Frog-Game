using System;
using UnityEngine;
using UnityEngine.U2D;
using System.Collections;

public class TongBehaviour : MonoBehaviour
{
    const string ATTAKING_TAG_NAME = "TongTip"; //Tag name to identify whenever the tong is able to catch creatures
    const string DISABLED_TONG_TAG_NAME = "DisabledTongTip"; //Tag name to identify whenever the tong is NOT able to catch creatures
    
    [Header("Screen Boundaries")]
    [SerializeField]
    [Tooltip("Screen boundaries that the tong cannot pass")]
    CreatureBoundaries SCREEN_BOUNDARIES;

    [Header("Tong Settings")]

    [SerializeField]
    [Tooltip("Bool to pause the tong at the middle of its spawn")]
    bool tongsIsPaused = false;

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

    [Tooltip("The greater the shorter")]
    [SerializeField][Range (5f, 20f)]
    float rangeOfTong = 12f;

    [SerializeField][Tooltip("Animation curve to define the speed of the tong when attacking")]
    AnimationCurve movementCurveTongAttack;

    [SerializeField][Tooltip("Animation curve to define the movement of the tong when moving backward")]
    AnimationCurve movementCurveTongBack;

    IEnumerator spawnTong;

    private void Awake() {
        transform.position = tongPivotObject.position;
        ChangeTagName(DISABLED_TONG_TAG_NAME);

    }

    private void Update() {
        if(!TongInMouth){
            EventSystem.current.SettingHeadsRotation(this.transform.position);
        }
    }
    private void OnDestroy() {
        if(spawnTong!=null){
            StopCoroutine(spawnTong);
        }
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

    // function that accepts a param distance which is the distance of the user's touch
    IEnumerator spawningTongCoroutine(float distance)
    {
        Vector3 finalPos = transform.position + (transform.up * (distance/rangeOfTong));
        
        ChangeTagName(ATTAKING_TAG_NAME);

        float time = 0;
        float speedTongAttack = 0;

        while (Vector3.Distance(this.transform.position, finalPos) >= 0.2f && !tongMustGoBack && ScreenLimits())
        {
            if(!tongsIsPaused){
                speedTongAttack = movementCurveTongAttack.Evaluate(time);
                time += Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, finalPos, speedTongAttack);
                
                _tongInMouth = false;
            }
            yield return null;
        }

        ChangeTagName(DISABLED_TONG_TAG_NAME);

        //EventSystem.current.SettingCombo(this.transform.childCount); //this event is to handle the COMBOS, I need to think about how to implement it

        finalPos = tongPivotObject.position;
        time = 0;
        
        while (Vector3.Distance(this.transform.position, finalPos) >= 0.2f)
        {
            tongMustGoBack = true;

            if(!tongsIsPaused){

                speedTongAttack = movementCurveTongBack.Evaluate(time);
                time += Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, finalPos, speedTongAttack * 1.5f);
            }
            finalPos = tongPivotObject.position;
            yield return null;
        }

        transform.position = tongPivotObject.position;
        
        tongMustGoBack = false;
        _tongInMouth = true;

        StopCoroutine(spawnTong);
    }

    bool ScreenLimits(){
        if (SCREEN_BOUNDARIES == null)
        {
            Debug.Log("No boundaries assigned: CreatureBoundaries missing");
        }
        if(this.transform.position.y > SCREEN_BOUNDARIES?.CoordinatesOfMovementY[0] || this.transform.position.x > SCREEN_BOUNDARIES?.CoordinatesOfMovementX[0] || this.transform.position.x < SCREEN_BOUNDARIES?.CoordinatesOfMovementX[1]){
            return false;
        }
        return true;
    }

//LERP MOVEMENT - THIS WILL CAUSE THAT THE TONG SPENDS THE SAME AMOUNT OF TIME REGARDLESS THE DISTANCE 
// ====================================================================================================
/*
//Coroutine to spawn the tong, it can hadle pauses, and it can returns the tong before it has completed its path.
//it sets the original position of the tong, the right position of the nodes of the body tong and the bools in charge of returning and pausing the tong
    /*IEnumerator spawningTongCoroutine(float distance)
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
                EventSystem.current.BodyTongFOllowingTong(this.transform.localPosition);
                _tongInMouth = false;
            }
            yield return null;
        }

        startingPos = transform.position;

        desiredTongAttackDuration = elapsedTime; // setting the duration of the return equals to the time elapsed going forward

        elapsedTime = 0;

        ChangeTagName(DISABLED_TONG_TAG_NAME);

        EventSystem.current.SettingCombo(this.transform.childCount);
        
        while (elapsedTime < desiredTongAttackDuration)
        {
            tongMustGoBack = true;
            if(!tongsIsPaused){

                elapsedTime += Time.deltaTime;
                percentageAttackCompleted = elapsedTime / desiredTongAttackDuration;

                transform.position = Vector3.Lerp(startingPos, tongPivotObject.position, percentageAttackCompleted);
                EventSystem.current.BodyTongFOllowingTong(this.transform.localPosition);
            }
            yield return null;
        }

        transform.position = tongPivotObject.position;

        EventSystem.current.BodyTongFOllowingTong(this.transform.localPosition);

        tongMustGoBack = false;
        _tongInMouth = true;

        StopCoroutine(spawnTong);
    }
    */
}
