using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToMenu : MonoBehaviour {

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(WaitAndLoad(5f, "MainMenu"));
    }

    private IEnumerator WaitAndLoad(float value, string scene)
    {
        yield return new WaitForSeconds(value);
        SceneManager.LoadScene(scene);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
