using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singelton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    //scene management variables
    private int sceneIndex;
    private int nextSceneIndex;
    private int maxScenes;

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    public GameObject player;

    [Header("House")]
    [SerializeField] private GameObject worldPrefab;
    public GameObject world;

    [Header("UI")]
    [SerializeField] private GameObject UIManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //set scenes
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = sceneIndex + 1;
        maxScenes = SceneManager.sceneCountInBuildSettings;
        Debug.Log("Scene Loaded: " + SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        //if (nextSceneIndex < maxScenes && Input.GetKeyDown(KeyCode.Space))
        //    GoToNextScene();
    }

    public GameObject AssignWorld()
    {
        //create and return house
        world = Instantiate(worldPrefab);
        return world;
    }

    public GameObject AssignPlayer()
    {
        //load player stats

        //create and return player
        Vector3 playerLocation = new Vector3(25, 1.5f, -26);
        player = Instantiate(playerPrefab, playerLocation, transform.rotation);
        return player;
    }

    #region SceneManagement Functions

    public void MenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayScene()
    {
        SceneManager.LoadScene(2);
    }

    public void StagingScene()
    {
        SceneManager.LoadScene(3);
    }

    public void TimeScene()
    {
        SceneManager.LoadScene(4);
    }

    public void PlayerStatsScene()
    {
        SceneManager.LoadScene(5);
    }

    public void PlayerPrefsScene()
    {
        SceneManager.LoadScene(6);
    }


    private void GoToNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
        sceneIndex = nextSceneIndex;
        nextSceneIndex = sceneIndex + 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
