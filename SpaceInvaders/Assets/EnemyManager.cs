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
    [SerializeField] GameObject m_bossLeftSp;
    [SerializeField] GameObject m_bossRightSp;
    float m_bossSpawnTime = 1.0f;

    [SerializeField] GameObject EnemyParent;

    public List<GameObject> EnemyList;
    [SerializeField] float distanceScale;

    [SerializeField] float MoveTime = 1.0f;

    Coroutine m_moveCor = null, m_shootCor = null;
    Vector3 m_moveDir;

    [SerializeField] float m_moveSpace = 1.0f;

    float AlientShootTime = 1.0f;

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
        InvokeRepeating("SpawnBoss", m_bossSpawnTime, m_bossSpawnTime);

        m_shootCor = StartCoroutine(RandomShoot());

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
        m_bossSpawnTime = Random.Range(5, 10);
    }

    void InitALienShootTime()
    {
        AlientShootTime = Random.Range(0.1f, 2.0f);
    }

    void SpawnBoss()
    {
        Debug.Log("Spawning Boss time:" + m_bossSpawnTime);
        if(m_bossShip == null)
        {
            int spawnPos = Random.Range(0, 1);

            // Spawn from left
            if(spawnPos == 0)
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
            InitBossSpawnTime();
        }
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
                newEnemy.transform.localPosition = new Vector3(i * distanceScale, 0, j * distanceScale);
            }
        }
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        m_moveCor = StartCoroutine(Move());
    }

    public void ShootEnemy()
    {
        int randEnemy = Random.Range(0, EnemyList.Count);
        Transform currEnemyTrans = EnemyList[randEnemy].transform;
        Vector3 spawnPos = currEnemyTrans.transform.position + currEnemyTrans.transform.forward * - currEnemyTrans.transform.localScale.z;
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
}
