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
    float velocityAttack = 0.1f;

    [Tooltip("The greater the shorter")]
    [SerializeField][Range (5f, 20f)]
    float rangeOfTong = 12f;

    [Header("Tong Animations")]
    [SerializeField]
    DropsParticlesBehavior dropsParticlesBehavior;

    [SerializeField]
    HeadBehaviour headBehaviour; //Double check this code

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Body":
                break;
            case "Collectibles":
                if (tongMustGoBack) // to catch objects only when tong is going forward
                {
                    catchigObjects (other);
                }
                break;
            default:
                break;
        }
        headBehaviour.playHeadAnimations (_tongInMouth); //Check this code
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Body"))
        {
            headBehaviour.playHeadAnimations (_tongInMouth); // check this code
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

    public IEnumerator spawningTongCoroutine(float distance)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + (transform.up * (distance/rangeOfTong));

        float elapsedTimeGo = 0;

        while (elapsedTimeGo < velocityAttack && !tongMustGoBack)
        {
            if(!tongsIsPaused){
                transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTimeGo / velocityAttack ));
                bodyTongBehaviour.NodesFollowing();
                elapsedTimeGo += Time.deltaTime;
                _tongInMouth = false;
            }
            yield return null;
        }

        float elapsedTimeBack = 0;
        Vector3 currentPosition = transform.position;
         
        while (elapsedTimeBack < elapsedTimeGo)
        {
            tongMustGoBack = true;
            if(!tongsIsPaused){
                transform.position = Vector3.Lerp(currentPosition, tongPivotObject.position, (elapsedTimeBack / elapsedTimeGo));
                bodyTongBehaviour.NodesFollowing();
                elapsedTimeBack += Time.deltaTime;
            }
            yield return null;
        }
        transform.position = tongPivotObject.position;

        bodyTongBehaviour.NodesFollowing();

        tongMustGoBack = false;
        _tongInMouth = true;
    }
}
