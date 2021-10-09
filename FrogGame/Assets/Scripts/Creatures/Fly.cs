using UnityEngine;

public class Fly : Creatures
{
    protected override void Awake()
    {
        base.Awake();
        MoveSpot = nextRandomSpot(availableAreaOfMovementX, availableAreaOfMovementY);
        lookAtTarget(MoveSpot);
    }
    protected override void movement()
    {
        if (transform.parent == null)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, MoveSpot, Creature.movementSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, MoveSpot) < 0.2f)
            {
                MoveSpot = nextRandomSpot(availableAreaOfMovementX, availableAreaOfMovementY);

                lookAtTarget(MoveSpot);
            }
        }
    }

    private Vector2 nextRandomSpot(float[] minMaxCoordinatesX, float[] minMaxCoordinatesY)
    {
        if (HasExit)
        {
            return ExitPoint;
        }
        else
        {
            return new Vector2(Random.Range(minMaxCoordinatesX[0], minMaxCoordinatesX[1]), Random.Range(minMaxCoordinatesY[0], minMaxCoordinatesY[1]));
        }
    }
}
