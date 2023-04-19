using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchAngle : MonoBehaviour
{
    public Quaternion angle;

    public Car car;
    
    void Update()
    {

        angle = new Quaternion(transform.rotation.x, transform.rotation.y, car.currentAngle, transform.rotation.w);
        transform.rotation = angle;
    }
}
