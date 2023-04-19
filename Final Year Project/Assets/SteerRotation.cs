using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerRotation : MonoBehaviour
{
    public float _previousRotation;
    public float _totalRotation;

    private void Update()
    {
        float currentRotation = transform.eulerAngles.z;
        float rotationDelta = currentRotation - _previousRotation;

        // Check if rotation has passed the 0-360 boundary
        if (rotationDelta < -180)
        {
            _totalRotation += rotationDelta + 360;
        }
        else if (rotationDelta > 180)
        {
            _totalRotation += rotationDelta - 360;
        }
        else
        {
            _totalRotation += rotationDelta;
        }

        _previousRotation = currentRotation;

        // The total rotation can be accessed via the _totalRotation field.
    }
}