using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCamBufferLocal : MonoBehaviour
{
    public int buffer;
    public float bufferFramerate;
    public List<Vector3> localPositions;
    public List<Quaternion> localRotations;

    public GameObject master;
    public GameObject child;

    void Start()
    {
        Debug.Log(transform.GetChild(0).GetComponent<Camera>().farClipPlane);
        transform.GetChild(0).GetComponent<Camera>().farClipPlane = 100f;
        Debug.Log(this.gameObject.name);

        localPositions.Capacity = buffer;
        localRotations.Capacity = buffer;

        for (int i = 0; i < buffer; i++)
        {
            localPositions.Add(new Vector3(0, 0, 0));
        }

        for (int i = 0; i < buffer; i++)
        {
            localRotations.Add(new Quaternion(0, 0, 0, 0));
        }
    }

    void FixedUpdate()
    {
        Time.fixedDeltaTime = 1 / bufferFramerate;

        PositionBuffer();
        RotationBuffer();
    }

    void PositionBuffer()
    {
        child.transform.position = master.transform.parent.TransformPoint(localPositions[0]);
        localPositions.RemoveAt(0);
        localPositions.Add(master.transform.parent.InverseTransformPoint(master.transform.position));
    }

    void RotationBuffer()
    {
        child.transform.rotation = master.transform.parent.rotation * localRotations[0];
        localRotations.RemoveAt(0);
        localRotations.Add(Quaternion.Inverse(master.transform.parent.rotation) * master.transform.rotation);
    }
}
