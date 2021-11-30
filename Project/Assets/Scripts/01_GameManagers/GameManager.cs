using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance != null)   Destroy(gameObject);
        else                    _instance = this;
    }
    #endregion

    //scene management variables
    private int sceneIndex;
    private string sceneName;

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    [HideInInspector] public GameObject player;

    [Header("House")]
    [SerializeField] private GameObject worldPrefab;
    [HideInInspector] public GameObject world;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //set scenes
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneName = SceneManager.GetActiveScene().name;
        
        //switch UI to current scene
        UIManager.UIManagerInstance.SwitchSceneUI(sceneName);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject AssignWorld()
    {
        //create and return house
        world = Instantiate(worldPrefab);
        return world;
    }

    public GameObject AssignPlayer(Transform playerStartPos)
    {
        //load player stats

        //create and return player
        player = Instantiate(playerPrefab, playerStartPos.position, transform.rotation);
        return player;
    }

    #region SceneManagement Functions

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayScene()
    {
        SceneManager.LoadScene(1);
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

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
