using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{




    private void OnBecameInvisible()
    {
        if (this.gameObject != null && this.gameObject.transform.position.z < 0)
        {
              Destroy(this.gameObject);
        //    //this.gameObject.SetActive(false);
        }


    }




}
