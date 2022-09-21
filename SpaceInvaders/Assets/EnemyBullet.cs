using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IBullet
{
    public void Damage(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IPlayer playerShip = other.gameObject.GetComponent<IPlayer>();
            // let the other object handle its own death throes
            playerShip.Die();
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
        if (other.collider.CompareTag("Player"))
        {
            IPlayer playerShip = other.gameObject.GetComponent<IPlayer>();
            // let the other object handle its own death throes
            playerShip.Die();
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
