using System.Collections;
using UnityEngine;

[System.Serializable]
[SerializeField]
public struct BorderSides
{
    public Vector3 point1;
    public Vector3 point2;
}

[CreateAssetMenu(fileName = "Creature Boundaries", menuName = "Boundaries/CreaturesBoundaries")]
public class CreatureBoundaries : ScriptableObject
{
    [SerializeField][Tooltip("Screen Area")]
    float[] coordinatesOfMovementX, coordinatesOfMovementY;  
    public float[] CoordinatesOfMovementY { get => coordinatesOfMovementY; }
    public float[] CoordinatesOfMovementX { get => coordinatesOfMovementX; } 
    

    [SerializeField]
    private BorderSides[] spawningBorder;

    public BorderSides[] SpawningBorder { get => spawningBorder; }

    public void DrawBorderToDebug(BorderSides[] border, Color color){
        foreach(var side in border){
            Debug.DrawLine(side.point1, side.point2, color, 100f);
        }
    }
    public Vector3 getRandomBorderPoint(){
        int randomSide = Random.Range(0, SpawningBorder.Length);
        float randomPositionX = Random.Range(SpawningBorder[randomSide].point1.x,SpawningBorder[randomSide].point2.x);
        float randomPositionY = Random.Range(SpawningBorder[randomSide].point1.y,SpawningBorder[randomSide].point2.y);
        return new Vector3(randomPositionX, randomPositionY, 0);
    }
}
