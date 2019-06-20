using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Runaway : MonoBehaviour
{

    public float speed;
    public float nearDistance;
    private bool patrol = true;
    private bool route = true;
    private Transform playerMove;
 
    //  public GameObject patrolStart;
    //public GameObject patrolEnd;
    Rigidbody mybody;
    Camera viewCamera;
    Vector3 Velocity;

    //USED FOR FIELD OF VIEW
    public float viewRadius;
    [Range(0, 360)]
    //public float viewAngle;
    //public LayerMask targetMask;
    //public LayerMask obstacleMask;
    [HideInInspector]
    public List<Transform> visibleTarget = new List<Transform>();

    public SFXManager m_soundFXManager;

    public float meshResolution;

    // Use this for initialization
    void Start()
    {
       
        m_soundFXManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();

        playerMove = GameObject.Find("Player").transform;
        // StartCoroutine("FindTargetsWithDelay", .2f);

        //get teh field of view
        mybody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;

    }

    //IEnumerator FindTargetsWithDelay(float delay)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(delay);
    //        FindVisibleTargets();
    //    }
    //}

    //void FindVisibleTargets()
    //{
    //    visibleTarget.Clear();
    //    Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
    //    for (int i = 0; i < targetsInViewRadius.Length; i++)
    //    {
    //        Transform target = targetsInViewRadius[i].transform;
    //        Vector3 dirToTarget = (target.position - transform.position).normalized;
    //        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
    //        {
    //            float dstToTarget = Vector3.Distance(transform.position, target.position);
    //            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
    //            {
    //                visibleTarget.Add(target);
    //            }
    //        }
    //    }
    //}

    //void drawFieldOfView()
    //{
    //    int stepcount = Mathf.RoundToInt(viewAngle * meshResolution);
    //    float stepAngleSize = viewAngle / stepcount;
    //    for (int i = 0; i<= stepcount; i++)
    //    {
    //        float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
    //        Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.red);
    //    }
    //}
    public Vector3 DirFromAngle(float angleInDegree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegree += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }


    // Update is called once per frame
    void Update()
    {
        // drawFieldOfView();
        //if (patrol == true)
        //{
        //    if (route == true)
        //    {
        //        float thedot = VectorAlgrithms.DotProduct(patrolStart.transform.position, transform.position);
        //        Vector3 toroute = VectorAlgrithms.VectorMinus(patrolStart.transform.position, transform.position);
        //        if (VectorAlgrithms.VectorLength(toroute) > 0.0f)
        //        {
        //            if (transform.position != patrolStart.transform.position)
        //            {
        //                transform.position += VectorAlgrithms.VectorNormailzed(toroute) * Time.deltaTime;
        //            }

        //        }
        //        if (transform.position == patrolStart.transform.position) { route = false; print("reached"); Debug.Log("reached"); }
        //    }else if (route == false)
        //    {
        //        float thedot = VectorAlgrithms.DotProduct(patrolEnd.transform.position, transform.position);
        //        Vector3 toroute = VectorAlgrithms.VectorMinus(patrolEnd.transform.position, transform.position);
        //        if (VectorAlgrithms.VectorLength(toroute) > 0.0f)
        //        {
        //            if (transform.position != patrolEnd.transform.position)
        //            {
        //                transform.position += VectorAlgrithms.VectorNormailzed(toroute) * Time.deltaTime;
        //            }

        //        }
        //        if (transform.position == patrolEnd.transform.position) { route = true;  }
        //    }
        //}

        if (route == true)
        {
            // transform.LookAt(patrolStart.transform.position + Vector3.up * transform.position.z);
        }
        else if (route == false)
        {
            // transform.LookAt(patrolEnd.transform.position);
        }

        //Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.z, viewCamera.transform.position.z));
        //transform.LookAt(mousePos + Vector3.up * transform.position.z);

        //if player is near the character run away

        //When player is destroyed at a game over, the script doesn't try to access it
        if (playerMove == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, playerMove.position) < nearDistance)
        {
            Velocity = Vector3.MoveTowards(transform.position, playerMove.position, -speed * Time.deltaTime);
            transform.position = Velocity;
            patrol = false;
        }
        else if (Vector3.Distance(transform.position, playerMove.position) < 5 && Vector3.Distance(transform.position, playerMove.position) > nearDistance)
        {
            transform.position = this.transform.position;
        }
    }

    void FixedUpdate()
    {
        mybody.MovePosition(transform.position);

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            this.enabled = false;
    
            
        }
    }
}
