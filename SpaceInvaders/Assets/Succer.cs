using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alien"))
        {
            GameManager.Instance.AddToResouce(1);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Bullet"))
        {
            IBullet bullet = other.gameObject.GetComponent<IBullet>();
            // let the other object handle its own death throes
            if(bullet.GetType() ==  typeof(EnemyBullet))
            {
                GameManager.Instance.AddToResouce(2);
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Alien"))
        {
            GameManager.Instance.AddToResouce(1);
            Destroy(other.gameObject);
        }
        if (other.collider.CompareTag("Bullet"))
        {
            IBullet bullet = other.gameObject.GetComponent<IBullet>();
            // let the other object handle its own death throes
            if (bullet.GetType() == typeof(EnemyBullet))
            {
                GameManager.Instance.AddToResouce(2);
            }
            Destroy(other.gameObject);
        }
    }
}
