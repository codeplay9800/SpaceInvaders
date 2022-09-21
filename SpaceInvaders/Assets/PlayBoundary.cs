using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBoundary : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Destroy Any kind of bullet
        if(other.CompareTag("Alien") || other.gameObject.GetComponent<IBullet>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
