using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    static public LevelGenerator instance;

    public GameObject levelObject;      // The parent object for level tiles

    public float motionSpeed;           // the speed of the level motion
    public float spawnTimestep;         // Each Timestep spawn the next piece
    public int numOfInitialTiles;       // Number of Initial Tiles to Spawn
    //public int numOfMaxTiles;           // Max Tiles activated atm (performance)
    public float generationDelay;       // A delay before Generation starts 


    private int numOfTilesSpawned;       // The number of active tiles at the moment
    private GameObject tileToSpawn;      // Next tile to be spawned 
    private bool isGenerating;           // Is Generating tiles atm

    //private int destroyCounter;          // Counter to Destroy Tiles

    public GameObject[] initialTilesPool;                               // Initial Tiles to be spawned
    public GameObject[] tilesPool;                                      // The pool of the tiles will be spawned
    public List<GameObject> spawnedTiles = new List<GameObject>();      // The tiles that have been spawned




    private void SubscribeEvents()
    {
        //print("ENABLED");
        GameEvents.instance.OnStartRound += StartGeneration;
        GameEvents.instance.OnGameOver += EndRound;
    }

    private void UnSubscribeEvents()
    {
        //print("DISABLED");
        GameEvents.instance.OnStartRound -= StartGeneration;
        GameEvents.instance.OnGameOver -= EndRound;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }




    private void Awake()
    {
        instance = this;
        
    }



    private void Start()
    {

        SpawnInitialTiles();
    }



    private void StartGeneration()
    {
        InvokeRepeating("SpawnTile", generationDelay, spawnTimestep);
        isGenerating = true;
    }



    void FixedUpdate()
    {

        // Motion (the whole Level)
        if (isGenerating)
        {
            levelObject.transform.position = new Vector3(levelObject.transform.position.x, levelObject.transform.position.y, levelObject.transform.position.z - motionSpeed);
        }

    }


    private void SpawnInitialTiles()
    {
        // Initial Tiles to be Spawned
        for (int i = 0; i < initialTilesPool.Length; i++)
        {
            tileToSpawn = Instantiate(Resources.Load("PREFABS/" + initialTilesPool[i].name) as GameObject);
            tileToSpawn.transform.SetParent(levelObject.transform);
            tileToSpawn.transform.localPosition = new Vector3(tileToSpawn.transform.localPosition.x, 0, tileToSpawn.transform.position.z + (numOfTilesSpawned * 60));
            spawnedTiles.Add(tileToSpawn);
            numOfTilesSpawned += 1;

        }
    }



    private void SpawnTile()
    {
        
        tileToSpawn = tilesPool[Random.Range(0, tilesPool.Length)];

        tileToSpawn = Instantiate(Resources.Load("PREFABS/"+tileToSpawn.name) as GameObject);
      
        tileToSpawn.transform.SetParent(levelObject.transform);
        tileToSpawn.transform.localPosition = new Vector3(tileToSpawn.transform.localPosition.x, 0, tileToSpawn.transform.position.z + (numOfTilesSpawned * 60));

        spawnedTiles.Add(tileToSpawn);
        numOfTilesSpawned += 1;

        //if (numOfTilesSpawned >= numOfMaxTiles)
        //{
        //    GameObject temp = spawnedTiles[spawnedTiles.Count - numOfMaxTiles];
        //    spawnedTiles.RemoveAt(spawnedTiles.Count - numOfMaxTiles);

        //    Destroy(temp);
        //}

    }


    private void EndRound()
    {
        StartCoroutine(EndRoundCoroutine());
    }

    private IEnumerator EndRoundCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<LevelGenerator>().enabled = false;
        GetComponent<LevelGenerator>().CancelInvoke();

    }


}
