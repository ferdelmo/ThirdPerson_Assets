using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperDeath : MonoBehaviour
{
    //number of hits for the chomper to death
    public int health = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDeath()
    {
        health--;

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
