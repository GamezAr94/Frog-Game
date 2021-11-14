using System.Collections;
using UnityEngine;
public abstract class Creatures : MonoBehaviour
{
    [SerializeField][Tooltip("Field to retrieve the information of the creature to instance")]
    Creature _creatureTypeStruct;
    public Creature CreatureTypeStruct { get => _creatureTypeStruct; }

 
    [Tooltip("Variable to store the exit point of the creature")]
    Vector3 _exitPoint;
    public Vector3 ExitPoint { get => _exitPoint; }

    
    [Tooltip("Field to store the coroutine")]
    IEnumerator _movementCreature;
    public IEnumerator MovementCreature { get => _movementCreature; }

    
    [Tooltip("Variable to store the number of remaining movements that the creature has")]
    int _movementsRemaining;
    public int MovementsRemaining { get => _movementsRemaining; set => _movementsRemaining = value; }


    [Tooltip("Event to be called when the OnDestroy() method is called")]
    System.Action whenDestroyed;

    protected virtual void Awake() {

        _movementsRemaining = _creatureTypeStruct.GetRemainingNumberOfMovements();
        _exitPoint = _creatureTypeStruct.GetExitPoint;
        _movementCreature = movementCreatureCoroutine();

        StartCoroutine(_movementCreature);
        whenDestroyed = () => { StopCoroutine(MovementCreature); };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("TongTip")){
            StopCoroutine(_movementCreature);
            CreatureCaught(other);
            whenDestroyed = DestroyedOnMouth;
        }
        if(this.transform.parent == null && other.CompareTag("Boundaries")){
            Destroy(this.gameObject);      
        }
        if(this.transform.parent && other.CompareTag("Mouth")){
            Destroy(this.gameObject);
        }
    }

    protected void DestroyedOnMouth(){
        Debug.Log("Function to activate the logic when the creature has been caught and destroy");
        // Logic to do when the creature is in the mouth
        // - play a sound
        // - send an evento to update the score UI
        // - play an animation 
    }

    void OnDestroy() {
        whenDestroyed();
    }

    protected virtual IEnumerator movementCreatureCoroutine(){
        yield return null;
    }
    
    protected void CreatureCaught(Collider2D caughtObject)
    {
        this.transform.parent = caughtObject.transform;
        //Some logic to do when the creature is caught
        // - change the sprite
        // - play a sound
        // - play some particles effects
        // - show text of the points gained
    }

    protected Vector2 nextRandomSpot(float[] minMaxCoordinatesX, float[] minMaxCoordinatesY)
    {
        return new Vector2(Random.Range(minMaxCoordinatesX[0], minMaxCoordinatesX[1]), Random.Range(minMaxCoordinatesY[0], minMaxCoordinatesY[1]));
    }
}
