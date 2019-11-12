using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChomperController : MonoBehaviour
{
    [Range(0, 1)]
    public float speed;
    public float runSpeed;
    public float rotationSpeed = 40;
    public float runRotationSpeed = 65;
    public Transform[] points;
    public float fov = 45;
    public float range = 10;
    public float sqrAttackDistance = 4;
    public float attackCadence = 2;

    private float animationSpeedWalk = 0.4f;
    private float animationSpeedRun = 0.6f;
    public float minDistSqr = 2;
    private bool enemyFound = false;
    private int nextPosition = 1;
    private Rigidbody rigidbody;
    private ChomperAnimation chomperAnimation;
    private Transform enemyPosition;
    private float lastAttack = 0;


    bool orientNeeded = false;

    public bool rotate = false;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
        chomperAnimation = GetComponent<ChomperAnimation>();
        enemyPosition = GameObject.FindGameObjectWithTag("Player").transform;
        orientNeeded = true;

    }

    // Update is called once per frame
    void Update()
    {
        lastAttack += Time.deltaTime;
        bool enemy = seeingEnemy();
        if (!enemy)
        {
            if (orientNeeded && !rotate)
            {

                rotate = true;
                orientNeeded = false;
                chomperAnimation.Updatefordward(0.2f);
                StartCoroutine(Orient(points[nextPosition].position));
            }
            else if (!rotate)
            {
                Vector3 direction = points[nextPosition].position - transform.position;
                //float angle = Mathf.Acos(Vector3.Dot(direction, transform.forward) / (direction.magnitude * transform.forward.magnitude));

                chomperAnimation.Updatefordward(animationSpeedWalk);

                transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
                if (direction.sqrMagnitude < minDistSqr)
                {
                    nextPosition = (nextPosition + 1) % points.Length;
                    rotate = true;
                    chomperAnimation.Updatefordward(0.2f);
                    StartCoroutine(Orient(points[nextPosition].position));
                }
            }
        }
        else
        {
            StopAllCoroutines();
            rotate = false;
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
            if(direction.sqrMagnitude < sqrAttackDistance && lastAttack >= attackCadence)
            {
                chomperAnimation.Attack();
                lastAttack = 0;
            }
            transform.position += transform.forward * runSpeed * Time.deltaTime;
            orientNeeded = true;
        }

    }

    bool seeingEnemy()
    {
        Vector3 direction = enemyPosition.position - transform.position;
        return direction.sqrMagnitude < range*range && Mathf.Acos(Vector3.Dot(direction, transform.forward) / (direction.magnitude * transform.forward.magnitude)) < Mathf.Deg2Rad * fov;
    }


    //orient the transform to look at a position (without changing Y axis)
    IEnumerator Orient(Vector3 pos)
    {
        Vector3 direction = pos - transform.position;
        float angle = Mathf.Acos(Vector3.Dot(direction, transform.forward) / (direction.magnitude * transform.forward.magnitude));
        if (Vector3.Dot(transform.right, direction) < 0)
        {
            angle = -angle;
        }

        angle = Mathf.Rad2Deg * angle;
        Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * angle);
        Quaternion originalRotation = transform.rotation;
        float time = 0;
        float rotationTime = Mathf.Abs(angle) / rotationSpeed;
        while (time < rotationTime)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(originalRotation, newRotation, time / rotationTime);

            yield return new WaitForEndOfFrame();
        }
        
        rotate = false;
        orientNeeded = false;

    }
}
