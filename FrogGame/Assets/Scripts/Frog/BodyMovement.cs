using System;
using System.Collections;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{

    IEnumerator frogBodyMovement;
    Vector3 startFrogPosition;
    Vector3 endFrogPositionLeft;
    Vector3 endFrogPositionRight;
    const float TOTAL_DISTANCE_TO_MOVE_FROG = 2; // Desired amount of distance to move the frog side to side (2.0f is the perfect distance) it shouldn't exceede the screen boundaries
    const float SCREEN_BOUNDARIES = 2.0f;
    bool _isFrogBodyMoving;
    public bool IsFrogBodyMoving { get => _isFrogBodyMoving; }
    float elapsedTime;

    [Tooltip("Desired amount of time to move the frog side to side")]
    [SerializeField][Range(0.01f, 3.0f)]
    float desiredDuration = 3f;

    //Function to set and start the coroutine to move the frog
    public void SetFrogBodyMovementCoroutine(float startingPoint, float endingPoint){
        frogBodyMovement = FrogHorizontalMovementCoroutine(startingPoint, endingPoint);
        StartCoroutine(frogBodyMovement);
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

        while(elapsedTime <= desiredDuration){

            elapsedTime += Time.deltaTime;
            float percentageCompleted = elapsedTime / desiredDuration;

            if (startingUserInputPoint > endingUserInputPoint && startFrogPosition.x < SCREEN_BOUNDARIES)//movement to the right
            {
                transform.position = Vector3.Lerp(startFrogPosition, endFrogPositionRight, percentageCompleted);
            }
            else if (startingUserInputPoint < endingUserInputPoint && startFrogPosition.x > -SCREEN_BOUNDARIES)//movement to the left
            {
                transform.position = Vector3.Lerp(startFrogPosition, endFrogPositionLeft, percentageCompleted);
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
