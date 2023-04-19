using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShapBox : MonoBehaviour
{
    //public Collider bottomCollider, topCollider;

    public List<GameObject> objectsTouching = new List<GameObject>();
    public List<Collider> objectsColliders = new List<Collider>();
    public List<Bounds> objectsBounds = new List<Bounds>();


    public List<GameObject> objectsContained = new List<GameObject>();    

    void Start()
    {
        
    }

    void Update()
    {
        objectsContained.Clear();
        objectsColliders.Clear();
        objectsBounds.Clear();
        for (int i = 0; i < objectsTouching.Count; i++)
        {
            objectsColliders.Add(objectsTouching[i].GetComponent<Collider>());
            objectsBounds.Add(objectsTouching[i].GetComponent<Bounds>());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        objectsTouching.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        objectsTouching.Remove(other.gameObject);
    }


}
