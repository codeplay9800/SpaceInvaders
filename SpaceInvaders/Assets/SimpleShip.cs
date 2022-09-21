using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShip : MonoBehaviour, IPlayer
{
    public float m_moveSpeed;
    public GameObject m_bullet;

    GameObject m_spawnedBullet = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
       
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            this.transform.position += new Vector3(1 * m_moveSpeed, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            this.transform.position += new Vector3(-1 * m_moveSpeed, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && m_spawnedBullet == null)
        {
            Vector3 spawnPos = this.transform.position + this.transform.forward * this.transform.localScale.z;
            // instantiate the Bullet
            m_spawnedBullet = Instantiate(m_bullet, spawnPos, Quaternion.identity) as GameObject;

            SoundManager.Instance.PlayerShootSound();
            // get the Bullet Script Component of the new Bullet instance
            //Bullet b = obj.GetComponent<Bullet>();
            // set the direction the Bullet will travel in
            //Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            //b.heading = rot;
        }
    }

    public void Die()
    {
        // Do Somethin
        GameManager.Instance.ReduceLife();
        SoundManager.Instance.PlayerDieSound();
        InGameContainer.Instance.StartShakeCamera();
        Destroy(gameObject);
    }

    public void AddAmmo(int val)
    {

    }
}
