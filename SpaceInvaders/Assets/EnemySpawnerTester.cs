using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTester : MonoBehaviour
{

    public GameObject EnemyPrefab1;
    public List<GameObject> EnemyList;
    [SerializeField] float distanceScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("SpawnEnemys")]
    void SpawnEnemys()
    {
        for(int i=-5; i<= 5; i++)
        {
            GameObject newEnemy = Instantiate(EnemyPrefab1, this.transform);
            EnemyList.Add(newEnemy);
            newEnemy.transform.position = new Vector3(i * distanceScale, 0, 0);
        }
    }
}
