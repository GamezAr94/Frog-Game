using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct CreatureSpawnDetails
{
    public int totalInstances;
    public Creatures behaviour;
}
public class SpawnCreatures : MonoBehaviour
{

    [SerializeField]
    CreatureSpawnDetails[] creaturesToSpawn;
    

    [SerializeField]
    [Range(0, 10)]
    [Tooltip("Range between a max and min amount of time where the insects can be instantiated")]
    float[] timeMinMax = new float[2];

    [SerializeField]
    CreatureBoundaries creatureSpawnBoundaries;
    List<Creatures> _listOfCreaturesToSpawn;
    IEnumerator _spawnTimeManager;
    private void Awake()
    {
        creatureSpawnBoundaries.DrawBorderToDebug(creatureSpawnBoundaries.SpawningBorder, Color.white);
        SetListOfCreatures();
        _spawnTimeManager = SpawnObjectsTimer();
        EventSystem.current.onStartingSpawnCreatures += StartingTheGame;
    }
    

    void StartingTheGame(){
        StartCoroutine(_spawnTimeManager);
    }

    public int GetTotalNumberOfCreaturesInTheLevel(){
        return _listOfCreaturesToSpawn.Count;
    }

    private void SetListOfCreatures()
    {
        _listOfCreaturesToSpawn = new List<Creatures>();
        getTheListOfElementsAsArray(creaturesToSpawn, _listOfCreaturesToSpawn);
        _listOfCreaturesToSpawn.Capacity = _listOfCreaturesToSpawn.Count;
    }

    private void getTheListOfElementsAsArray(CreatureSpawnDetails[] creatureDetails, List<Creatures> listToAdd)
    {
        foreach (CreatureSpawnDetails instance in creatureDetails)
        {
            Creatures[] temp = Enumerable.Repeat(instance.behaviour, instance.totalInstances).ToArray();
            listToAdd.AddRange(temp);
        }
    }

    IEnumerator SpawnObjectsTimer()
    {
        while (_listOfCreaturesToSpawn != null && _listOfCreaturesToSpawn.Count > 0)
        {
            yield return new WaitForSeconds(Random.Range(timeMinMax[0], timeMinMax[1]));
            
            Vector3 randomPoint = creatureSpawnBoundaries.getRandomBorderPoint();

             int randomCreatureFromList = Random.Range(0, _listOfCreaturesToSpawn.Count - 1);
            Instantiate(_listOfCreaturesToSpawn[randomCreatureFromList], randomPoint, Quaternion.identity);
            _listOfCreaturesToSpawn.RemoveAt(randomCreatureFromList);
            yield return new WaitForSeconds(Random.Range(timeMinMax[0], timeMinMax[1]));
        }
        EventSystem.current.EndingGame(true);
        StopCoroutine(_spawnTimeManager);
    }
}
