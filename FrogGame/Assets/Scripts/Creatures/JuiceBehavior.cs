using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class JuiceBehavior : MonoBehaviour
{
    [SerializeField]
    AnimationCurve instanceSpeedCurve;
    [SerializeField]
    AnimationCurve leavingSpeedCurve;

    [Tooltip("Speed of the juice, the greater the faster")]
    [Range(0,50)]
    [SerializeField] private int movementSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        float scale = Random.Range(.3f, 1);
        this.transform.localScale = new Vector3(scale,scale,scale);
        StartCoroutine(ParticleMovement());
    }

    private void OnDisable()
    {
        StopCoroutine(ParticleMovement());
    }

    private IEnumerator ParticleMovement()
    {
        float positionX = Random.Range(-1.5f, 1.5f);
        float positionY = Random.Range(.5f, 1.5f);
        Vector3 endingPosition = this.transform.position + new Vector3(positionX, positionY);
        
        float time = 0;
        float curveMovement = 0;
        
        while(this.transform.position != endingPosition){
            curveMovement = instanceSpeedCurve.Evaluate(time);
            time += Time.deltaTime;

            transform.position = Vector3.MoveTowards(this.transform.position, endingPosition,  movementSpeed * curveMovement * Time.deltaTime);
            yield return null;
        }
        
        yield return new WaitForSeconds(Random.Range(.1f, .5f));
        time = 0;
        while(this.transform.position != BodyMovement.FrogPosition){
            curveMovement = leavingSpeedCurve.Evaluate(time);
            time += Time.deltaTime;

            transform.position = Vector3.MoveTowards(this.transform.position, BodyMovement.FrogPosition, movementSpeed * curveMovement * Time.deltaTime);
            yield return null;
        }

        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
