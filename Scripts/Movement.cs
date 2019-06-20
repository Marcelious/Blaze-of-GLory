using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    float zCoordinate;
    float xCoordinate;
    static public int  points = 0;
    public Text score;
    int enemyCount;

    public GameObject playerSprite;
    public GameObject deathFire;
    public GameObject deadNPC;

    public SFXManager m_soundManager;

    void Awake()
    {
        m_soundManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }

    // Use this for initialization
    void Start()
    {
        setPoints();
        //deathFire.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        m_soundManager.PlayFootstep();

        setPoints();

        //Debug.Log(deathFire.active);

        if(enemyCount == 12)
        {
            BurnTimer.timer = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            xCoordinate = 0.0f;
            zCoordinate = 0.0f;
            zCoordinate += 0.05f;
            playerSprite.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            xCoordinate = 0.0f;
            zCoordinate = 0.0f;
            zCoordinate += -0.05f;
            m_soundManager.PlayFootstep();
            playerSprite.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            xCoordinate = 0.0f;
            zCoordinate = 0.0f;
            xCoordinate += -0.05f;
            playerSprite.transform.rotation = Quaternion.Euler(90.0f, 180.0f, 90.0f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            xCoordinate = 0.0f;
            zCoordinate = 0.0f;
            xCoordinate += 0.05f;
            playerSprite.transform.rotation = Quaternion.Euler(90.0f, 180.0f, 270.0f);
        }

        transform.Translate(xCoordinate, 0.0f, zCoordinate);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            points+= 10;
            Destroy(other.gameObject);
            deadNPC.SetActive(false);
            deathFire.SetActive(true);
            enemyCount++;
            BurnTimer.resetTime = true;
            m_soundManager.PlayScream();
        }
    }

    void setPoints()
    {
        score.text = "Score " + points.ToString();
    }
}

