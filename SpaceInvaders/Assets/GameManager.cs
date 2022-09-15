using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int playerLives = 3;

    bool m_gameStarted = false;

    GameState currGameState = GameState.None;

    int m_playerScore = 0;

    GameObject m_player= null;
    [SerializeField] GameObject m_playerPrefab;
    GameObject m_playerplayerSpawnPoint;

    [SerializeField] float m_playerDestroyedPauseTime = 2.0f;

    public int PlayerScore { get { return m_playerScore; } }

    //Events
    public event System.Action GameIsOver;
    public event System.Action<float> PlayerDestroyed;

    public bool GameStarted {  get { return m_gameStarted; } }
    public GameState CurrGameState {  get { return currGameState; } }
    // Start is called before the first frame update

    private void Awake()
    {
        InitSingleton();
    }

    void InitSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Scene Loaded Start Scene
        if(scene.buildIndex == 1)
        {
            StartGame();
        }
        //// End Game
        //if (scene.buildIndex == 2)
        //{
        //    // Show Game Data

        //}
    }

    void InitGameData()
    {
        m_playerScore = 0;
        playerLives = 3;
        currGameState = GameState.None;
        m_playerplayerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
    }

    public void ReduceLife()
    {
        playerLives -= 1;
        InGameUIManager.Instance.ReduceLife();
        if (playerLives<=0)
        {
            GameOver(false);
        }
        else
        {
            PlayerDestroyed?.Invoke(m_playerDestroyedPauseTime);
            Invoke("InitPlayer", m_playerDestroyedPauseTime);
        }
    }

    public void GameOver(bool gameWon)
    {
        // Go to the gameover screen
        // Do something will Game Won
        GameIsOver?.Invoke();
        if(gameWon)
        {
            currGameState = GameState.Won;
        }
        else
        {
            currGameState = GameState.Lost;
        }
        m_gameStarted = false;
        Debug.Log("Game Ended with State: " + gameWon);
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitPlayer()
    {
        m_player = Instantiate(m_playerPrefab, m_playerplayerSpawnPoint.transform.position, m_playerPrefab.transform.rotation);
    }

    public void AddToScore(int a_points)
    {
        //NOte there will be errors when the game finishes and I change to the Final scene since some of the references will be deleted
        m_playerScore += a_points;
        InGameUIManager.Instance.UpdateScore(m_playerScore);
    }

    void StartGame()
    {
        InitGameData();
        InGameUIManager.Instance.InitUI(playerLives, m_playerScore);
        EnemyManager.Instance.SpawnEnemys();
        InitPlayer();
        // Initialise Everything and Start the game
        m_gameStarted = true;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance!= null)
        {
            Instance = null;
        }
    }
}
