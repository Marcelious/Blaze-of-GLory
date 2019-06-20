using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BurnTimer : MonoBehaviour {

    //Burn timer variable
    static public int timer = 20;
    string gameOverText = "Game over!!";
    string startGame = "Catch 5 red cubes";
    static public bool resetTime = false;

    public Text timerText;
    public Text gameOver;
    public Text startText;
    public Text leaderboard;

    public Slider burnTime;

    public SFXManager m_sfxManager;


    // Use this for initialization
    void Start () {
        m_sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        gameOver.enabled = false;
        leaderboard.enabled = false;
        burnTime.value = timer;
        //Calls DecreaseTime after 1 second from starting the game
        //Calls it again every second
        InvokeRepeating("DecreaseTime", 1, 1);
    }

    // Update is called once per frame
    void Update () {

    }

    void DecreaseTime()
    {
      
        if(burnTime.value <= 0.0f)
        {
            //When timer is 0, object is destroyed/killed
            Destroy(gameObject);
            // Sound
            m_sfxManager.PlayDoorslam();
            m_sfxManager.EnableFootstepSFX = false;
            GameOver();
            gameOver.enabled = true;
           // timerText.enabled = false;
            leaderboard.enabled = true;

            SaveData.UpdateScore(Movement.points);
            LeaderBoard board = SaveData.GetLeaderBoard();
            
            string s = "Highscores\n" + board.ToString();

            leaderboard.text = s;
        }
        else
        {
            if(resetTime == true)
            {
                burnTime.value = 20;
                resetTime = false;
            }
            //Timer decreases
            burnTime.value -= 0.08f;
        }

    }

    //void setTimerText()
    //{
    //    timerText.text = "Time Left: " + timer.ToString();
    //}

    void GameOver()
    {
        gameOver.text = gameOverText;
    }
}
