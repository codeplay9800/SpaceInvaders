using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsShip : MonoBehaviour, IPlayer
{
    public float m_moveSpeed;
    public GameObject m_bullet;
    [SerializeField] float firerate = 0.2f;
    float currFireTime = 0; 
    GameObject m_spawnedBullet = null;
    int ammo = 10;
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
        Shoot();
    }

    void Shoot()
    {
        currFireTime += Time.deltaTime;
        if(Input.GetButtonDown("Fire1") && (currFireTime >= firerate) && ammo >= 0)
        {
            // reduce ammo
            ammo -= 1;

            currFireTime = 0;
            Vector3 spawnPos = this.transform.position + this.transform.forward * this.transform.localScale.z;
            // instantiate the Bullet
            m_spawnedBullet = Instantiate(m_bullet, spawnPos, Quaternion.identity) as GameObject;

            SoundManager.Instance.PlayerShootSound();
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
        ammo += val;
    }
}
