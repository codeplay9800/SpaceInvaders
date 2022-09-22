using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsShip : MonoBehaviour, IPlayer
{
    public float m_moveSpeed;
    public GameObject m_bullet;
    [SerializeField] GameObject Shield;
    Joystick inGameJoystick;
    [SerializeField] float firerate = 0.2f;
    float currFireTime = 0; 

    PlayerBulletType m_currBullet = PlayerBulletType.Simple;
    float horizontalMoveKeyBoard = 0.0f;
    float horizontalMoveAndroid = 0.0f;

    int ammo = 10;
    // Start is called before the first frame update
    void Start()
    {
        InitShip();
    }
    void InitShip()
    {
        InGameUIManager.Instance.UpdateAmmo(ammo);
        inGameJoystick = InGameContainer.Instance.InGameJoyStick;
        InGameContainer.Instance.FireButton.onClick.AddListener(Shoot);
        InGameContainer.Instance.PowerUpButton.onClick.AddListener(UsePowerUp);
    }

    public void UpdatePowerUp(bool state, PlayerBulletType val)
    {
        if(state)
        {
            Shield.SetActive(true);
            firerate = 0.0f;
            StartCoroutine(PowerUpCounter());

        }
        else
        {
            Shield.SetActive(false);
            firerate = 0.2f;
        }
        m_currBullet = val;
        SoundManager.Instance.UpdatePowerUpSound(state);
    }

    void FixedUpdate()
    {
        horizontalMoveKeyBoard = Input.GetAxisRaw("Horizontal");
        horizontalMoveAndroid = inGameJoystick.Horizontal;
        if (horizontalMoveKeyBoard > 0 || horizontalMoveAndroid >0)
        {
            this.transform.position += new Vector3(1 * m_moveSpeed, 0, 0);
        }
        else if (horizontalMoveKeyBoard < 0 || horizontalMoveAndroid < 0)
        {
            this.transform.position += new Vector3(-1 * m_moveSpeed, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currFireTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") )
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            UsePowerUp();
        }
    }

    void UsePowerUp()
    {
        if(GameManager.Instance.CanUsePowerUp())
        {
            UpdatePowerUp(true, PlayerBulletType.Power1);
        }
    }

    IEnumerator PowerUpCounter()
    {
        yield return new WaitForSeconds(10.0f);
        UpdatePowerUp(false, PlayerBulletType.Simple);
    }

    void Shoot()
    {
        
        if((currFireTime >= firerate) && ammo > 0)
        {
            // reduce ammo
            UpdateAmmo(-1);
            currFireTime = 0;
            
            // instantiate the Bullet
            if(m_currBullet == PlayerBulletType.Simple)
            {
                SimpleBullet();
            }
            else if(m_currBullet == PlayerBulletType.Power1)
            {
                Power1Bullet();
            }
            SoundManager.Instance.PlayerShootSound();
        }
      
    }

    void SimpleBullet()
    {
        Vector3 spawnPos = this.transform.position + this.transform.forward * this.transform.localScale.z;
         Instantiate(m_bullet, spawnPos, Quaternion.identity);
    }
    void Power1Bullet()
    {
        Vector3 spawnPos = this.transform.position + this.transform.forward * this.transform.localScale.z;
        Vector3 spawnPos1 = spawnPos - new Vector3(0.15f, 0, 0);
        Vector3 spawnPos2 = spawnPos + new Vector3(0.15f, 0, 0);
        Instantiate(m_bullet, spawnPos1, Quaternion.identity);
        Instantiate(m_bullet, spawnPos2, Quaternion.identity);
    }

    public void Die()
    {
        // Do Somethin
        GameManager.Instance.ReduceLife();
        SoundManager.Instance.PlayerDieSound();
        InGameContainer.Instance.StartShakeCamera();
        Destroy(gameObject);
    }

    public void UpdateAmmo(int val)
    {
        ammo += val;
        InGameUIManager.Instance.UpdateAmmo(ammo);
    }
}
