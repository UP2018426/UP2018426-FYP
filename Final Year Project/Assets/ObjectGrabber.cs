using System.Collections;
using System.Collections.Generic;
using System.Security;
using UltimateXR.Avatar;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    public float inputTriggerValue = 0.0f;  // Set this value from the VR input device
    public float distanceThreshold = 0.5f;  // Set the maximum distance from which an object can be picked up
    public float forceMagnitude;
    public float torqueMagnitude;
    public GameObject selected;

    [SerializeField]
    private bool rightHand;

    [SerializeField]
    private List<GameObject> pickupObjects = new List<GameObject>();

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        // Add all pickupable objects to the list
        //foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Shape"))
        //{
        //    pickupObjects.Add(obj);
        //}
        GenerateList();
    }

    private void FixedUpdate()
    {
        if (rightHand)
        {
            inputTriggerValue = UxrAvatar.LocalAvatarInput.GetInput1D(UltimateXR.Core.UxrHandSide.Right, UltimateXR.Devices.UxrInput1D.Grip);
        }

        else
        {
            inputTriggerValue = UxrAvatar.LocalAvatarInput.GetInput1D(UltimateXR.Core.UxrHandSide.Left, UltimateXR.Devices.UxrInput1D.Grip);

        }

        if (inputTriggerValue > 0.5f)
        {
            GenerateList();
            GameObject nearestObj = null;

            if(selected == null)
            {
                nearestObj = FindNearestObject();

            }
            if (nearestObj != null && Vector3.Distance(nearestObj.transform.position, transform.position) < distanceThreshold)
            {
                // Apply a force towards the VR hand to simulate holding the object
                //Rigidbody rb = nearestObj.GetComponent<Rigidbody>();
                //Vector3 forceDirection = transform.position - nearestObj.transform.position;
                //rb.AddForce(forceDirection.normalized * forceMagnitude, ForceMode.Force);

                gm.TimerStart();
                
                selected = nearestObj;
                if(selected.GetComponent<FixedJoint>() == null)
                {
                    selected.AddComponent<FixedJoint>();
                    selected.GetComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
                }


                // Apply a torque to the object to make it try to match the rotation of the hand
                //Vector3 torqueDirection = transform.rotation.eulerAngles - nearestObj.transform.rotation.eulerAngles;
                //rb.AddTorque(torqueDirection.normalized * torqueMagnitude, ForceMode.Force);
            }
        }
        else
        {
            if(selected != null)
            {
                //selected.GetComponent<FixedJoint>().connectedBody = selected.GetComponent<Rigidbody>();
                Destroy(selected.GetComponent<FixedJoint>());
                selected = null;
            }
        }
    }

    private GameObject FindNearestObject()
    {
        GameObject nearestObj = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject obj in pickupObjects)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (distance < minDistance)
            {
                nearestObj = obj;
                minDistance = distance;
            }
        }

        return nearestObj;
    }

    private void GenerateList()
    {
        pickupObjects.Clear();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Shape"))
        {
            pickupObjects.Add(obj);
        }
    }
}
