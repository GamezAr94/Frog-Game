using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
[SerializeField]
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
    float countDownToStartGame;

    [SerializeField]
    [Range(0, 10)]
    float[] timeMinMax = new float[2];

    [SerializeField]
    CreatureBoundaries creatureSpawnBoundaries;
    List<Creatures> listOfCreaturesToSpawn;
    IEnumerator spawnTimeManager;
    private void Awake()
    {
        creatureSpawnBoundaries.DrawBorderToDebug(creatureSpawnBoundaries.SpawningBorder, Color.white);
        setListOfCreatures();
        spawnTimeManager = SpawnOjectsTimer();
        EventSystem.current.onStartingSpawnCreatures += StartingTheGame;
    }

    private void Start() {
        //add hthe spawnObjectsTimer to another coroutine to control when it should starts
        //StartCoroutine(spawnTimeManager);
    }

    public void StartingTheGame(){
        StartCoroutine(spawnTimeManager);
    }

    public int getTotalNumberOfCreaturesInTheLevel(){
        return listOfCreaturesToSpawn.Count;
    }

    private void setListOfCreatures()
    {
        listOfCreaturesToSpawn = new List<Creatures>();
        getTheListOfElementsAsArray(creaturesToSpawn, listOfCreaturesToSpawn);
        listOfCreaturesToSpawn.Capacity = listOfCreaturesToSpawn.Count;
    }

    private void getTheListOfElementsAsArray(CreatureSpawnDetails[] creatureDetails, List<Creatures> listToAdd)
    {
        Creatures[] temp;
        foreach (CreatureSpawnDetails instance in creatureDetails)
        {
            temp = Enumerable.Repeat(instance.behaviour, instance.totalInstances).ToArray();
            listToAdd.AddRange(temp);
        }
    }

    public IEnumerator SpawnOjectsTimer()
    {
        while (listOfCreaturesToSpawn != null && listOfCreaturesToSpawn.Count > 0)
        {
            Vector3 randomPoint = creatureSpawnBoundaries.getRandomBorderPoint();
            int randomCreatureFromList;

            randomCreatureFromList = Random.Range(0, listOfCreaturesToSpawn.Count - 1);
            Instantiate(listOfCreaturesToSpawn[randomCreatureFromList], randomPoint, Quaternion.identity);
            listOfCreaturesToSpawn.RemoveAt(randomCreatureFromList);
            yield return new WaitForSeconds(Random.Range(timeMinMax[0], timeMinMax[1]));
        }
        EventSystem.current.EndingGame(true);
        StopCoroutine(spawnTimeManager);
    }
}
