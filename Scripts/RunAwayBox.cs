using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayBox : MonoBehaviour
{

    float zCoordinate;
    float xCoordinate;
    private Transform playerPosition;

    // Use this for initialization
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (playerPosition.position);

    }

  
}