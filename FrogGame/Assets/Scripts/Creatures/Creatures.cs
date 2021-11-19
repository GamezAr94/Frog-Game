using System.Collections;
using UnityEngine;
public abstract class Creatures : MonoBehaviour
{
    [SerializeField][Tooltip("Field to retrieve the information of the creature to instance")]
    Creature _creatureTypeStruct;
    public Creature CreatureTypeStruct { get => _creatureTypeStruct; }

    public GameObject sprite;

 
    [Tooltip("Variable to store the exit point of the creature")]
    Vector3 _exitPoint;
    public Vector3 ExitPoint { get => _exitPoint; }

    
    [Tooltip("Field to store the coroutine")]
    IEnumerator _movementCreature;
    public IEnumerator MovementCreature { get => _movementCreature; }

    int scoreValue;
    
    [Tooltip("Variable to store the number of remaining movements that the creature has")]
    int _movementsRemaining;
    public int MovementsRemaining { get => _movementsRemaining; set => _movementsRemaining = value; }

    protected virtual void Awake() {
        
        scoreValue = _creatureTypeStruct.ScoreValue;

        _movementsRemaining = _creatureTypeStruct.GetRemainingNumberOfMovements();
        _exitPoint = _creatureTypeStruct.GetExitPoint;
        _movementCreature = movementCreatureCoroutine();

        StartCoroutine(_movementCreature);
    }
    private void Start() {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("TongTip")){
            StopCoroutine(_movementCreature);
            CreatureCaught(other);
        }
        if(this.transform.parent && other.CompareTag("Mouth")){
            EventSystem.current.AddingPoints(scoreValue);
            Destroy(this.gameObject);
        }
    }


    void OnDestroy() {
        if(_movementCreature != null){
            StopCoroutine(_movementCreature);
        }
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

    protected void LookForward(Vector3 nextStop){
        Vector3 dir = nextStop - sprite.transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(sprite.transform.position.x > nextStop.x){
            sprite.transform.localScale = new Vector3(1,1,1);
            //sprite.transform.rotation= Quaternion.AngleAxis(angle -180, Vector3.forward);
        }else{
            sprite.transform.localScale = new Vector3(-1,1,1);
            //sprite.transform.rotation= Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
