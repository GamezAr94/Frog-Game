using System.Collections;
using UnityEngine;
public abstract class Creatures : MonoBehaviour
{
    [SerializeField]
    Creature creature;
    public Creature Creature { get => creature; }

    [SerializeField]
    private TextMesh pointsText;

    float timeRemaining;
    public float TimeRemaining { get => timeRemaining; set => timeRemaining = value; }

    [SerializeField]
    bool hasExit = false;
    public bool HasExit { get => hasExit; set { hasExit = value; } }

    Vector2 exitPoint;
    public Vector2 ExitPoint { get => exitPoint; set => exitPoint = value; }

    Vector2 moveSpot;
    public Vector2 MoveSpot { get => moveSpot;  set => moveSpot = value; }

    protected float[] availableAreaOfMovementX;
    protected float[] availableAreaOfMovementY;
    
    protected virtual void Awake()
    {
        availableAreaOfMovementX = Creature.creatureBoundaries.coordinatesOfMovementX;
        availableAreaOfMovementY = Creature.creatureBoundaries.coordinatesOfMovementY;
        TimeRemaining = Creature.lifeTime;
    }
    private void Start()
    {
        ExitPoint = Creature.creatureBoundaries.getRandomBorderPoint();
    }

    private void Update()
    {
        movement();
        Exit();
    }

    protected virtual void movement()
    {
    }

    protected virtual void Exit()
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
        }
        else
        {
            HasExit = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name.Equals("BrainAndPoints"))
        {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Boundaries") && HasExit)
        {
            Destroy(this.gameObject);
        }else if(other.CompareTag("TongTip")){
            // this.transform.SetParent(other.transform);
            if(pointsText != null){
                ShowFloatingText();
            }
        }
    }
    private void ShowFloatingText(){
        TextMesh instance = Instantiate(pointsText, this.gameObject.transform.position, Quaternion.identity);
        instance.GetComponent<TextMesh>().text = Creature.value.ToString();
    }
    protected virtual void SetParticleSystem() { }

    protected void lookAtTarget(Vector3 targetSpot)
    {
        Vector3 target = new Vector3(targetSpot.x, targetSpot.y, 0);
        target.x = target.x - this.gameObject.transform.position.x;
        target.y = target.x - this.gameObject.transform.position.y;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) <= 90)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 50f));
            this.transform.localScale = new Vector3(1, -1, 1);
        }
    }
}
