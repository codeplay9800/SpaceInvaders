using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion heading;
    public enum BulletType { Player = 0, Enemy =1};

    [SerializeField] BulletType m_bulletTyp = BulletType.Player;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the X-axis
        thrust.z = 400.0f;
        // do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;
        // set the direction it will travel in
        //GetComponent<Rigidbody>().MoveRotation(heading);
        // apply thrust once, no need to apply it again since
        // it will not decelerate
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    void OnCollisionEnter(Collision collision)
    {
        //// the Collision contains a lot of info, but it’s the colliding
        //// object we’re most interested in.
        //Collider collider = collision.collider;
        //if (collider.CompareTag("Asteroid"))
        //{
        //    Asteroid roid = collider.gameObject.GetComponent<Asteroid>();
        //    // let the other object handle its own death throes
        //    roid.Die();
        //    // Destroy the Bullet which collided with the Asteroid
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    // if we collided with something else, print to console
        //    // what the other thing was
        //    Debug.Log("Collided with " + collider.tag);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Trigger");
        if (m_bulletTyp == BulletType.Player && other.CompareTag("Alien"))
        {
            Alien invader = other.gameObject.GetComponent<Alien>();
            // let the other object handle its own death throes
            invader.Die();
            // Destroy the Bullet which collided with the Asteroid
            Destroy(gameObject);
        }
        if (m_bulletTyp == BulletType.Enemy && other.CompareTag("Player"))
        {
            //Kill Player 
            Destroy(gameObject);
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
