using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeScript : MonoBehaviour
{
    private bool hasBeenHit;
    private GameManager gm;

    private void OnCollisionEnter(Collision collision)
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if(collision.gameObject.CompareTag("Car") && hasBeenHit != true)
        {
            hasBeenHit = true;
            gm.AddConeHit(1);
            Debug.Log("ConeHit");
        }
    }
}
