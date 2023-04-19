using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class VRCamBuffer : MonoBehaviour
{
    /// <summary>
    /// This script is designed to make one object mimic another.
    /// It does this by storing an array (or maybe list) of transform data and applying it to a child object. 
    /// 
    /// Child object should not actually be a child
    /// Script needs to be on the designated master object
    /// </summary>

    public int buffer;
    public float bufferFramerate;
    public List<Vector3> positions;
    public List<Quaternion> rotations;

    public GameObject master;
    public GameObject child;

    void Start()
    {
        //if(GameObject.)

        Debug.Log(transform.GetChild(0).GetComponent<Camera>().farClipPlane);
        transform.GetChild(0).GetComponent<Camera>().farClipPlane = 100f;
        Debug.Log(this.gameObject.name);
        //master = this.gameObject;
        positions.Capacity = buffer;
        rotations.Capacity = buffer;

        //Time.fixedDeltaTime = 1 / bufferFramerate;

        for (int i = 0; i < buffer; i++)                // Generate a default position buffer. Currently will start at 0,0,0
        {
            positions.Add(new Vector3(0, 0, 0));
        }   // Generates starting Position Buffer
        
        for (int i = 0; i < buffer; i++)                // Generate a default rotation buffer. Currently will start at 0,0,0
        {
            rotations.Add(new Quaternion(0,0,0,0));
        }   // Generates starting Rotation Buffer
    }

    void FixedUpdate()
    {
        //Setting the FPS
        Time.fixedDeltaTime = 1 / bufferFramerate;

        PositionBuffer();
        RotationBuffer();
    }

    void PositionBuffer() // Provides a buffer for transform.position
    {
        child.transform.position = positions[0];    // Sets child position to next in line
        positions.RemoveAt(0);                      // Pops off the lead position
        positions.Add(master.transform.position);   // Add current position to the list
    }

    void RotationBuffer() // Provides a buffer for transform.rotation (quaternion)
    {
        child.transform.rotation = rotations[0];    // Sets child rotation to next in line
        rotations.RemoveAt(0);                      // Pops off the lead rotation
        rotations.Add(master.transform.rotation);   // Add current rotation to the list
    }
}
