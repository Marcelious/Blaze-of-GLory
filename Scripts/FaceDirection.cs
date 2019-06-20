using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    public float speed = 10;
    private Vector3 targetDir;
    public float smooth = 2.0F;
    public float tiltAngle = 30.0F;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            targetDir = new Vector3(-1, -1, 0);
            Vector3 dir = Vector3.RotateTowards(transform.forward, targetDir, speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(dir);
            targetDir = new Vector3(0, 0, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            targetDir = new Vector3(1, 1, 0);
            Vector3 dir = Vector3.RotateTowards(transform.forward, targetDir, speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(dir);
            targetDir = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            targetDir = new Vector3(0, 1, 0);
            Vector3 dir = Vector3.RotateTowards(transform.forward, targetDir, speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(dir);
            targetDir = new Vector3(0, -1, 0);

        }
        if (Input.GetKey(KeyCode.S))
        {
            targetDir = new Vector3(0, -1, 0);
            Vector3 dir = Vector3.RotateTowards(transform.forward, targetDir, speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(dir);
            targetDir = new Vector3(0, 1, 0);
        }
    }
}

