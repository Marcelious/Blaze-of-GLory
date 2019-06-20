using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;

    [SerializeField]
    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = Vector3.up * 2f;


    }
	
	// Update is called once per frame
	void LateUpdate () {
        if(player == null)
        {
            return;
        }
       transform.position = player.transform.position + offset;
	}
}
