using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public BoxCollider holeCollider;
    public BoxCollider drumCollider;

    [SerializeField]
    private MeshCollider thisMesh;

    /// <summary>
    /// The issue with this code is that there can only be one toybox
    /// can have more but you'd need to delete the colliders being set in start and just doing it manually
    /// </summary>

    void Start()
    {
        //holeCollider = GameObject.FindWithTag("hole").GetComponent<BoxCollider>(); this doesnt work coz there are several holes.
        /*drumCollider = GameObject.FindWithTag("Drum").GetComponent<BoxCollider>();
        thisMesh = GetComponent<MeshCollider>();*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnTriggerEnter(Collision collision)
    {
        if(collision.transform.tag == ("tag"))
        {
            Debug.Log("object has touched drum");
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
    }
}
