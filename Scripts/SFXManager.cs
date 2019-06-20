using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour {

    public AudioClip[] Screams;
    public AudioClip[] Footsteps;
    public AudioSource DoorSlam;

    public bool EnableFootstepSFX = true;

    private AudioSource[] m_audioSources;

    public Vector2 randomRange = new Vector2(0.8f, 1.25f);

    public float m_footstepInterval = 0.2f;
    private float m_footstepWaitTimer = 0f;

    void Awake()
    {
        m_audioSources = GetComponentsInChildren<AudioSource>();
        EnableFootstepSFX = true;
    }

    public void PlayScream()
    {
        m_audioSources[0].PlayOneShot(Screams[Random.Range(0, Screams.Length - 1)]);
    }

    public void PlayFootstep()
    {
        if (EnableFootstepSFX)
        {
            if (m_footstepWaitTimer >= m_footstepInterval)
            {
                m_audioSources[1].pitch = Random.Range(randomRange.x, randomRange.y);
                m_audioSources[1].PlayOneShot(Footsteps[Random.Range(0, Footsteps.Length - 1)]);
                m_footstepWaitTimer = 0f;
            }
            m_footstepWaitTimer += Time.deltaTime;
        }
    }

    public void PlayDoorslam()
    {
        DoorSlam.pitch = Random.Range(randomRange.x, randomRange.y);
        DoorSlam.Play();
    }
}
