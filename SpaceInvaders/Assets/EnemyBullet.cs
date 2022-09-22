using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IBullet
{
    // ENemy bullets do not effect triggers only colliders
    public bool Damage(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IPlayer playerShip = other.gameObject.GetComponent<IPlayer>();
            // let the other object handle its own death throes
            playerShip.Die();
            return true;

        }
        if (other.CompareTag("Shield"))
        {
            Shield shield = other.gameObject.GetComponent<Shield>();
            // let the other object handle its own death throes
            shield.Damage();
            return true;
        }
        if (other.CompareTag("Ground") || other.CompareTag("PlayerShield"))
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
        if (other.collider.CompareTag("Player"))
        {
            IPlayer playerShip = other.gameObject.GetComponent<IPlayer>();
            // let the other object handle its own death throes
            playerShip.Die();
            return true;
        }
        if (other.collider.CompareTag("Shield"))
        {
            Shield shield = other.gameObject.GetComponent<Shield>();
            // let the other object handle its own death throes
            shield.Damage();
            return true;
        }
        if (other.collider.CompareTag("Ground") || other.collider.CompareTag("PlayerShield"))
        {
            // Turn Off Bullet
            return true;
        }

        return false;
        // Destroy the Bullet which collided with the Asteroid
        //Destroy(gameObject);
    }

}
