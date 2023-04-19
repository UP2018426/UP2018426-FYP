using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float timeSpent;
    private bool timerIsGoing;

    void Update()
    {
        if (timerIsGoing)
        {
            timeSpent += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            timerIsGoing = true;
        }
    }
}
