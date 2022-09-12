using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IBullet
{
    public void Damage(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SimpleShip playerShip = other.gameObject.GetComponent<SimpleShip>();
            // let the other object handle its own death throes
            playerShip.Die();
            // Destroy the Bullet which collided with the Asteroid
            Destroy(gameObject);
        }
        if (other.CompareTag("Shield"))
        {
            Shield shield = other.gameObject.GetComponent<Shield>();
            // let the other object handle its own death throes
            shield.Damage();
            // Destroy the Bullet which collided with the Asteroid
            Destroy(gameObject);
        }
    }
}
