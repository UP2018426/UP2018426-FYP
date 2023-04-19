using System.Collections;
using System.Collections.Generic;
using UltimateXR.Core.Math;
using UltimateXR.Extensions.Unity.Math;
using UltimateXR.Manipulation;
using UnityEngine;

public class smoothSnap : MonoBehaviour
{
    public UxrGrabbableObject grabbableObject;

    public Quaternion targetRotation1;
    public Quaternion targetRotation2;

    public float rotationSpeed;

    public Quaternion lastFrameQuat;
    public Quaternion quaternion;

    void Update()
    {
        float angle1 = Quaternion.Angle(transform.rotation, targetRotation1);
        float angle2 = Quaternion.Angle(transform.rotation, targetRotation2);

        if (!GetComponent<UxrGrabbableObject>().IsBeingGrabbed && GetComponent<UxrGrabbableObject>() != null)
        {
            if (angle1 < angle2)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation1, Time.deltaTime * rotationSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation2, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
