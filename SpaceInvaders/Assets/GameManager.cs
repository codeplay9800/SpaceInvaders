using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int playerLives = 3;

    bool m_gameStarted = false;

    int m_playerScore = 0;

    GameObject m_player= null;
    [SerializeField] GameObject m_playerPrefab;
    [SerializeField] GameObject m_playerplayerSpawnPoint;

    [SerializeField] float m_playerDestroyedPauseTime = 2.0f;

    public int PlayerScore { get { return m_playerScore; } }

    //Events
    public event System.Action GameIsOver;
    public event System.Action<float> PlayerDestroyed;

    public bool GameStarted {  get { return m_gameStarted; } }
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
    }

    void Start()
    {
        StartGame();
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
        Debug.Log("Game Ended with State: " + gameWon);
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
        InGameUIManager.Instance.InitUI(playerLives, 0);
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
