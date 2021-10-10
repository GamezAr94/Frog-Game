using System;
using UnityEngine;
using UnityEngine.U2D;
using System.Collections;

public class TongBehaviour : MonoBehaviour
{
    public bool tongsGoesBack = false;
    public float distancePivotAndTong;

    public bool tongInMouth = true;

    float velocityAttack = 20.0f;

    [SerializeField]
    DropsParticlesBehavior dropsParticlesBehavior;

    [SerializeField]
    HeadBehaviour headBehaviour;

    Rigidbody2D thisObjectRigidBody;

    [SerializeField]
    Transform tongPivotObject;

    public bool isCatching = true;

    private void Awake()
    {
        GameObject parentObject = this.transform.parent.gameObject;
        thisObjectRigidBody = gameObject.GetComponent<Rigidbody2D>();
        resetingTong (tongPivotObject);
    }

    private void Update()
    {
        distancePivotAndTong = DistanceBetween2Points2D(tongPivotObject.position, this.transform.position);

        if (!tongInMouth)
        {
            //     tongIsGoingBackuards();
        }
    }

    private void tongIsGoingBackuards()
    {
        Vector3 localVelocity =
            transform.InverseTransformDirection(thisObjectRigidBody.velocity);
        if (localVelocity.x < -.1)
        {
            isCatching = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Body":
                tongInMouth = true;
                isCatching = true;
                resetingTong (tongPivotObject);
                Debug.Log("Body " + isCatching);
                break;
            case "Collectibles":
                // if (isCatching)
                // {
                catchigObjects (other);

                // }
                break;
            default:
                break;
        }
        headBehaviour.playHeadAnimations (tongInMouth);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Body"))
        {
            tongInMouth = false;
            headBehaviour.playHeadAnimations (tongInMouth);
        }
    }

    private void catchigObjects(Collider2D catchedObject)
    {
        if (catchedObject.transform.parent != this.transform)
        {
            dropsParticlesBehavior
                .DropParticles(catchedObject.transform.position);
            catchedObject.transform.parent = this.transform;
        }
    }

    private void resetingTong(Transform pivotObject)
    {
        thisObjectRigidBody.velocity = Vector3.zero;
        thisObjectRigidBody.angularVelocity = 0;
        this.transform.localPosition = pivotObject.localPosition;
        this.transform.rotation = pivotObject.rotation;
    }


    public IEnumerator spawningTongCoroutine(float distance)
    {
        tongInMouth = false;

        Debug.Log("Tong spawned");
        this.GetComponent<Rigidbody2D>().AddForce(transform.up * velocityAttack);

        yield return new WaitUntil( () => distancePivotAndTong >= distance );

        this.GetComponent<Rigidbody2D>().AddForce(-transform.up * (velocityAttack*2));

        yield return null;
    }

    //Function to return the distance between two points in float number
    private float DistanceBetween2Points2D(Vector3 positionOne, Vector3 positionTwo)
    {
        Vector3 distanceCoordinates = positionOne - positionTwo;
        distanceCoordinates.z = 0f;
        float distanceFloat = (distanceCoordinates[0] * distanceCoordinates[0]) + (distanceCoordinates[1] * distanceCoordinates[1]);
        return distanceFloat;
    }
}
