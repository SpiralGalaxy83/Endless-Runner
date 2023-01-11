using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    static public Pool instance;

    [SerializeField] GameObject pickUpCoinEffectPrefab;
    [SerializeField]public  GameObject[] pickUpCoinEffectPool;

    //private GameObject tempObject;



    private void Awake()
    {
        instance = this;
    }




    // Start is called before the first frame update
    void Start()
    {
    
        CreatePool(pickUpCoinEffectPrefab , pickUpCoinEffectPool);

    }



    private void CreatePool(GameObject prefab , GameObject[] pool)
    {

        for (int i = 0; i<pool.Length; i++)
        {        
            
                pool[i] = Instantiate(prefab);
                pool[i].SetActive(false);
        }

       
    }


    public GameObject GetCoinEffect()
    {

        for (int i = 0; i< pickUpCoinEffectPool.Length; i++)
        {
            if (!pickUpCoinEffectPool[i].activeSelf)
            {
                print("INSIDE");
                //pickUpCoinEffectPool[i].SetActive(true);
                return pickUpCoinEffectPool[i];
            }

        }

        return null;

    }


}
