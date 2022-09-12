using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance { get; private set; }

    public GameObject m_livesPrefab;

    public Text ScoreText;
    public GameObject m_livesHorizonTalLayout;
    List<GameObject> m_lifeUIList = new List<GameObject>();

    private void Awake()
    {
        InitSingleton();
    }

    public void InitUI(int a_totalLives, int a_score)
    {
        // InitLives
        for(int i=0; i< a_totalLives; i++)
        {
            GameObject lifeUI = Instantiate(m_livesPrefab, m_livesHorizonTalLayout.transform) as GameObject;
            m_lifeUIList.Add(lifeUI);
        }

        ScoreText.text = "Score: " + a_score.ToString();

    }
    
    public void UpdateScore(int a_score)
    {
        ScoreText.text = "Score: " + a_score.ToString();
    }

    public void ReduceLife()
    {
        if (m_lifeUIList.Count > 0)
        {
            GameObject uiToRemove = m_lifeUIList[m_lifeUIList.Count - 1];
            m_lifeUIList.Remove(uiToRemove);
            Destroy(uiToRemove);
        }
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
