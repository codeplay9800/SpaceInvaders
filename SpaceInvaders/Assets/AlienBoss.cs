using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBoss : MonoBehaviour
{
    public Vector3 m_moveDir = Vector3.zero;
    [SerializeField] float m_movSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_moveDir * m_movSpeed;
    }
}
