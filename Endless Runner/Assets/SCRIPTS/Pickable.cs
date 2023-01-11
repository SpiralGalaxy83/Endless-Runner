using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {

        EnablePickUpEffect();
        GameEvents.instance.CoinPickUp();
        

    }


    private void EnablePickUpEffect()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }


}
