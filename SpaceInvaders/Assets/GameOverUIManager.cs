using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{

    public Text m_scoreText;
    public Text m_gameStateText;
    // Start is called before the first frame update

    void Start()
    {
        ShowUI();
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ShowUI();
    }

    void ShowUI()
    {
        m_scoreText.text = "Score: " + GameManager.Instance.PlayerScore.ToString();
        if(GameManager.Instance.CurrGameState == GameState.Won)
        {
            m_gameStateText.text = "You Won!";
        }
        if (GameManager.Instance.CurrGameState == GameState.Won)
        {
            m_gameStateText.text = "You Lose :(";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
