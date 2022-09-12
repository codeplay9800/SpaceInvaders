using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int playerLives;

    bool m_gameStarted = false;

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
        if(playerLives<=0)
        {
            GameOver(false);
        }
    }

    public void GameOver(bool gameWon)
    {
        // Go to the gameover screen
        // Do something will Game Won
        Debug.Log("Game Ended with State: " + gameWon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        EnemyManager.Instance.SpawnEnemys();
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
