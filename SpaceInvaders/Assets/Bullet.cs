using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IBullet))]
public class Bullet: MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion heading;

    [SerializeField] SpriteRenderer m_sr;

    bool canDamage = true;


    [SerializeField] public IBullet bulletType;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the X-axis
        thrust.z = 800f;
        // do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;
        // set the direction it will travel in
        //GetComponent<Rigidbody>().MoveRotation(heading);
        // apply thrust once, no need to apply it again since
        // it will not decelerate
        GetComponent<Rigidbody>().AddRelativeForce(thrust);

        bulletType = GetComponent<IBullet>(); 
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
        if (canDamage)
        {
            canDamage = !bulletType.Damage(collision);
            UpdateState(canDamage);
        }
            //canDamage = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDamage)
        {
            //Debug.Log("Enter Trigger");
            canDamage = !bulletType.Damage(other);
            UpdateState(canDamage);
        }
    }

    void UpdateState(bool canDamage)
    {
        if(!canDamage)
        {
            // Turn Off
            m_sr.color = new Vector4(m_sr.color.r, m_sr.color.g, m_sr.color.b, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
