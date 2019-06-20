using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private AudioSource[] m_music;

    void Awake()
    {
        m_music = GetComponentsInChildren<AudioSource>();
        var rand = Random.Range(0, 10000);
        int play = rand < 5000 ? 0 : 1;
        m_music[play].Play();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
