using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType { Normal  =0, Boss =1}
public enum GameState { None = 0, Won = 1 , Lost =2}
public interface IBullet
{
    void Damage(Collider other);
    void Damage(Collision other);
}

public interface IPlayer
{
    void Die();
    void AddAmmo(int val);
}