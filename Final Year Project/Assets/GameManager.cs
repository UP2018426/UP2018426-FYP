using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int framerate = 120;

    public static GameManager Instance;

    [SerializeField]
    private int coneHit, barrierHit, outOfBounds, shapes;

    public TextMeshPro fpsCounterText;

    private float deltaTime = 0.0f;

    public float timeSpent;
    [SerializeField]
    private bool timerIsGoing;
    public bool carEnd;
    public List<float> timeHistory;

    [SerializeField]
    string fileName;

    //[System.Serializable]
    [SerializeField]
    public float[,] data = new float[10,10];

    void Update()
    {
        if(fpsCounterText != null)
        {
            fpsCounterText = GameObject.FindGameObjectWithTag("FPSCounter").GetComponent<TextMeshPro>();
        }

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        //fpsCounterText.text = string.Format("{0:0.} FPS", fps);

        if (timerIsGoing)
        {
            if(shapes >= 10)
            {
                NextScene();
            }
            /*if (carEnd == true)
            {
                carEnd = false;
                Debug.Log("Shite");
                NextScene();
            }*/
            timeSpent += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.P))
        {
            GenerateCSV();
        }
    }

    private void Awake()
    {
        Application.targetFrameRate = 0;
        Debug.Log("AHHH");
        Application.targetFrameRate = framerate;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        TimerPause();
    }

    //timer

    public void TimerStart()
    {
        timerIsGoing = true;
    }

    public void TimerPause()
    {
        timerIsGoing = false;
    }

    public void TimerReset()
    {
        timeHistory.Add(timeSpent);
        TimerPause();
        timeSpent = 0;
    }

    public void NextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1)
        {
            //This was last scene end test
            Debug.Log("End Of Test");
            GenerateCSV();
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        data[SceneManager.GetActiveScene().buildIndex, 0] = timeSpent;
        data[SceneManager.GetActiveScene().buildIndex, 1] = coneHit;
        data[SceneManager.GetActiveScene().buildIndex, 2] = barrierHit;
        data[SceneManager.GetActiveScene().buildIndex, 3] = outOfBounds;
        data[SceneManager.GetActiveScene().buildIndex, 4] = shapes;

        TimerReset();
        ResetConeHit();
        ResetBarrierHit();
        ResetOFBHit();
        ResetShapeCount();
        //Debug.Log("Sakdsalkdn");
        carEnd = false;

        return;
    }

    //adding score

    public void AddConeHit(int value)
    {
        coneHit += value;
        return;
    }

    public void AddBarrierHit(int value)
    {
        barrierHit += value;
        return;
    }

    public void AddOFBHit(int value)
    {
        outOfBounds += value;
        return;
    }

    public void AddShapesCount(int value)
    {
        shapes += value;
        return;
    }

    //getting score

    public int GetConeHit()
    {
        return coneHit;
    }

    public int GetBarrierHit()
    {
        return barrierHit;
    }

    public int GetOFBHit()
    {
        return outOfBounds;
    }

    public int GetShapesCount()
    {
        return shapes;
    }

    //reset score

    public void ResetConeHit()
    {
        coneHit = 0;
        return;
    }

    public void ResetBarrierHit()
    {
        barrierHit = 0;
        return;
    }

    public void ResetOFBHit()
    {
        outOfBounds = 0;
        return;
    }

    public void ResetShapeCount()
    {
        shapes = 0;
        return;
    }

    public void GenerateCSV()
    {
        // persistent data path
        string path = Application.persistentDataPath + "/" + fileName + DateTime.Now.ToString().Replace(':', '-');

        // creates new dir if it doesnt already exist
        string directoryPath = Path.GetDirectoryName(path);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        StreamWriter writer = new StreamWriter(path);

        // loop rows of the array
        for (int i = 0; i < data.GetLength(0); i++)
        {
            string line = "";

            // loop columms of the array
            for (int j = 0; j < data.GetLength(1); j++)
            {
                line += data[i, j].ToString();

                if (j < data.GetLength(1) - 1)
                {
                    line += ",";
                }
            }

            writer.WriteLine(line);
        }

        writer.Close();
        Debug.Log("Output file path: " + path);

        return;
    }

}

