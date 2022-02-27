using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    const float TOTAL_DISTANCE_TO_MOVE_FROG = 2; // Desired amount of distance to move the frog side to side (2.0f is the perfect distance) it shouldn't exceede the screen boundaries
    const float SCREEN_BOUNDARIES = 2.0f;
    
    IEnumerator frogBodyMovement;
    bool _isFrogBodyMoving;

    [SerializeField]
    AnimationCurve movementCurve;
    
    [SerializeField]
    [Range(10, 50)]
    [Tooltip("Speed of the car when moving side to side, the greater the faster")]
    int movementSpeed;

    public static Vector3 FrogPosition;


    private void Start() {
        FrogPosition = this.transform.position;
        EventSystem.current.onMovingFrogSideToSide += SetFrogBodyMovementCoroutine;
    }

    //Function to set and start the coroutine to move the frog
    public void SetFrogBodyMovementCoroutine(float startingPoint, float endingPoint){
        if(!_isFrogBodyMoving){
            frogBodyMovement = FrogHorizontalMovementCoroutine(startingPoint, endingPoint);
            StartCoroutine(frogBodyMovement);
        }
    }
    private void OnDestroy() {
        if(frogBodyMovement!=null){
            StopCoroutine(frogBodyMovement);
        }
    }
    
    //Coroutine to move the frog side to side
    private IEnumerator FrogHorizontalMovementCoroutine(float startingUserInputPoint, float endingUserInputPoint)
    {
        float movementDirection = startingUserInputPoint > endingUserInputPoint ? -TOTAL_DISTANCE_TO_MOVE_FROG : TOTAL_DISTANCE_TO_MOVE_FROG;
        Vector3 endingPosition = this.transform.position;
        endingPosition.x += movementDirection;
        
        _isFrogBodyMoving = true;

        float time = 0;
        float speedMovement = 0;

        while(this.transform.position.x != endingPosition.x){
            speedMovement = movementCurve.Evaluate(time);
            time += Time.deltaTime;

            if(endingPosition.x >= -SCREEN_BOUNDARIES && endingPosition.x <= SCREEN_BOUNDARIES){
                transform.position = Vector3.MoveTowards(this.transform.position, endingPosition, movementSpeed*speedMovement*Time.deltaTime);
            }
            else{
                break;
            }

            FrogPosition = this.transform.position;
            
            yield return null;
        }

        _isFrogBodyMoving = false;
        StopCoroutine(frogBodyMovement);
    }
}
