using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperGravity : MonoBehaviour
{
    public float gravity = 9.8f;
    private float velocity = 0;

    int missed = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position+Vector3.up*0.5f, Vector3.down, out hit, 0.8f, LayerMask.GetMask("Ground")))
        {
            //transform.position = new Vector3(transform.position.x, hit.point.y+, transform.position.z);
            missed = 0;
        }
        else
        {
            missed++;
        }

        if (missed > 20)
        {
            Debug.Log("PABAJO");
            transform.position = transform.position + Vector3.down * velocity * Time.deltaTime;
            velocity += gravity * Time.deltaTime;
        }
    }
}
