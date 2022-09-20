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
    [SerializeField] GameObject m_enemyPrefab4;
    public GameObject m_enemyBullet;


    GameObject m_bossShip = null;
    bool canSpawnBoss = true;

    [SerializeField] GameObject m_bossLeftSp;
    [SerializeField] GameObject m_bossRightSp;
    float m_bossSpawnTime = 1.0f;

    [SerializeField] GameObject EnemyParent;

    public List<Alien> EnemyList;
    [SerializeField] float distanceScale;

    [SerializeField] float MoveTime = 1.0f;

    Coroutine m_moveCor = null, m_shootCor = null, m_bossCor = null;
    Vector3 m_moveDir;

    [SerializeField] float m_moveSpace = 1.0f;

    float AlientShootTime = 1.0f;

    public int m_enemyInGrid_X= 11;
    public int m_enemyInGrid_Y = 5;

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
        InitBossSpawnTime();
        InitALienShootTime();
        GameManager.Instance.GameIsOver += GameOver;
        GameManager.Instance.PlayerDestroyed += PlayerDestroyed ;
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

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    void InitBossSpawnTime()
    {
        m_bossSpawnTime = Random.Range(10, 15);
    }

    void InitALienShootTime()
    {
        AlientShootTime = Random.Range(0.1f, 2.0f);
    }

    void InstantiateBoss()
    {
        Debug.Log("Spawning Boss time:" + m_bossSpawnTime);
        int spawnPos = Random.Range(0, 1);
        
            // Spawn from left
            if (spawnPos == 0)
            {
                m_bossShip = Instantiate(m_enemyPrefab4, m_bossLeftSp.transform.position, m_enemyPrefab4.transform.rotation);
                m_bossShip.GetComponent<AlienBoss>().m_moveDir = Vector3.right;
            }

            // Spawn from Right
            else if (spawnPos == 1)
            {
                m_bossShip = Instantiate(m_enemyPrefab4, m_bossRightSp.transform.position, m_enemyPrefab4.transform.rotation);
                m_bossShip.GetComponent<AlienBoss>().m_moveDir = Vector3.left;
            }
        SoundManager.Instance.PlayBossSound();
        canSpawnBoss = false;
    }

    [ContextMenu("SpawnEnemys")]
    public void SpawnEnemys()
    {
        GameObject EnemyToInstantiate = null;

        // Broken Logic but its fine
        int elements = m_enemyInGrid_X / 2;
        for (int j = 0; j < m_enemyInGrid_Y; j++)
        {
            if(j==0)
            {
                EnemyToInstantiate = m_enemyPrefab1;
            }
            else if (j == 1 || j == 2)
            {
                EnemyToInstantiate = m_enemyPrefab2;
            }
            else if (j >= 3)
            {
                EnemyToInstantiate = m_enemyPrefab3;
            }
            if(EnemyToInstantiate == null)
            {
                Debug.LogError("[EnemyManager] Intantiate Enemy Error Abort!");
            }
            for (int i = -elements; i <= elements; i++)
            {
                GameObject newEnemy = Instantiate(EnemyToInstantiate, EnemyParent.transform);
                EnemyList.Add(newEnemy.GetComponent<Alien>());
                newEnemy.transform.localPosition = new Vector3(i * distanceScale, 0, j * distanceScale);
                newEnemy.GetComponent<Alien>().initEnemy(j); 
            }
        }
        InitEnemyBehaviour();
    }

    public void InitEnemyBehaviour()
    {
        m_moveCor = StartCoroutine(Move());
        m_shootCor = StartCoroutine(RandomShoot());
        m_bossCor = StartCoroutine(SpawnBoss());
    }

    public void ShootEnemy()
    {
        int randEnemy = Random.Range(0, EnemyList.Count);
        Transform currEnemyTrans = EnemyList[randEnemy].transform;
        Vector3 spawnPos = currEnemyTrans.transform.position + currEnemyTrans.transform.forward * currEnemyTrans.transform.localScale.z;
        // instantiate the Bullet
        Instantiate(m_enemyBullet, spawnPos, currEnemyTrans.transform.rotation);
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
            //for (int i = 0; i < EnemyList.Count; i++)
            //{
            //    // Check if reached boundary

            //    EnemyList[i].RandomOrient();
            //}
            //Play movement sound
            SoundManager.Instance.PlayMoveSound();


            yield return new WaitForSeconds(MoveTime);
            if (CheckMoveDirection())
            {
                // Move Down
                UpdateTime();
                EnemyParent.transform.position += new Vector3(0, 0, -1) * m_moveSpace;

                //Play movement sound
                SoundManager.Instance.PlayMoveSound();

                for (int i=0; i<EnemyList.Count; i++)
                {
                    // Check if reached boundary
                    if(EnemyList[i].transform.position.z <=-6)
                    {
                        GameManager.Instance.GameOver(false);
                    }
                }
                yield return new WaitForSeconds(MoveTime);
            }
        }
    }

    IEnumerator RandomShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(AlientShootTime);
            // Alien Shoot
            ShootEnemy();
            // Re Initialise Random Time to shoot
            InitALienShootTime();
        }
    }

    public void BossDestoryed(Alien a_boss)
    {
        Debug.Log("Boss Destroyed");
        InitBossSpawnTime();
        canSpawnBoss = true;
        SoundManager.Instance.StopBossSound();
    }
    IEnumerator SpawnBoss()
    {
        while (true)
        {
            // Can spawn Boss
            if (canSpawnBoss)
            {
                yield return new WaitForSeconds(m_bossSpawnTime);
                // Alien Shoot
                InstantiateBoss();
            }
            yield return null;
            // Re Initialise Random Time to shoot\
        }
        yield return null;
    }

    public void RemoveAlienFromList(Alien currAlien)
    {
        EnemyList.Remove(currAlien);

        // Check game over
        if (GameManager.Instance.GameStarted == true && EnemyList.Count <= 0)
        {
            GameManager.Instance.GameOver(true);
        }
    }

    public void PlayerDestroyed(float a_pauseTime)
    {
        StartCoroutine(TakeAGamePause(a_pauseTime));
    }

    IEnumerator TakeAGamePause(float a_pauseTime)
    {
        GameOver();
        yield return new WaitForSeconds(a_pauseTime);
        InitEnemyBehaviour();
    }

    void GameOver()
    {
        // Stop Corotuine
        if (m_moveCor != null)
        {
            StopCoroutine(m_moveCor);
        }

        if (m_shootCor != null)
        {
            StopCoroutine(m_shootCor);
        }

        if (m_bossCor != null)
        {
            StopCoroutine(m_bossCor);
        }
    }
}