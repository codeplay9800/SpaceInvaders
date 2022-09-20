using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject deathExplosion;
    //public AudioClip deathKnell;
    public EnemyType m_currType = EnemyType.Normal;
    [SerializeField] int pointValue = 10;
    [SerializeField] GameObject spriteImage;

    Rigidbody alienRB;
    Collider alienCollider;
    [SerializeField] SpriteRenderer alienSR;

    public int PointValue { get { return pointValue; } }

    int currStartOrient = 0;

    Vector3 StartOrentation;
    // Start is called before the first frame update
    void Start()
    {
        alienRB = GetComponent<Rigidbody>();
        alienCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ApplyRandomForce()
    {
        float randomAngle = Random.Range(-90, 90);
        float randomForce = Random.Range(0.1f , 3.0f);
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);
        Matrix4x4 rotateM = Matrix4x4.Rotate(rotation);
        Vector3 randomDirection = rotateM * -this.transform.forward;
        randomDirection = randomDirection.normalized;
        alienRB.AddForce(randomDirection * EnemyManager.Instance.hitForce);
    }

    void DieBehaviour()
    {
        alienRB.isKinematic = false;
        alienCollider.isTrigger = false;
        alienRB.useGravity = true;
        alienSR.color = new Vector4(alienSR.color.r, alienSR.color.g, alienSR.color.b, 0.4f);
        ApplyRandomForce();
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

        //Destroy(gameObject);
        DieBehaviour();
    }

    private void OnDestroy()
    {
        if(m_currType == EnemyType.Boss)
        {
            EnemyManager.Instance.BossDestoryed(this);
        }
    }
    public void initEnemy(int currRow)
    {

        StartOrentation = spriteImage.transform.localRotation.eulerAngles;
        currStartOrient = currRow % 2;
    }

    public void RandomOrient()
    {
        currStartOrient = (currStartOrient+1) % 2;
        if (currStartOrient == 0)
        {
            //Left Turn

            spriteImage.transform.localRotation = Quaternion.Euler(StartOrentation + new Vector3(0, 0, 5));

        }
        else
        {
            //Left Turn
            spriteImage.transform.localRotation = Quaternion.Euler(StartOrentation + new Vector3(0, 0, -5));
        }
    }
}
