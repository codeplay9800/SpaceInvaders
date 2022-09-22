using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IBullet
{
    public bool Damage(Collider other)
    {
        if (other.CompareTag("Alien"))
        {
            Alien enemyShip = other.gameObject.GetComponent<Alien>();

            GameManager.Instance.AddToScore(enemyShip.PointValue);
            // let the other object handle its own death throes
            enemyShip.Die();
            return true;
        }
        if (other.CompareTag("Shield"))
        {
            Shield shield = other.gameObject.GetComponent<Shield>();
            // let the other object handle its own death throes
            shield.Damage();
            return true;
        }
        if (other.CompareTag("Ground"))
        {
            // Turn Off Bullet
            return true;
        }
        return false;
        // Destroy the Bullet which collided with the Asteroid
        //Destroy(gameObject);
    }

    public bool Damage(Collision other)
    {
        if (other.collider.CompareTag("Alien"))
        {
            Alien enemyShip = other.gameObject.GetComponent<Alien>();

            GameManager.Instance.AddToScore(enemyShip.PointValue);
            // let the other object handle its own death throes
            enemyShip.Die();
            return true;
        }
        if (other.collider.CompareTag("Shield"))
        {
            Shield shield = other.gameObject.GetComponent<Shield>();
            // let the other object handle its own death throes
            shield.Damage();
            return true;
        }
        if (other.collider.CompareTag("Ground"))
        {
            // let the other object handle its own death throes
            
            return true;
        }
        return false;

        // Destroy the Bullet which collided with the Asteroid
        //Destroy(gameObject);
    }

}
