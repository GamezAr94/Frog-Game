using System.Collections;
using UnityEngine;

public class FatFly : Creatures
{
    [SerializeField]
    bool stop;

    //This type of movements allows the creatures to have a constant movement speed
    protected override IEnumerator movementCreatureCoroutine(){

        Vector3 endFlyPosition;
        
        float movementSpeed = CreatureTypeStruct.DesiredMovementDuration;

        while(MovementsRemaining > 0){
            endFlyPosition = nextRandomSpot(CreatureTypeStruct.CreatureBoundaries.CoordinatesOfMovementX, CreatureTypeStruct.CreatureBoundaries.CoordinatesOfMovementY);
            LookForward(endFlyPosition);
            while(Vector3.Distance(this.transform.position, endFlyPosition) >= 0.2f){

                if(!stop){
                    this.transform.position = Vector3.MoveTowards(this.transform.position, endFlyPosition, movementSpeed *0.1f);
                }

                yield return null;
            }

            MovementsRemaining--;
            yield return new WaitForSeconds(CreatureTypeStruct.GetDesiredTimeToWaitBetweenMovements());
        }

        endFlyPosition = ExitPoint;
        LookForward(endFlyPosition);

        while(Vector3.Distance(this.transform.position, endFlyPosition) >= 0.2f){
            
            if(!stop){
                    this.transform.position = Vector3.MoveTowards(this.transform.position, endFlyPosition, movementSpeed * 0.5f);
            }

            yield return null;
        } 
        Destroy(this.gameObject);
    }
}