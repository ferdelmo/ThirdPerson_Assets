using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperGravity : MonoBehaviour
{
    public float gravity = 9.8f;
    private float velocity = 0;

    private bool grav = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        grav = !Physics.Raycast(transform.position, -transform.up, out hit, 0.1f, LayerMask.GetMask("Ground"));
        if (grav)
        {
            velocity += gravity * Time.fixedDeltaTime;
        }
        else
        {
            velocity = 0;
        }
        transform.position += Vector3.down * velocity * Time.fixedDeltaTime;
    }
}
