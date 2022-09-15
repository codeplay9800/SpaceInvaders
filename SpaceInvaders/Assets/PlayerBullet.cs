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
