using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    //Function to move the frog side to side
    public void FrogMovement(float startingPoint, float endingPoint)
    {
        if (startingPoint > endingPoint && this.transform.position.x < 2.0f)//movement to the right
        {
            this.transform.position += new Vector3(2.0f, 0, 0);
        }
        if (startingPoint < endingPoint && this.transform.position.x > -2.0f)//movement to the left
        {
            this.transform.position -= new Vector3(2.0f, 0, 0);
        }
    }
}
