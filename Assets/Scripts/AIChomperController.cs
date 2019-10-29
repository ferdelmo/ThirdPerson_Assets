using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChomperController : MonoBehaviour
{
    [Range(0, 1)]
    public float animationSpeed;
    public float speed;
    public float runSpeed;
    public float rotationSpeed = 40;
    public float runRotationSpeed = 65;
    public Transform[] points;
    public float fov = 45;
    
    private float animationSpeedWalk = 0.3f;
    private float animationSpeedRun = 0.6f;
    public float minDistSqr = 0.1f;
    private bool enemyFound = false;
    private int nextPosition = 1;
    private Rigidbody rigidbody;
    private ChomperAnimation chomperAnimation;
    private Transform enemyPosition;


    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
        chomperAnimation = GetComponent<ChomperAnimation>();
        enemyPosition = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        enemyFound = seeingEnemy();
        if (!enemyFound)
        {
             Vector3 direction = points[nextPosition].position - transform.position;
             float angle = Mathf.Acos(Vector3.Dot(direction, transform.forward) / (direction.magnitude * transform.forward.magnitude));
             if (angle > 0.1)
             {
                if(Vector3.Dot(transform.right, direction) < 0)
                {
                    chomperAnimation.Updatefordward(animationSpeedWalk);
                    transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
                }
                else
                {
                    chomperAnimation.Updatefordward(animationSpeedWalk);
                    transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                }
             }
             else
             {
                 chomperAnimation.Updatefordward(animationSpeedWalk);
                 transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
                 if (direction.sqrMagnitude < minDistSqr)
                 {
                     nextPosition = (nextPosition + 1) % points.Length;
                     Vector3 newDirection = points[nextPosition].position - transform.position;
                 }
             }
        }
        else
        {
            Vector3 direction = enemyPosition.position - transform.position;
            float angle = Mathf.Acos(Vector3.Dot(direction, transform.forward) / (direction.magnitude * transform.forward.magnitude));
            if (angle > 0.04)
            {
                if (Vector3.Dot(transform.right, direction) < 0)
                {
                    chomperAnimation.Updatefordward(animationSpeedRun);
                    transform.Rotate(0, -runRotationSpeed * Time.deltaTime, 0);
                }
                else
                {
                    chomperAnimation.Updatefordward(animationSpeedRun);
                    transform.Rotate(0, runRotationSpeed * Time.deltaTime, 0);
                }
            }
            transform.position += transform.forward * runSpeed * Time.deltaTime;
        }

    }

    bool seeingEnemy()
    {
        Vector3 direction = enemyPosition.position - transform.position;
        return Mathf.Acos(Vector3.Dot(direction, transform.forward) / (direction.magnitude * transform.forward.magnitude)) < Mathf.Deg2Rad * fov;
    }

}
