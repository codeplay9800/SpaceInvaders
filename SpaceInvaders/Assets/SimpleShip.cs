﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShip : MonoBehaviour
{
    public float m_moveSpeed;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
       
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            this.transform.position += new Vector3(1 * m_moveSpeed, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            this.transform.position += new Vector3(-1 * m_moveSpeed, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 spawnPos = this.transform.position + this.transform.forward * this.transform.localScale.z; 
            // instantiate the Bullet
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
            // get the Bullet Script Component of the new Bullet instance
            //Bullet b = obj.GetComponent<Bullet>();
            // set the direction the Bullet will travel in
            //Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            //b.heading = rot;
        }
    }
}
