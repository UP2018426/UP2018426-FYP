using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField]
    private float distance, gapDiameter, camFOV;
    [SerializeField]
    private Camera cam;
    public bool isBlockingView;
    public bool lastBlocking;
    [SerializeField]
    private MeshRenderer mr;

    void Start()
    {
        if(transform.parent != null)
        {
            cam = transform.parent.GetComponent<Camera>();
        }
        //lastBlocking = !isBlockingView;

        mr = transform.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        //if (isBlockingView != lastBlocking)
        //{
        if (isBlockingView)
        {
            mr.enabled = true;
        }
        if(isBlockingView == false)
        {
            mr.enabled = false;
        }
        //lastBlocking = isBlockingView;
        //}

        gapDiameter = 0.5f;
        distance = (cam.transform.position - transform.position).magnitude;

        camFOV = CalculateFOV(gapDiameter, distance);
    }

    public static float CalculateFOV(float diameter, float distance)
    {
        float radians = 2 * (float)Math.Atan((diameter / 2) / distance);
        float degrees = radians * (180 / (float)Math.PI);
        return degrees;
    }
}
