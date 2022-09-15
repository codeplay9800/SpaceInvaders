using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject deathExplosion;
    //public AudioClip deathKnell;
    public EnemyType m_currType = EnemyType.Normal;
    [SerializeField] int pointValue = 10;

    public int PointValue { get { return pointValue; } }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Die()
    {
        //Debug.Log ("OH NO I AM DYING");


        SoundManager.Instance.AlienDieSound();

        //Update Player Score
        if (m_currType == EnemyType.Normal)
        {
            EnemyManager.Instance.RemoveAlienFromList(this);
        }
        /* all of Shuriken's particle effects by default use the convention of Z being upwards, 
        and XY being the horizontal plane. as a result, since we are looking down the Y axis, we rotate 
        the particle system so that it flys in the right way.
        */
        //Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        //GameObject obj = GameObject.Find("GlobalObject");
        //Global g = obj.GetComponent<Global>();
        //g.score += pointValue;
        // Destroy removes the gameObject from the scene and
        // marks it for garbage collection
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        if(m_currType == EnemyType.Boss)
        {
            EnemyManager.Instance.BossDestoryed(this);
        }
    }
}
