using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipOverPlatform : MonoBehaviour
{
    private Rigidbody rb;
    public float force = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        //Maybe only throws ray where the plataform is close
        if(Physics.Raycast(transform.position, -transform.up, out hit, 0.5f))
        {
            if(Mathf.Acos(Vector3.Dot(hit.normal, transform.up)/(hit.normal.magnitude * transform.up.magnitude)) > 45*Mathf.PI/180)
            {
                 
                //Debug.Log("esta sucediendo");
                Vector3 d = Vector3.Cross(transform.up.normalized, hit.normal.normalized);

                Vector3 mov = Vector3.Cross(hit.normal, d);
                if (mov.y > 0)
                {
                    mov = -mov;
                }
                //Debug.Log(mov);
                //maybe change it for transforms
                rb.AddForce(mov * force, ForceMode.Force);
            }
        }
    }
}
