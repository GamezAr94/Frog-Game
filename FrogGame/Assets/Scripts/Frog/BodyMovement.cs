using System;
using System.Collections;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    [Tooltip("Desired amount of time to move the frog side to side")]
    [SerializeField][Range(0.01f, 3.0f)]
    public float desiredMovementDuration = 3f;
    const float TOTAL_DISTANCE_TO_MOVE_FROG = 2; // Desired amount of distance to move the frog side to side (2.0f is the perfect distance) it shouldn't exceede the screen boundaries
    const float SCREEN_BOUNDARIES = 2.0f;
    
    IEnumerator frogBodyMovement;
    Vector3 startFrogPosition;
    Vector3 endFrogPositionLeft;
    Vector3 endFrogPositionRight;
    bool _isFrogBodyMoving;
    float elapsedTime;


    private void Start() {
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
        _isFrogBodyMoving = true;

        startFrogPosition = this.transform.position;

        Vector3 endFrogPositionLeft = startFrogPosition;
        endFrogPositionLeft.x -= TOTAL_DISTANCE_TO_MOVE_FROG;

        Vector3 endFrogPositionRight = startFrogPosition;
        endFrogPositionRight.x += TOTAL_DISTANCE_TO_MOVE_FROG;

        elapsedTime = 0.0f;

        while(elapsedTime <= desiredMovementDuration){

            elapsedTime += Time.deltaTime;
            float percentageCompleted = elapsedTime / desiredMovementDuration;

            if (startingUserInputPoint > endingUserInputPoint && startFrogPosition.x > -SCREEN_BOUNDARIES)//movement to the left
            {
                transform.position = Vector3.Lerp(startFrogPosition, endFrogPositionLeft, percentageCompleted);
            }
            else if (startingUserInputPoint < endingUserInputPoint && startFrogPosition.x < SCREEN_BOUNDARIES)//movement to the right
            {
                transform.position = Vector3.Lerp(startFrogPosition, endFrogPositionRight, percentageCompleted);
            }
            else
            {
                break;
            }

            yield return null;
        }

        _isFrogBodyMoving = false;
        StopCoroutine(frogBodyMovement);
    }
}
