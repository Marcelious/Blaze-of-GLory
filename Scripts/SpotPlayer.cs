using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpotPlayer : MonoBehaviour {

    public float speed = 1;
    public float waitTime = 2f;
    public float turnSpeed = 180;
    Rigidbody mybody;
    bool route = true;

    public Light spotLight;
    public float viewDistance = 3;
    float viewAngle = 45;
    public LayerMask viewMask;
    Color orginalSpotLightColour = Color.white;

    public Transform target;
    public Transform pathHolder;
    public NavMeshAgent agent;
    Transform Player;
    private bool activate = false;

    public SFXManager m_soundFXManager;

    void Start()
    {
        m_soundFXManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();

        Player = GameObject.Find("Player").transform;
        mybody = GetComponent<Rigidbody>();
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        //viewDistance = spotLight.range;
        StartCoroutine(followPath(waypoints));
    }

    void Update()
    {
        if (canSeePlayer())
        {
            Debug.Log("i see you");
            //stop following path
            //StopAllCoroutines();
            //use the navmesh to make the enemy pathfind to the exit
            agent.SetDestination(target.transform.position);
        }
    }
   

    bool canSeePlayer()
    {
        //Debug.Log(Vector3.Distance(transform.position, Player.position));
        if (Vector3.Distance(transform.position, Player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (Player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                //Debug.Log("yes");
                //if can see the player
                if (Physics.Linecast(transform.position, Player.position, viewMask))
                {
                    //Debug.Log("yes2");
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator followPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);
        while (route)
        {
            transform.position = Vector3.MoveTowards(transform.position,targetWaypoint, speed *Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(turnToFace(targetWaypoint));
            }
            yield return null;
        }
    }
    IEnumerator turnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;
        //while not looking at player keep turning
        while (Mathf.Abs( Mathf.DeltaAngle(transform.eulerAngles.y,targetAngle)) > 0.05)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }
    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
    void FixedUpdate()
    {
        mybody.MovePosition(transform.position);
    }
}
