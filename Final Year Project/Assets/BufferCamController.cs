using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferCamController : MonoBehaviour
{
    [SerializeField]
    private GameObject cam5, cam10, cam20, camMain;
    private GameManager gm;

    [SerializeField]
    private List<GameObject> mainChildren;

    public ActiveCam camStatus;
    private ActiveCam lastFrameCam;

    public enum ActiveCam
    {
        c5,
        c10,
        c20,
        mainCam
    }

    void Start()
    {
        camMain = this.transform.gameObject;
        //camStatus = ActiveCam.mainCam;
        lastFrameCam = camStatus;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject childObject = transform.GetChild(i).gameObject;
            mainChildren.Add(childObject);
        }

        DetermineStatus();
    }

    void Update()
    {
        if(lastFrameCam != camStatus)
        {
            DetermineStatus();
            if(camStatus == ActiveCam.mainCam)
            {
                cam5.SetActive(false);
                cam10.SetActive(false);
                cam20.SetActive(false);
            }
            lastFrameCam = camStatus;
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            DisableLagCams();
        }*/
    }
    void DisableLagCams()
    {
        cam5.SetActive(false);
        cam10.SetActive(false);
        cam20.SetActive(false);
        CamMainStatus(false);
    }

    void DetermineStatus()
    {
        DisableLagCams();
        if(camStatus == ActiveCam.c5)
        {
            EnableCam5();
        }
        if(camStatus == ActiveCam.c10)
        {
            EnableCam10();
        }
        if(camStatus == ActiveCam.c20)
        {
            EnableCam20();
        }
        if(camStatus == ActiveCam.mainCam)
        {
            DisableLagCams();
            CamMainStatus(true);
        }
    }

    void EnableCam5()
    {
        cam5.SetActive(true);
    }

    void EnableCam10()
    {
        cam10.SetActive(true);
    }

    void EnableCam20()
    {
        cam20.SetActive(true);
    }

    void CamMainStatus(bool on)
    {
        cam5.SetActive(false);
        cam10.SetActive(false);
        cam20.SetActive(false);

        camMain.GetComponent<Camera>().enabled = on;

        for (int i = 0; i < mainChildren.Count; i++)
        {
            mainChildren[i].SetActive(on);
        }
    }
}
