using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperDeath : MonoBehaviour
{
    //number of hits for the chomper to death
    public int health = 2;

    public delegate void OnDeathFunctions();

    OnDeathFunctions odf;

    public void RegisterOnDeath(OnDeathFunctions o)
    {
        odf += o;
    }

    public void UnRegisterOnDeath(OnDeathFunctions o)
    {
        odf -= o;
    }

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
            odf?.Invoke();
            Destroy(this.gameObject);
        }
    }

    void Die()
    {
        odf?.Invoke();
        Destroy(this.gameObject);
    }
}
