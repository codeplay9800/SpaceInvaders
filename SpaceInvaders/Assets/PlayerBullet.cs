using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IBullet
{
    public void Damage(Collider other)
    {
        if (other.CompareTag("Alien"))
        {
            Alien enemyShip = other.gameObject.GetComponent<Alien>();

            GameManager.Instance.AddToScore(enemyShip.PointValue);
            // let the other object handle its own death throes
            enemyShip.Die();
        }
        if (other.CompareTag("Shield"))
        {
            Shield shield = other.gameObject.GetComponent<Shield>();
            // let the other object handle its own death throes
            shield.Damage();
        }

        // Destroy the Bullet which collided with the Asteroid
        //Destroy(gameObject);
    }

    public void Damage(Collision other)
    {
        if (other.collider.CompareTag("Alien"))
        {
            Alien enemyShip = other.gameObject.GetComponent<Alien>();

            GameManager.Instance.AddToScore(enemyShip.PointValue);
            // let the other object handle its own death throes
            enemyShip.Die();
        }
        if (other.collider.CompareTag("Shield"))
        {
            Shield shield = other.gameObject.GetComponent<Shield>();
            // let the other object handle its own death throes
            shield.Damage();
        }

        // Destroy the Bullet which collided with the Asteroid
        //Destroy(gameObject);
    }

}
