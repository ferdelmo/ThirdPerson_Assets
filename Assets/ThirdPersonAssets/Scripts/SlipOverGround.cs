using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipOverGround : MonoBehaviour
{
    private Rigidbody rb;

    public bool more45degreesOnly = true;
    // if its true, when the transform is on a more than 45degrees slope, it apply a constant force
    // if false, apply a force depending on the angle and without using physics


    private float gravity = 9.8f;
    public float velocity = 0;
    public float force = 2;

    private Vector3 inertia = Vector3.zero;
    private bool incrementVelocity = false;
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
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.5f, LayerMask.GetMask("Ground")))
        {
            if (more45degreesOnly)
            {
                if (Mathf.Acos(Vector3.Dot(hit.normal, transform.up) / (hit.normal.magnitude * transform.up.magnitude)) > 45 * Mathf.PI / 180)
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
            else
            {
                float angle = Mathf.Acos(Vector3.Dot(hit.normal, transform.up) / (hit.normal.magnitude * transform.up.magnitude));
                if (angle > 10 * Mathf.Deg2Rad)
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
                    velocity += gravity * Mathf.Sin(90 * Mathf.Deg2Rad - angle) * Time.fixedDeltaTime;
                    inertia = mov;
                    incrementVelocity = true;
                }
                else
                {
                    incrementVelocity = false;
                }
            }
        }
        if (!more45degreesOnly)
        {
            if (!incrementVelocity && velocity > 0)
            {
                float roza = 8 * gravity;
                velocity -= roza * Time.fixedDeltaTime;
                if (velocity < 0)
                {
                    velocity = 0;
                }
            }
            Vector3 movement;
            //dont move on Y, as rigibody use gravity
            if (!incrementVelocity)
            {
                movement = new Vector3(inertia.x, 0, inertia.z) * velocity * Time.fixedDeltaTime;
            }
            else
            {
                movement = inertia * velocity * Time.fixedDeltaTime;
            }
            transform.position += movement;
        }
    }
}
