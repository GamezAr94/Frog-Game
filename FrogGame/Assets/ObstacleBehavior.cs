using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleBehavior : MonoBehaviour
{
    [SerializeField] private AnimationCurve movementCurveObstacle;
    
    public IEnumerator MovingObstacle(float obstacleSpeed, Vector3 target)
    {
        float time = 0;

        while (Vector3.Distance(this.transform.position, target) >= 0.2f)
        {
            var speedObstacleCurve = movementCurveObstacle.Evaluate(time); 
            time += Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, target, obstacleSpeed*speedObstacleCurve * Time.deltaTime);
                
            yield return null;
        }
        Destroy(this.gameObject);
        yield return null;
    }
}
