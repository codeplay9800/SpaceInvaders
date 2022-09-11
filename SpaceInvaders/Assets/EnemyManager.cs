using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    enum MoveDir { right =0 , left = 1, down = 2};
    public static EnemyManager Instance { get; private set; }

    [SerializeField] GameObject m_enemyPrefab1;
    [SerializeField] GameObject m_enemyPrefab2;
    [SerializeField] GameObject m_enemyPrefab3;

    [SerializeField] GameObject EnemyParent;

    public List<GameObject> EnemyList;
    [SerializeField] float distanceScale;

    [SerializeField] float MoveTime = 1.0f;

    Coroutine m_moveCor = null;
    Vector3 m_moveDir;

    [SerializeField] float m_moveSpace = 1.0f;

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
    // Start is called before the first frame update
    void Start()
    {
        //float horizontalPos = Random.Range(-1, width);
        m_moveDir = new Vector3(1, 0, 0);
    }

    bool CheckMoveDirection()
    {
        if(EnemyParent.transform.position.x == 5)
        {
            //m_moveDir = MoveDir.left;
            m_moveDir = new Vector3(-1, 0, 0);
            return true;
        }
        if (EnemyParent.transform.position.x == -5)
        {
            //m_moveDir = MoveDir.right;
            m_moveDir = new Vector3(1, 0, 0);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("SpawnEnemys")]
    public void SpawnEnemys()
    {
        GameObject EnemyToInstantiate = null;
        for (int j = 0; j < 5; j++)
        {
            if(j==0)
            {
                EnemyToInstantiate = m_enemyPrefab1;
            }
            else if (j == 1 || j == 2)
            {
                EnemyToInstantiate = m_enemyPrefab2;
            }
            if (j == 3)
            {
                EnemyToInstantiate = m_enemyPrefab3;
            }
            if(EnemyToInstantiate == null)
            {
                Debug.LogError("[EnemyManager] Intantiate Enemy Error Abort!");
            }
            for (int i = -5; i <= 5; i++)
            {
                GameObject newEnemy = Instantiate(EnemyToInstantiate, EnemyParent.transform);
                EnemyList.Add(newEnemy);
                newEnemy.transform.position = new Vector3(i * distanceScale, 0, j * distanceScale);
            }
        }
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        m_moveCor = StartCoroutine(Move());
    }

    public void UpdateTime()
    {
        MoveTime = Mathf.Clamp(MoveTime - 0.05f, 0.2f, 1.0f);
    }

    IEnumerator Move()
    {
        while(true)
        {
            EnemyParent.transform.position += m_moveDir * m_moveSpace;
            yield return new WaitForSeconds(MoveTime);
            if (CheckMoveDirection())
            {
                // Move Down
                UpdateTime();
                EnemyParent.transform.position += new Vector3(0, 0, -1) * m_moveSpace;
                yield return new WaitForSeconds(MoveTime);
            }
        }
    }
}
