using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType { Normal  =0, Boss =1}
public interface IBullet
{
    void Damage(Collider other);
}
