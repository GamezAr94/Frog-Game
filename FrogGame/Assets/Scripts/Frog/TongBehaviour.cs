using UnityEngine;
using UnityEngine.U2D;
using System;

public class TongBehaviour : MonoBehaviour
{
    public bool tongInMouth = true;

    [SerializeField]
    DropsParticlesBehavior dropsParticlesBehavior;
    [SerializeField] HeadBehaviour headBehaviour;
    Rigidbody2D thisObjectRigidBody;
    [SerializeField] Transform tongPivotObject;
    public bool isCatching = true;

    private void Awake()
    {
        GameObject parentObject = this.transform.parent.gameObject;
        thisObjectRigidBody = gameObject.GetComponent<Rigidbody2D>();
        resetingTong(tongPivotObject);
    }
    private void Update()
    {
        if (!tongInMouth)
        {
        //     tongIsGoingBackuards();
        }
    }

    private void tongIsGoingBackuards()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(thisObjectRigidBody.velocity);
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
                resetingTong(tongPivotObject);
                Debug.Log("Body " + isCatching);
                break;
            case "Collectibles":
                // if (isCatching)
                // {
                catchigObjects(other);
                // }
                break;
            default:
                break;
        }
        headBehaviour.playHeadAnimations(tongInMouth);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Body"))
        {
            tongInMouth = false;
            headBehaviour.playHeadAnimations(tongInMouth);
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
    private void resetingTong(Transform pivotObject)
    {
        thisObjectRigidBody.velocity = Vector3.zero;
        thisObjectRigidBody.angularVelocity = 0;
        this.transform.localPosition = pivotObject.localPosition;
        this.transform.rotation = pivotObject.rotation;
    }

}
