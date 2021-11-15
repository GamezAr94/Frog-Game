
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureDetails", menuName = "Creature/CreatureDetails")]
public class Creature : ScriptableObject
{
    [Header("Values Of Creature")]
    
    [SerializeField][Range(0, 50)]
    [Tooltip("This is the Score value that it will give to the player after catching it")]
    int _scoreValue;
    public int ScoreValue { get => ScoreValue; }

    [Range(0, 3)][SerializeField]
    [Tooltip("the numbers of hits it can receive before being caught")]    
    int _strength;
    public int Strength { get => _strength; }

    [Header("Limits Of Movement")]

    [SerializeField]
    [Tooltip("Setting the limits where the creature can fly, also this boundaries set the starting and ending point")]
    CreatureBoundaries _creatureBoundaries;
    public CreatureBoundaries CreatureBoundaries { get => _creatureBoundaries; }


    [Header("Movement Settings - (2 items per array)")]

    [Range(0, 5)][SerializeField]
    [Tooltip("the amount of time the creature will wait until move to the next point, the Min and Max amount")]
    float[] _desiredWaitToMoveTime;
    public float[] DesiredWaitToMoveTime { get => _desiredWaitToMoveTime; }


    [Range(0, 35)][SerializeField]
    [Tooltip("the number of movements before to exit the scene, The min and Max num of movements")]
    int[] _rangeOfMovementsOnScene;
    public int[] RangeOfMovementsOnScreen { get => _rangeOfMovementsOnScene; }
    

    [SerializeField][Range(0, 10)]
    [Tooltip("Desired Movement duration between each point, the starting and ending point")]
    float _desiredMovementDuration;
    public float DesiredMovementDuration { get => _desiredMovementDuration; }

    public Vector3 GetExitPoint { get => _creatureBoundaries.getRandomBorderPoint(); }
    
    public int GetRemainingNumberOfMovements(){ 
        int movements = 0;
        if(_rangeOfMovementsOnScene.Length == 1){
            movements = _rangeOfMovementsOnScene[0];
        }
        else if(_rangeOfMovementsOnScene.Length >= 2){
            movements = Random.Range(_rangeOfMovementsOnScene[0], _rangeOfMovementsOnScene[1]);
        }
        else{
            Debug.LogError("You must set the range of movements, current length of _rangeOfMovements variable is: " + _rangeOfMovementsOnScene.Length);
        }
        return movements;
    }

    public float GetDesiredTimeToWaitBetweenMovements(){ 
        float time = 0.0f;
        if(_desiredWaitToMoveTime.Length == 1){
            time = _desiredWaitToMoveTime[0];
        }
        else if(_desiredWaitToMoveTime.Length >= 2){
            time = Random.Range(_desiredWaitToMoveTime[0], _desiredWaitToMoveTime[1]);
        }
        return time;
    }


}
