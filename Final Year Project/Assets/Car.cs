using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Manipulation;
using UltimateXR.UI.UnityInputModule.Controls;
using UnityEditor.XR;
//using System.Threading;
using TMPro;
using UnityEngine.SceneManagement;
//using Unity.VisualScripting;

public class Car : MonoBehaviour
{
    private GameManager gm;

    public GameObject[] wheels;

    [SerializeField]
    private List<GameObject> carVisuals;
    [Header("Variables")]
    [SerializeField]
    private bool showCarVisuals, lastShowCarVisuals, referencePoints, lastReferencePoints;
    public Material unlit, lit;

    private Rigidbody rb;

    private GameObject player;
    private GameObject mainCam;

    public Vector3 playerPosition;
    private Vector3 playerOffset;

    public float idleForce;
    public float wheelForce;
    public float maxMotorTorque;
    public float brakeForce;
    public float maxBrakeTorque;
    public float steerForce;
    public float maxAngle;

    public float currentAngle;

    public Vector3 currentQuat;
    public float testSteering;

    public float debugValue1;
    public Quaternion debugValue2;
    public Quaternion debugValue3;
    public bool keyboardInput;

    public GameObject steeringWheel;
    //public float steering;

    [SerializeField]
    private string trig;
    [SerializeField]
    private float gasValue, brakeValue;

    public AudioSource idleAudio;
    private float idlePitch;
    public AnimationCurve idleAnim;

    public AudioSource lowAudio;
    private float lowPitch;
    public AnimationCurve lowAnim;

    public AudioSource highAudio;
    private float highPitch;
    public AnimationCurve highAnim;

    [Range(0.15f,3f)]
    public float debug;

    private gear carGear;

    enum gear
    {
        Stopped,
        Forward,
        Backward
    }

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //gm.carEnd = false;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("CarVisual"))
            {
                carVisuals.Add(child.gameObject);
            }
        }

        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = player.GetComponentInChildren<Camera>().transform.gameObject;
        //mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody>();

        SetCarVisuals(showCarVisuals);
        SetReferencePoints(referencePoints);
        
        ResetHead();
        gm.TimerPause();

        Application.targetFrameRate = 120;
    }

    /*void DetermineGear(float rTrigger, float lTrigger)
    {
        if (rb.velocity.z > -0.01f && rb.velocity.z < 0.01f) //stopped
        {
            //carGear = gear.Stopped;

            
        }
    }*/

    void DetermineVisuals()
    {
        if (lastShowCarVisuals != showCarVisuals)
        {
            SetCarVisuals(showCarVisuals);
            lastShowCarVisuals = showCarVisuals;
        }
        return;
    }

    void DetermineReferencePoints()
    {
        if(lastReferencePoints != referencePoints)
        {
            SetReferencePoints(referencePoints);
        }
        return;
    }

    void Update()
    {
        float rTrigger = UxrAvatar.LocalAvatarInput.GetInput1D(UltimateXR.Core.UxrHandSide.Right, UltimateXR.Devices.UxrInput1D.Trigger);
        float lTrigger = UxrAvatar.LocalAvatarInput.GetInput1D(UltimateXR.Core.UxrHandSide.Left, UltimateXR.Devices.UxrInput1D.Trigger);

        //triggerValue = UxrAvatar.LocalAvatarInput.GetInput1D(UxrAvatar.LocalAvatarInput.Handedness, UltimateXR.Devices.UxrInput1D.Trigger);

        //DetermineGear(rTrigger, lTrigger);

        for (int i = 0; i < wheels.Length; i++)
        {
            //wheels[i].GetComponent<WheelCollider>().motorTorque = (wheelForce * rTrigger);
        }

        for (int i = 0; i < wheels.Length; i++)
        {
            //wheels[i].GetComponent<WheelCollider>().brakeTorque = 0;
            //wheels[i].GetComponent<WheelCollider>().brakeTorque = (brakeForce * lTrigger);
        }

        /*if(carGear == gear.Stopped)
        {
            if(rTrigger > 0.1f)
            {
                carGear = gear.Forward;
            }
            else if(lTrigger > 0.1f)
            {
                carGear = gear.Backward;
            }
        }*/

        //if(carGear == gear.Forward)
        /*if(rb.velocity.z >= 0)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetComponent<WheelCollider>().motorTorque = (wheelForce * rTrigger);
            }

            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetComponent<WheelCollider>().brakeTorque = (brakeForce * lTrigger;
            }
        }*/

        //else if(carGear == gear.Backward)
        /*else if(rb.velocity.z < 0)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetComponent<WheelCollider>().motorTorque = (wheelForce * lTrigger) * Time.deltaTime;
            }

            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetComponent<WheelCollider>().brakeTorque = (brakeForce * rTrigger) * Time.deltaTime;
            }
        }*/

        //this.transform.Find("Gear").GetComponent<TextMeshPro>().text = carGear.ToString();

        //gasValue = rTrigger - lTrigger;
        
        trig = gasValue.ToString("F1");


        ////
        ///VR Throttle
        ////

        //this.GetComponentInChildren<TextMeshPro>().text = rb.velocity.z.ToString("F2");
        this.transform.Find("Speedo").GetComponent<TextMeshPro>().text = (rb.velocity.magnitude * 2.23694f).ToString("F2") + " mph";

        /*for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetComponent<WheelCollider>().motorTorque = idleForce * Time.deltaTime;
            if (wheels[i].GetComponent<WheelCollider>().motorTorque < 0)
                wheels[i].GetComponent<WheelCollider>().motorTorque = 0;
        }*/
        
        /*for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetComponent<WheelCollider>().motorTorque = (wheelForce * gasValue) * Time.deltaTime;
        }

        ////
        ///VR Braking
        ////

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetComponent<WheelCollider>().brakeTorque = (brakeForce * brakeValue) * Time.deltaTime;
        }*/

        ////
        ///VR Steering
        ////

        maxAngle = steeringWheel.GetComponent<UxrGrabbableObject>().RotationAngleLimitsMax.z;

        wheels[0].GetComponent<WheelCollider>().steerAngle = (steeringWheel.GetComponent<SteerRotation>()._totalRotation / maxAngle) * steerForce;
        wheels[1].GetComponent<WheelCollider>().steerAngle = (steeringWheel.GetComponent<SteerRotation>()._totalRotation / maxAngle) * steerForce;

        currentAngle = (steeringWheel.GetComponent<SteerRotation>()._totalRotation / maxAngle) * steerForce;

        if (keyboardInput) // Set steering with debug
        {
            wheels[0].GetComponent<WheelCollider>().steerAngle = testSteering;
            wheels[1].GetComponent<WheelCollider>().steerAngle = testSteering;
        }

        //Update Visuals

        wheels[0].transform.localRotation = Quaternion.Euler(0,currentAngle,0);
        wheels[1].transform.localRotation = Quaternion.Euler(0,currentAngle,0);

        ////
        ///Head Reset
        ////

        if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UltimateXR.Core.UxrHandSide.Right, UltimateXR.Devices.UxrInputButtons.Button1))
        {
            ResetHead();
        }

        ////
        ///Determine Visuals Status
        ////

        DetermineVisuals();

        DetermineReferencePoints();

        /*if(lastShowCarVisuals != showCarVisuals)
        {
            SetCarVisuals(showCarVisuals);
            lastShowCarVisuals = showCarVisuals;
        }*/

        ////
        ///Engine Audio
        ////

        /*idlePitch = 1 + (triggerValue * 0.5f);
        idlePitch = Mathf.Clamp(idlePitch, 1, 1.3f);

        idleAudio.pitch = Mathf.Lerp(idleAudio.pitch, idlePitch, 0.75f);*/


        ///

        idleAudio.volume = idleAnim.Evaluate(debug);
        lowAudio.volume = lowAnim.Evaluate(debug);
        highAudio.volume = highAnim.Evaluate(debug);
        
        idleAudio.pitch = debug;
        lowAudio.pitch = debug - 1;
        highAudio.pitch = debug - 2;

        idleAudio.pitch = Mathf.Clamp(idleAudio.pitch, 0.8f, 1.2f);
        lowAudio.pitch = Mathf.Clamp(lowAudio.pitch, 0.8f, 1.2f);
        highAudio.pitch = Mathf.Clamp(highAudio.pitch, 0.8f, 1.2f);
    }

    void FixedUpdate()
    {
        float rTrigger = UxrAvatar.LocalAvatarInput.GetInput1D(UltimateXR.Core.UxrHandSide.Right, UltimateXR.Devices.UxrInput1D.Trigger);
        float lTrigger = UxrAvatar.LocalAvatarInput.GetInput1D(UltimateXR.Core.UxrHandSide.Left, UltimateXR.Devices.UxrInput1D.Trigger);

        WheelCollider[] wheels = GetComponentsInChildren<WheelCollider>();

        // Apply throttle to all wheels
        foreach (WheelCollider wheel in wheels)
        {
            wheel.motorTorque = rTrigger * maxMotorTorque;
        }

        // Apply brake to all wheels
        if (lTrigger > 0.005f)
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.brakeTorque = lTrigger * maxBrakeTorque;
            }
        }
        else
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.brakeTorque = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            //Debug.DrawRay(wheels[i].transform.position + debugv3, wheels[i].transform.right, Color.red);
            //Debug.DrawRay(wheels[i].GetComponent<WheelCollider>().center + wheels[i].transform.position + debugv3, wheels[i].transform.right, Color.yellow);
        }
        //DetermineVisuals();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillWall"))
        {
            gm.AddOFBHit(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.CompareTag("Start"))
        {
            gm.TimerStart();
        }

        if (other.CompareTag("End"))
        {
            gm.NextScene();
            gm.carEnd = true;
        }

        else
        {
            gm.carEnd = false;
        }
    }

    void ResetHead()
    {
        playerOffset = mainCam.transform.localPosition;

        player.transform.localPosition = playerPosition - playerOffset;

        //player.transform.localRotation = Quaternion.identity;

        return;
    }

    /*void ResetHead()
    {
        Debug.Log("reset");

        playerOffset = mainCam.transform.localPosition;

        // Reset position
        player.transform.localPosition = playerPosition - playerOffset;

        // Reset rotation
        player.transform.localRotation = Quaternion.identity;

        // Make player face forward
        player.transform.forward = mainCam.transform.forward;

        mainCam.transform.eulerAngles = Vector3.zero;

        return;
    }*/

    private void SetCarVisuals(bool active)
    {
        foreach (GameObject vis in carVisuals)
        {
            vis.gameObject.GetComponent<Renderer>().enabled = active;
        }
        /*for (int i = 0; i < carVisuals.Count; i++)
        {
            carVisuals[i].gameObject.SetActive(active);
        }*/
    }

    private void SetReferencePoints(bool points)
    {
        GameObject[] floorObjects = GameObject.FindGameObjectsWithTag("Ground");

        if (points)
        {
            mainCam.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
            for (int i = 0; i < floorObjects.Length; i++)
            {
                floorObjects[i].GetComponent<Renderer>().material = lit;
            }
        }
        else
        {
            mainCam.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
            for (int i = 0; i < floorObjects.Length; i++)
            {
                floorObjects[i].GetComponent<Renderer>().material = unlit;
            }
        }
    }
}
