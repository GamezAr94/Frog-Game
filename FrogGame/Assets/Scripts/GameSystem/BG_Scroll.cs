using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BG_Scroll : MonoBehaviour
{
    [SerializeField] private bool instantiateRock;
    [SerializeField][Tooltip("Animation curve to define the speed of the tong when attacking")]
    AnimationCurve BGSpeed;
    
    [SerializeField] private GameObject[] obstaclesSpawnPoint;
    [SerializeField] private GameObject prefabObstacle_Rock;
    
    [SerializeField]
    float speed;

    [SerializeField]
    Renderer rendererComponent;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.onStartMovingTheGame += ParallaxEffectController;
    }  
    
    void ParallaxEffectController(){ 
        StartCoroutine(ParallaxEffect());
    }

    private void OnDestroy()
    {
        StopCoroutine(ParallaxEffect());
        EventSystem.current.onStartMovingTheGame -= ParallaxEffectController;
    }

    IEnumerator ObstacleSpawner()
    {
        while (true)
        {
            GameObject obstacle = Instantiate(prefabObstacle_Rock, new Vector3(0, 11, 0), Quaternion.identity);
            ObstacleBehavior obstacleBehavior = obstacle.GetComponent<ObstacleBehavior>();
            var movingObstacle = obstacleBehavior.MovingObstacle(speed*22.9f, new Vector3(0, -11, 0));
            StartCoroutine(movingObstacle);
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator ParallaxEffect()
    {
        float time = 0;
        float bgCurveMovement = 0;
        
        
        float marker = Time.time;
        
       StartCoroutine(ObstacleSpawner());

        while(true){
            bgCurveMovement = BGSpeed.Evaluate(time);
            time += Time.deltaTime;
            rendererComponent.material.mainTextureOffset = new Vector2(0, (Time.time - marker) * bgCurveMovement * speed);
            yield return null;
        }
    }
}
