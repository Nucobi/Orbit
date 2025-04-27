using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public GameObject PauseMenu;
    public GameObject PauseButton;

    bool isPaused;
    // Start is called before the first frame update
    public static GameManager getInstance()
    {
        return instance;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;

        SetCameraBorder();
        LevelManager.Instance.LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        // Test
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("[TestMode] Starting Level");
            PlayerPrefs.SetString("Level", "Level1-2");
            PlayerPrefs.Save();
            StartScene("Level");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1f;
                PauseMenu.SetActive(false);
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                isPaused = true;
            }
        }
    }


    public void SetCameraBorder()
    {
        Camera cam = Camera.main;
        float screenHeight = 2f * cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;

        Vector2 topLeft = new Vector2(-screenWidth / 2, screenHeight / 2);
        Vector2 topRight = new Vector2(screenWidth / 2, screenHeight / 2);
        Vector2 bottomRight = new Vector2(screenWidth / 2, -screenHeight / 2);
        Vector2 bottomLeft = new Vector2(-screenWidth / 2, -screenHeight / 2);

        // Create EdgeCollider2D
        GameObject edgeBorder = new GameObject("CameraBorder");
        EdgeCollider2D edgeCollider = edgeBorder.AddComponent<EdgeCollider2D>();
        edgeCollider.points = new Vector2[] { topLeft, topRight, bottomRight, bottomLeft, topLeft }; // Closed loop
    }

    public void StartScene(string name)
    {
        SceneManager.LoadScene(name);
        string levelname = PlayerPrefs.GetString("Level");
        Debug.Log("Starting Level: "+levelname);
        Object level = Resources.Load(levelname);
        StartCoroutine(CreateObject(level, 2));
    }

    IEnumerator CreateObject(Object obj, int delay) {
        yield return new WaitForSeconds(delay);

        Instantiate(obj);
    }

    void OpenPauseWindow()
    {

    }
}
