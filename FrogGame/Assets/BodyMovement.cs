using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    public void FrogMovement(float startingPoint, float endingPoint){
        if(startingPoint > endingPoint && this.transform.position.x < 2.0f){
            this.transform.position += new Vector3(2.0f,0,0);
            Debug.Log("Right");
        }
        if(startingPoint < endingPoint && this.transform.position.x > -2.0f){
            this.transform.position -= new Vector3(2.0f,0,0);
            Debug.Log("Left");
        }
    }
}
