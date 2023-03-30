using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    const float TOTAL_DISTANCE_TO_MOVE_FROG = 2.1f; // Desired amount of distance to move the frog side to side (2.0f is the perfect distance) it shouldn't exceede the screen boundaries
    const float SCREEN_BOUNDARIES = 2.1f; //this is the same value as the total distance to move frog because is the limit of movement
    private const float CHARACTER_Y_POSITION = -4.92f;
    
    IEnumerator frogBodyMovement;
    bool _isFrogBodyMoving;

    [SerializeField]
    AnimationCurve movementCurve;
    
    [SerializeField]
    [Range(5, 50)]
    [Tooltip("Speed of the car when getting the position when starting the game")]
    int placingCarSpeed;
    
    [SerializeField]
    [Range(10, 50)]
    [Tooltip("Speed of the car when moving side to side, the greater the faster")]
    int movementSpeed;

    public static Vector3 FrogPosition;


    private void Start()
    {
        FrogPosition = this.transform.position;
        EventSystem.current.onMovingFrogSideToSide += SetFrogBodyMovementCoroutine;
        
        //EventSystem.current.onStartMovingTheGame += PlacingCarStarter;
    }

    //Function to set and start the coroutine to move the frog
    public void SetFrogBodyMovementCoroutine(float startingPoint, float endingPoint){
        if(!_isFrogBodyMoving){
            frogBodyMovement = FrogHorizontalMovementCoroutine(startingPoint, endingPoint);
            StartCoroutine(frogBodyMovement);
            
            //StartCoroutine(PlacingTheCharacter());
        }
    }
    private void OnDestroy() {
        if(frogBodyMovement!=null){
            StopCoroutine(frogBodyMovement);
        }
        EventSystem.current.onMovingFrogSideToSide -= SetFrogBodyMovementCoroutine;

        StopCoroutine(PlacingTheCharacter());
        //EventSystem.current.onStartMovingTheGame -= PlacingCarStarter;
    }
    
    void PlacingCarStarter(){ 
        StartCoroutine(PlacingTheCharacter());
    }

    //------------------------CHECK IF IT IS NECESSARY IMPLEMENT THIS----------------------
    // I didnt implement this because depending on the screen size the frog is in the middle of the screen or at the very bottom
    
    //Coroutine to place the car at the beginning of the game
    private IEnumerator PlacingTheCharacter()
    {
        float time = 0;
        while (Math.Abs(this.transform.position.y - CHARACTER_Y_POSITION) > 0.02f)
        {
            var curveMovement = movementCurve.Evaluate(time);
            time += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, CHARACTER_Y_POSITION, 0), placingCarSpeed*curveMovement * Time.deltaTime);
            yield return null;
        }
        EventSystem.current.isAllowedToMove = true; //setting this to true allow the user to move the frog side to side and attack
        yield return null;
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
