using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update

    public List<Material> mat;
    void Start()
    {
        health = mat.Count;
    }

    void InitTexture()
    {
        int index = mat.Count - health;
        if (index >= 0 && index < mat.Count)
        {
            //Debug.Log(mat.Count - health);
            GetComponent<MeshRenderer>().material = mat[mat.Count - health];
        }
    }

    public void Damage()
    {
        health -= 1;
        InitTexture();
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
