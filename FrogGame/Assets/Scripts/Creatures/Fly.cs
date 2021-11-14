using System.Collections;
using UnityEngine;

public class Fly : Creatures
{
    [SerializeField]
    bool stop;

    //This type of movements allows the creatures to move fast if the distance is long and slow if the distance is short
    protected override IEnumerator movementCreatureCoroutine(){

        Vector3 startFlyPosition;
        Vector3 endFlyPosition;
        float elapsedTime;
        float desiredMovementDuration;

        while(MovementsRemaining > 0){

            startFlyPosition = this.transform.position;
            endFlyPosition = nextRandomSpot(CreatureTypeStruct.CreatureBoundaries.coordinatesOfMovementX, CreatureTypeStruct.CreatureBoundaries.coordinatesOfMovementY);
            elapsedTime = 0.0f;
            desiredMovementDuration = CreatureTypeStruct.DesiredMovementDuration;

            while(elapsedTime <= desiredMovementDuration){

                if(!stop){
                    elapsedTime += Time.deltaTime;
                    float percentageCompleted = elapsedTime / desiredMovementDuration;
                    this.transform.position = Vector3.Lerp(startFlyPosition, endFlyPosition, percentageCompleted);
                }

                yield return null;
            }

            MovementsRemaining--;
            yield return new WaitForSeconds(CreatureTypeStruct.GetDesiredTimeToWaitBetweenMovements());
        }

        startFlyPosition = this.transform.position;
        endFlyPosition = ExitPoint;
        elapsedTime = 0.0f;
        desiredMovementDuration = CreatureTypeStruct.DesiredMovementDuration;

        while(elapsedTime <= desiredMovementDuration){
            
            if(!stop){
                elapsedTime += Time.deltaTime;
                float percentageCompleted = elapsedTime / desiredMovementDuration;
                this.transform.position = Vector3.Lerp(startFlyPosition, endFlyPosition, percentageCompleted);
            }

            yield return null;
        } 
    }
}
