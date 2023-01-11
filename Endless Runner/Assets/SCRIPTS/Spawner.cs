using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{



    [SerializeField] SpawnerClass.SpawnType spawnType;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float spawnChance;

    private GameObject tempObject;
    


    private void OnEnable()
    {
        TriggerSpawn();
    }



    private void Start()
    {

        //SpawnObject(Test);
    }


    private void TriggerSpawn()
    {

        if (spawnType == SpawnerClass.SpawnType.Fixed)
        {
            Spawn(objectToSpawn);
        }
        else if (spawnType == SpawnerClass.SpawnType.Chance)
        {
            ChanceToSpawn(spawnChance);
        }
        else
        {
            Debug.Log("Random Spawning");
        }



    }








    // Spawns objectToSpawn GameObject
    private void Spawn(GameObject objectToSpawn)
    {
        GameObject temp = Instantiate(objectToSpawn);
        temp.transform.parent = transform.parent;
        temp.transform.position = transform.position;
    }


    // Chance to spawn (0-100 range) objectToSpawn GameObject
    private void ChanceToSpawn(float chance)
    {
        if (Random.Range(0 , 100) < chance)
        {
            Spawn(objectToSpawn);
        }

    }


    // Random Spawn From a pool of Objects
    private void PoolSpawn()
    {

    }




}
