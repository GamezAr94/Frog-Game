using System;
using UnityEngine;
using UnityEngine.U2D;
using System.Collections;

public class TongBehaviour : MonoBehaviour
{
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

    [Tooltip("The greater the slower")]
    [SerializeField][Range (0.1f, 2f)]
    float desiredTongAttackDuration = 0.1f;

    [Tooltip("The greater the shorter")]
    [SerializeField][Range (5f, 20f)]
    float rangeOfTong = 12f;

    [Header("Tong Animations")]
    [SerializeField]
    DropsParticlesBehavior dropsParticlesBehavior;

    IEnumerator spawnTong;

    private void Awake() {
        transform.position = tongPivotObject.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Body":
                break;
            case "Collectibles":
                if (!tongMustGoBack) // to catch objects only when tong is going forward
                {
                    catchigObjects (other);
                }
                break;
            default:
                break;
        }
    }

    private void catchigObjects(Collider2D catchedObject)
    {
        if (catchedObject.transform.parent != this.transform)
        {
            dropsParticlesBehavior.DropParticles(catchedObject.transform.position);
            catchedObject.transform.parent = this.transform;
        }
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
