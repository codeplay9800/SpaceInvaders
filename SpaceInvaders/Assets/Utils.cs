using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType { Normal  =0, Boss =1}

public enum PlayerBulletType { Simple = 0, Power1 = 1 }
public enum GameState { None = 0, Won = 1 , Lost =2}
public interface IBullet
{
    bool Damage(Collider other);
    bool Damage(Collision other);
}

public interface IPlayer
{
    void Die();
    void UpdateAmmo(int val);
}