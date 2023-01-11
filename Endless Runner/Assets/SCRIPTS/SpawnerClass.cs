using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerClass : MonoBehaviour
{

    //public enum SpawnObject { Obstacle, CoinX5, CoinX10 ,magnet };


    // Types of Spawning
    // Fixed - Spawn the Specific GameObject
    // Chance - A Chance to spawn the specific Gameobject;
    // Random - Spawn a GameObject from a pool of GameObjects;
    public enum SpawnType {Fixed , Chance , Random}


}
