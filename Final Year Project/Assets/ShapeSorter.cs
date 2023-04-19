using System.Collections;
using System.Collections.Generic;
using UltimateXR.Avatar;
using UnityEngine;

public class ShapeSorter : MonoBehaviour
{
    public List<GameObject> objectsToCheck; // The list of GameObjects to check
    public List<GameObject> objectsInside; // The list of GameObjects to check
    private Collider thisCollider;

    [SerializeField]
    private List<GameObject> shapePrefabs;

    public GameManager gm;

    public Material green;

    public Transform spawnLocation;

    private GameObject player;
    private GameObject mainCam;

    public Vector3 playerPosition;
    private Vector3 playerOffset;

    private bool startReset;

    void Start()
    {
        gm = GameManager.Instance;
        thisCollider = GetComponent<Collider>();

        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = player.GetComponentInChildren<Camera>().transform.gameObject;

        if (thisCollider == null)
        {
            Debug.LogError("The main GameObject should have a Collider component attached.");
        }

        if (objectsToCheck.Count == 0)
        {
            Debug.LogWarning("No GameObjects to check. Please add GameObjects to the list.");
        }

        foreach (GameObject obj in objectsToCheck)
        {
            if (obj.GetComponent<Collider>() == null)
            {
                Debug.LogError("All GameObjects to check should have Collider components attached.");
            }
        }
        startReset = false;
    }

    void Update()
    {
        FindObjects();
        objectsInside.Clear();
        foreach (GameObject obj in objectsToCheck)
        {
            if (IsObjectFullyInside(obj))
            {
                objectsInside.Add(obj);
                Destroy(obj);
                //Destroy(obj, 1f);
                //obj.GetComponent<Renderer>().material = green;
                gm.AddShapesCount(1);
                Debug.Log($"{obj.name} is fully inside the main GameObject.");
            }
        }

        if(objectsToCheck.Count < 1) //spawn new 
        {
            int random = Random.Range(0, shapePrefabs.Count - 1);

            Instantiate(shapePrefabs[random], spawnLocation);
        }

        if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UltimateXR.Core.UxrHandSide.Right, UltimateXR.Devices.UxrInputButtons.Button1) || Input.GetKeyDown(KeyCode.Space))
        {
            ResetHead();
        }
    }

    void LateUpdate()
    {
        if (!startReset)
        {
            ResetHead();
            startReset = true;
        }
    }

    bool IsObjectFullyInside(GameObject objectToCheck)
    {
        Collider objectToCheckCollider = objectToCheck.GetComponent<Collider>();

        Bounds thisBounds = thisCollider.bounds;
        Bounds otherBounds = objectToCheckCollider.bounds;

        return thisBounds.Contains(otherBounds.min) && thisBounds.Contains(otherBounds.max);
    }

    void FindObjects()
    {
        objectsToCheck.Clear();

        GameObject[] shapesArray = GameObject.FindGameObjectsWithTag("Shape");
        foreach (GameObject shape in shapesArray)
        {
            objectsToCheck.Add(shape);
        }
    }


    void ResetHead()
    {
        playerOffset = mainCam.transform.localPosition;

        player.transform.localPosition = playerPosition - playerOffset;

        //player.transform.localRotation = Quaternion.identity;

        return;
    }
}
